using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : Obstacle
{
    private int score = 200;
    private bool alive = false;
    private void OnEnable()
    {
        alive = true;
    }
    public override void PlayCollisionAnim()
    {
        if (alive)
        {
            alive = false;
            FindObjectOfType<GameController>().OnReachOffice();
        }
    }
}
