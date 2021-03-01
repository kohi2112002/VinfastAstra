using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerCollisionHandle : MonoBehaviour
{
    public PlayerController playerController;
    private bool invincible = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Obstacle")
        {
            other.gameObject.SendMessage("PlayCollisionAnim");
        }
    }
}
