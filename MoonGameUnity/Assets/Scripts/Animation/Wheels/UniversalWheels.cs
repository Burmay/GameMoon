using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UniversalWheels : MonoBehaviour
{
    private GameObject playerTag;
    protected PlayerController playerController;

    private void Awake()
    {
        playerTag = GameObject.FindGameObjectWithTag("PlayerController");
        playerController = playerTag.GetComponent<PlayerController>();
    }

    void SpeedRotation()
    {
        if (playerController._speed == 0)
        {
            
        }
    }
}
