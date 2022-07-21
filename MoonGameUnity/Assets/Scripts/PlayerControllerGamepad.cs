using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerGamepad : MonoBehaviour
{
    MainControls controls;
    PlayerController playerController;
    Vector2 move;
    bool mode;
    float currentReloadChangeMode;
    bool isReloadMode;
    public float reloadChangeMode;
    public float turnOnMode;

    public PlayerControllerGamepad()
    {
        mode = false;
        isReloadMode = false;
        reloadChangeMode = 1;
        turnOnMode = 0.5f;

        controls = new MainControls();
        controls.ActionsPlayerMap.Mode.performed += cts => ChangeMode();
        controls.ActionsPlayerMap.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.ActionsPlayerMap.Move.canceled += ctx => move = Vector2.zero;
    }

    public void Init(PlayerController playerController)
    {
        Debug.Log("Приняли");
        this.playerController = playerController;
    }


    public void ChangeMode()
    {
        if(isReloadMode == true)
        {
            mode = true;
            isReloadMode = false;
            currentReloadChangeMode = 0;

        }
    }

    void ReloadChangeMode()
    {
        reloadChangeMode += Time.fixedDeltaTime;
    }

    public void FixedUpdate()
    {
            Debug.Log(move.x + " " + move.y);
            if (move.x != 0)
            {
                Turn();
            }

            if (move.y != 0)
            {
                Riding();
            }

            else
            {
                Deceleration();
            }

            if (isReloadMode == false)
            {
                ReloadChangeMode();
            }
    }

    void Turn()
    {
        if (mode == false)
        {
            playerController.Turn(move.x);
        }
        else
        {
            playerController.Turn(move.x * turnOnMode);
        }
    }

    void Riding()
    {
        if (mode == false)
        {
            playerController.Riding(move.y);
        }
    }

    void Deceleration()
    {
        playerController.Deceleration();
    }

}
