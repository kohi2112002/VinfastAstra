using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class VinEnergy : Obstacle
{
    [SerializeField] private int lifeTime = 7;
    [SerializeField] private GameObject audioSource;
    private bool alive = false;
    private void OnEnable()
    {
        alive = true;
        Destroy(gameObject, lifeTime);
    }
    private IEnumerator FloatUp()
    {
        yield return null;
        FindObjectOfType<GameController>().Energy += 5;
        FindObjectOfType<GameController>().Score += 50;
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
