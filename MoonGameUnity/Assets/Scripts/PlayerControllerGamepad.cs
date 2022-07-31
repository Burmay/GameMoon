using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerGamepad : MonoBehaviour
{
    PlayerControls controls;
    PlayerController playerController;
    GameObject playerTag;
    Vector2 move;
    bool mode;
    bool isReloadMode;
    public float reloadChangeMode, turnOnMode;
    Vector2 _prevPos, _newPos;

    public void Start()
    {
        _prevPos = transform.position;
        _newPos = transform.position;

        playerTag = GameObject.FindGameObjectWithTag("PlayerController");
        playerController = playerTag.GetComponent<PlayerController>();

        mode = false;
        isReloadMode = false;
        reloadChangeMode = 1;
        turnOnMode = 0.5f;
    }

    public void Awake()
    {
        controls = new PlayerControls();
        controls.Gameplay.ChangeMode.performed += cts => ChangeMode();
        controls.Gameplay.Move.performed += ctx => move = ctx.ReadValue<Vector2>();
        controls.Gameplay.Move.canceled += ctx => move = Vector2.zero;
    }


    public void ChangeMode()
    {
        if(isReloadMode == true)
        {
            if(mode == true) { mode = false; Debug.Log("MODE OFF");}
            else { mode = true; Debug.Log("MODE ON"); }
            isReloadMode = false;
            reloadChangeMode = 0;
        }
    }

    void ReloadChangeMode()
    {
        if(reloadChangeMode > 1)
        {
            isReloadMode = true;
        }
        reloadChangeMode += Time.fixedDeltaTime;
    }

    public void FixedUpdate()
    {
        Move();
        playerController.ChangeSizeCamera();
    }

    void Move()
    {
        playerController.Turn(move.x);

        if (move.y != 0)
        {
            playerController.Riding(move.y, mode);
        }

        else
        {
            playerController.Deceleration();
        }


        if (isReloadMode == false)
        {
            ReloadChangeMode();
        }

    }

    void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

}