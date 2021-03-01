using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Coin : Obstacle
{
    private int score = 200;
    private bool alive = false;
    [SerializeField] private GameObject[] props;
    [SerializeField] private int lifeTime = 7;
    [SerializeField] private GameObject audioSource;
    private void OnEnable()
    {
        alive = true;
        int propIndex = Random.Range(0, props.Length);
        Instantiate(props[propIndex], transform.position, Quaternion.identity, transform);
        score = propIndex > 2 ? 100 : 200;
        Destroy(gameObject, lifeTime);
    }
    private IEnumerator FloatUp()
    {
        yield return null;
        FindObjectOfType<GameController>().Score += score;
        GameObject go = Instantiate(audioSource, transform.position, Quaternion.identity);
        Destroy(go, 1);
        Destroy(gameObject);
    }
    public override void PlayCollisionAnim()
    {
        if (alive)
        {
            alive = false;
            StartCoroutine(FloatUp());
        }
    }
}
