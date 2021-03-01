using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class TrackManager : MonoBehaviour
{
    private int totalConcurrentSegment = 4;
    [SerializeField] private TrackSegment[] trackPrefabs;
    [SerializeField] private GameObject officePrefab;
    private List<GameObject> tracksPool;
    public List<GameObject> activeTracks;
    private GameController gameController;
    private bool hasSpawnOffice = false;
    protected void Start()
    {
        tracksPool = new List<GameObject>();
        gameController = FindObjectOfType<GameController>();
        hasSpawnOffice = false;
    }
    void Update()
    {
        if (activeTracks.Count < totalConcurrentSegment) SpawnNewSegment();
    }
    public void SpawnNewSegment()
    {
        if (!hasSpawnOffice)
        {
            GameObject trackSegment;
            if (gameController.HasReachOffice)
            {
                trackSegment = Instantiate(officePrefab, GetSegmentSpawnPos(), Quaternion.identity);
                hasSpawnOffice = true;
            }
            else
            {
                if (tracksPool.Count > 0)
                {
                    trackSegment = tracksPool[Random.Range(0, tracksPool.Count)];
                    trackSegment.transform.position = GetSegmentSpawnPos();
                    trackSegment.SetActive(true);
                    tracksPool.Remove(trackSegment);
                }
                else
                    trackSegment = Instantiate(trackPrefabs[Random.Range(0, trackPrefabs.Length)].gameObject, GetSegmentSpawnPos(), Quaternion.identity);
                activeTracks.Add(trackSegment);
            }
        }
    }
    private Vector3 GetSegmentSpawnPos()
    {
        Vector3 spawnPos = Vector3.zero;
        if (activeTracks.Count != 0)
        {
            GameObject go = activeTracks[activeTracks.Count - 1];
            spawnPos = go.transform.position + new Vector3(0, 0, go.GetComponent<TrackSegment>().deltaToNextSegment);
        }
        return spawnPos;
    }
    public void ReturnSegmentToPool(GameObject segment)
    {
        if (activeTracks.Contains(segment))
        {
            segment.SetActive(false);
            activeTracks.Remove(segment);
            tracksPool.Add(segment);
        }
    }
}