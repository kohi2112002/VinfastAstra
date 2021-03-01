using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTutorial : MonoBehaviour
{
    [SerializeField] private GameObject tutorial;
    private void OnTriggerEnter(Collider other)
    {
        tutorial.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        tutorial.SetActive(false);
    }
}
