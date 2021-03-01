using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class PropSegment
{
    public int lane1 = 1, lane2 = 0, lane3 = 0;
}
public class PropManager : MonoBehaviour
{
    [SerializeField] private int propSeekRange = 10;
    [SerializeField] private int[] keys;
    [SerializeField] private GameObject[] props;
    [SerializeField] private PropSegment[] propData;
    private int currentPropIndex = 0;
    private Dictionary<int, GameObject> propCollection = new Dictionary<int, GameObject>();
    private void Start()
    {
        for (int i = 0; i < keys.Length; i++)
            propCollection.Add(keys[i], props[i]);
    }
    public void SpawnProp(int currentPlayerPos)
    {
        if (currentPropIndex * 2 < currentPlayerPos + propSeekRange)
        {
            for (int i = (currentPlayerPos + propSeekRange) / 2 - 1; i >= currentPropIndex; i--)
                Spawn(i % 180, i);
            currentPropIndex = (int)((currentPlayerPos + propSeekRange) / 2);
        }
    }
    private void Spawn(int index, int distance)
    {
        PropSegment segment = propData[index];
        if (segment.lane1 != 0)
            Instantiate(propCollection[segment.lane1], GetSpawnPos(distance, 0), Quaternion.identity);
        if (segment.lane2 != 0)
            Instantiate(propCollection[segment.lane2], GetSpawnPos(distance, 1), Quaternion.identity);
        if (segment.lane3 != 0)
            Instantiate(propCollection[segment.lane3], GetSpawnPos(distance, 2), Quaternion.identity);
    }
    private Vector3 GetSpawnPos(int distance, int lane)
    {
        return new Vector3((lane - 1) * 1.25f, 0.15f, distance * 2);
    }
}
