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
        if(base.playerController.speed == 0)
        {
            animator.speed = 0;
        }
        else
        {
            if(base.playerController.speed < 0)
            {
                animator.speed = -playerController.speed * 1.25f;
            }
            else
            {
                animator.speed = playerController.speed * 1.25f;
            }
        }
    }
}
