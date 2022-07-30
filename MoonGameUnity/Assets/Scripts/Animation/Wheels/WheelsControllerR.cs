using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelsControllerR : UniversalWheels
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
        animator.speed = 0;
    }
}
