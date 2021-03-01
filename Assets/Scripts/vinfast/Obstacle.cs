using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class Obstacle : MonoBehaviour
{
    protected Animation _animation;
    public abstract void PlayCollisionAnim();
}
