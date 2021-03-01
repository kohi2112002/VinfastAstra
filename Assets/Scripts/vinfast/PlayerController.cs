using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5, laneSpeed = 3;
    private float actualSpeed = 0;
    private int currentLane = 0;
    private float lastTimeSwitchLane, jumpStart, yPos, stunTime, turnVal = 0.5f;
    private Vector3 laneTargetPos;
    [SerializeField] private Transform playerCollider;
    private bool jumping = false, isHitted = false, isPause = true;
    private AstraInputController astraInputController;
    private CameraFollow cameraFollow;
    [SerializeField] private GameObject[] bikeMeshs;
    private Animator bikeAnimator;
    private IEnumerator Start()
    {
        yield return null;
        isPause = true;//stop player to wait for tutorial
        cameraFollow = FindObjectOfType<CameraFollow>();
        astraInputController = FindObjectOfType<AstraInputController>();
        astraInputController.onDetectBody += OnDetectBody;
        Instantiate(bikeMeshs[DataManager.Instance.dataCollection.vehicleIndex], playerCollider.transform.position, Quaternion.identity, playerCollider.transform);
        bikeAnimator = GetComponentInChildren<Animator>();
        actualSpeed = speed / 4;
    }
    private void Destroy()
    {
        astraInputController.onDetectBody -= OnDetectBody;
    }
    void Update()
    {
        if (!isPause)
        {
            if (!isHitted)
            {
                if (Time.time - lastTimeSwitchLane > 0.5f)
                {
                    if (Input.GetAxis("Horizontal") > 0.1f)
                        ChangeLane(1);
                    else if (Input.GetAxis("Horizontal") < -0.1f)
                        ChangeLane(-1);
                }
                if (actualSpeed < speed) actualSpeed += Time.deltaTime;
                transform.Translate(transform.forward * Time.deltaTime * actualSpeed);
                float deltaX = laneTargetPos.x - playerCollider.localPosition.x;
                float targetTurnVal = 0.5f;
                if (deltaX > 0.15f)
                    targetTurnVal = 1;
                else if (deltaX < -0.15f)
                    targetTurnVal = 0;
                else
                if (-0.02f < deltaX && deltaX < 0.02f)
                    targetTurnVal = 0.5f;
                playerCollider.localPosition = Vector3.Lerp(playerCollider.localPosition, laneTargetPos + new Vector3(0, yPos, 0), Time.deltaTime * laneSpeed);
                turnVal = Mathf.Lerp(turnVal, targetTurnVal, Time.deltaTime * 5);
                bikeAnimator.SetFloat("Turn", turnVal);
            }
            else
            {
                stunTime += Time.deltaTime;
                if (stunTime > 1f)
                {
                    isHitted = false;
                    stunTime = 0;
                }
            }
        }
    }
    private void ChangeLane(int value)
    {
        if (currentLane + value > 1 || currentLane + value < -1) return;
        lastTimeSwitchLane = Time.time;
        currentLane += value;
        laneTargetPos = new Vector3(currentLane * 1.5f, 0, 0);
    }
    public void OnPlayerDie()
    {
        isHitted = true;
    }
    public void StartRunning()
    {
        cameraFollow.Follow = true;
        playerCollider.gameObject.SetActive(true);
        isPause = false;
    }
    public void StopRunning()
    {
        isPause = true;
    }
    private void OnDetectBody(bool status, Vector3 bodyPos)
    {
        float xPos = Mathf.Clamp(bodyPos.x * 2.25f, -1.5f, 1.5f);
        if (status)
        {
            laneTargetPos = new Vector3(xPos, 0, 0);
        }
    }
}
