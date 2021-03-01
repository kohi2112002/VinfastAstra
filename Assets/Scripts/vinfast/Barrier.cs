using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Barrier : Obstacle
{
    [SerializeField] private int lifeTime = 7;
    public GameObject[] props;
    [SerializeField] private GameObject crashAudio;
    private void OnEnable()
    {
        Instantiate(props[Random.Range(0, props.Length)], transform.position - new Vector3(0, 0.15f, 0), Quaternion.identity, transform);
        Destroy(gameObject, lifeTime);
    }
    public override void PlayCollisionAnim()
    {
        FindObjectOfType<GameController>().OnCrash();
        GameObject go = Instantiate(crashAudio, transform.position, Quaternion.identity);
        Destroy(go, 1);
        Destroy(gameObject);
    }
}
