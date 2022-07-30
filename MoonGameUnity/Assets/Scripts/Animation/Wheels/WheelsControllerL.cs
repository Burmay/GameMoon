using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsControllerL : UniversalWheels
{
    Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        SpeedRotation();
    }

    void SpeedRotation()
    {
        if(base.playerController._speed == 0)
        {
            animator.speed = 0;
        }
    }
}
