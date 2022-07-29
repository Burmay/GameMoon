using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerGamepad : MonoBehaviour
{
    Rigidbody2D rigidbody2D;
    PlayerControls controls;
    PlayerController playerController;
    Vector2 move;
    bool mode;
    bool isReloadMode;
    public float reloadChangeMode, turnOnMode;
    Vector2 _prevPos, _newPos;

    public void Start()
    {
        _prevPos = transform.position;
        _newPos = transform.position;
        playerController = new PlayerController(_prevPos, _newPos);

        mode = false;
        isReloadMode = false;
        reloadChangeMode = 1;
        turnOnMode = 0.5f;

        rigidbody2D = new Rigidbody2D();
        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.rotation = -90;
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

            if(playerController._speed + 0.1 > playerController._maxSpeed && mode == false)
            {
                Deceleration();
        }
    }

    void Turn()
    {
        float currentTurn;
        if (mode == false)
        {
            currentTurn = playerController.Turn(move.x);
            rigidbody2D.rotation += currentTurn;
        }
        else
        {
            currentTurn = playerController.Turn(move.x);
            rigidbody2D.rotation += currentTurn;
        }
    }

    void Riding()
    {
        if (mode == false)
        {
            rigidbody2D.MovePosition(playerController.Riding(move.y, false) * (Vector2)transform.right + (Vector2)transform.position);
        }
        else
        {
            rigidbody2D.MovePosition(playerController.Riding(move.y, true) * (Vector2)transform.right + (Vector2)transform.position);
        }
    }

    void Deceleration()
    {
        rigidbody2D.MovePosition(playerController.Deceleration() * (Vector2)transform.right + (Vector2)transform.position);
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
