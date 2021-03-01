using UnityEngine;
using System.Collections;
using System.Collections.Generic;
/// <summary>
/// This defines a "piece" of the track. This is attached to the prefab and contains data such as what obstacles can spawn on it.
/// It also defines places on the track where obstacles can spawn. The prefab is placed into a ThemeData list.
/// </summary>
public class TrackSegment : MonoBehaviour
{
    public const float LANE_OFFSET = 1.5f;
    public float deltaToNextSegment = 18;
    private TrackManager trackManager;
    private void OnEnable()
    {
        trackManager = FindObjectOfType<TrackManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        trackManager.ReturnSegmentToPool(gameObject);
    }
}