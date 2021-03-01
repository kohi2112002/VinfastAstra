using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BusBarrier : Obstacle
{
    private bool hasSwitchLane = false;
    private PlayerController player;
    private Vector3 targetPos;
    [SerializeField] private int lookRange = 3;
    [SerializeField] private GameObject crashAudio;
    private void OnEnable()
    {
        targetPos = transform.position;
        player = FindObjectOfType<PlayerController>();
    }
    private void Update()
    {
        if (!hasSwitchLane && Vector3.Distance(transform.position, player.transform.position) < lookRange)
        {
            hasSwitchLane = true;
            SwitchLane();
        }
        transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * 2);
    }
    public override void PlayCollisionAnim()
    {
        FindObjectOfType<GameController>().OnCrash();
        GameObject go = Instantiate(crashAudio, transform.position, Quaternion.identity);
        Destroy(go, 1);
        Destroy(gameObject, 0.75f);
    }
    private void SwitchLane()
    {
        int laneToSwitch = GetAvailableLane();
        targetPos = new Vector3(laneToSwitch * 1.5f, 0, transform.position.z);
    }
    private int GetAvailableLane()
    {
        if (transform.position.x > 0.25f || transform.position.x < -0.25f)
            return 0;
        else
            return Random.Range(0, 10) > 4 ? 1 : -1;
    }
}
