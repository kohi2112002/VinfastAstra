using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TutorialController : MonoBehaviour
{
    [SerializeField] private float thresHoldX = 0.1f, thresholdZ = 0.15f;
    private float totalTime;
    [SerializeField] private Button ready;
    [SerializeField] private Image statusLight;
    private void Start()
    {
        statusLight.color = Color.red;
        ready.interactable = false;
        StartCoroutine(TutorialLoop());
        FindObjectOfType<AstraInputController>().onDetectBody += OnDetectBody;
    }
    private IEnumerator TutorialLoop()
    {
        while (totalTime < 0.5f)
            yield return null;
        ready.interactable = true;
        FindObjectOfType<AstraInputController>().onDetectBody -= OnDetectBody;
    }
    private void OnDetectBody(bool status, Vector3 bodyPos)
    {
        if (Mathf.Abs(bodyPos.x) < thresHoldX && Mathf.Abs(bodyPos.z - 2.5f) < thresholdZ)
        {
            totalTime += Time.deltaTime;
            statusLight.color = Color.green;
        }
        else
            statusLight.color = Color.red;
    }
}
