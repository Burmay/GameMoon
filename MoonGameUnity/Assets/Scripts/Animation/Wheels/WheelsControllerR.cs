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
        animator.SetFloat("speed", playerController.speed * animationSpeedMultiplier);
    }


}
