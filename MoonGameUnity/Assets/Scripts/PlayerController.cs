using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

// Проверки на противоположные клавиши

public class PlayerController : MonoBehaviour
{
    public float speed, _acceleration, _maxSpeed, _maxTurnSpeed, _currentTurnSpeed, _turnBunus, _maxDashReloadTime, _currentdashReloadTime, _dashDistance, _stopBonus, maxAccSpeed, _accMode, minSizeCam, maxSizeCam, camCurrentSize, CamDeepSpeed ;
    bool _isDashReloadTime, _isDash;
    Vector3 mousePosition;
    Accelerator accelerator;

    // Слежение за скоростью
    Vector2 _prevPos, _newPos;

    Vector2 move;
    GameObject cameraTag;
    FallowCamera fallowCamera;
    Rigidbody2D rigidbody2D;

    public void Start()
    {
        rigidbody2D = new Rigidbody2D();

        rigidbody2D = GetComponent<Rigidbody2D>();
        rigidbody2D.rotation = -90;

        speed = 0;
        _maxTurnSpeed = 45;
        _turnBunus = 2;
        _maxSpeed = 10;
        _acceleration = 5;
        _maxDashReloadTime = 1;
        accelerator = new Accelerator();
        _dashDistance = 10;
        _stopBonus = 2;
        maxAccSpeed = 50;
        _accMode = 1.5f;
        minSizeCam = 15;
        maxSizeCam = 50;
        CamDeepSpeed = 2;

        cameraTag = GameObject.FindGameObjectWithTag("MainCamera");
        fallowCamera = cameraTag.GetComponent<FallowCamera>();
        camCurrentSize = Camera.main.orthographicSize;
    }
    public void SetStartPos(Vector2 prevPos, Vector2 newPos)
    {
        _prevPos = prevPos;
        _newPos = newPos;
    }

    public void Turn(float x)
    {
        if (Math.Abs(speed) < _maxSpeed)
        {
            _currentTurnSpeed = _maxTurnSpeed * Math.Abs(speed) / _maxSpeed;
        }
        else
        {
            _currentTurnSpeed = _maxTurnSpeed;
        }

        if (x > 0.5)
        {
            if (speed != 0)
            {
                rigidbody2D.rotation += -Time.deltaTime * _currentTurnSpeed * x;
            }
            else
            {
                rigidbody2D.rotation += -Time.deltaTime * _maxTurnSpeed * _turnBunus * x;
            }
        }
        if (x < -0.5)
        {
            if (speed != 0)
            {
                rigidbody2D.rotation += Time.deltaTime * _currentTurnSpeed * -x;
            }
            else
            {
                rigidbody2D.rotation += Time.deltaTime * _maxTurnSpeed * _turnBunus * -x;
            }
        }
    }

    public void Riding(float y, bool mode)
    {
        Debug.Log(speed);
        if(speed == 0)
        {
            if (y > 0)
            {
                speed = -0.1f;
            }
            else
            {
                speed = 0.1f;
            }
        }
        if (y > 0)
        {
            if (mode == true)
            {
                if (speed < 0)
                {
                    speed += accelerator.FastStart(_accMode * _acceleration, maxAccSpeed, speed, y);
                }
                else
                {
                    speed += accelerator.FastBreak(_accMode * _acceleration * _stopBonus, speed, maxAccSpeed, y);
                }
            }
            else if(speed > 0 || speed > _maxSpeed)
            {
                speed -= accelerator.LineBreak(_acceleration * _accMode * _stopBonus, speed);
            }
            else
            {
                speed += accelerator.FastStart(_acceleration, _maxSpeed * _stopBonus, speed, y);
                if (Math.Abs(speed) > _maxSpeed) { speed = -_maxSpeed; }
            }
            rigidbody2D.MovePosition(speed * (Vector2)transform.right * Time.fixedDeltaTime + (Vector2)transform.position); 
        }
        else if (y < 0)
        {
            if (mode == true)
            {
                if (speed > 0)
                {
                    speed += accelerator.FastStart(_accMode * _acceleration, maxAccSpeed, speed, y);
                }
                else
                {
                    speed += accelerator.FastBreak(_accMode * _acceleration * _stopBonus, speed, maxAccSpeed, y);
                }
            }
            else if (speed < 0 || Math.Abs(speed) > _maxSpeed)
            {
                speed -= accelerator.LineBreak(_acceleration * _stopBonus * _accMode, speed);
            }
            else
            {
                speed += accelerator.FastStart(_acceleration, _maxSpeed * _stopBonus, speed, y);
                if(Math.Abs(speed) > _maxSpeed) { speed = _maxSpeed; }
            }
            rigidbody2D.MovePosition(speed * (Vector2)transform.right * Time.fixedDeltaTime + (Vector2)transform.position);
        }
    }

    public void Deceleration()
    {

        speed += accelerator.LineBreak(_acceleration * _stopBonus, -speed);
        if(speed < 0.15f && speed > -0.15f) { speed = 0; }
            rigidbody2D.MovePosition(speed * Time.fixedDeltaTime * (Vector2)transform.right + (Vector2)transform.position);
    }

    public void ChangeSizeCamera()
    {
        if(camCurrentSize < Math.Abs(speed) / maxAccSpeed * maxSizeCam)
        {
            camCurrentSize += CamDeepSpeed * Time.fixedDeltaTime;
        }
        else
        {
            camCurrentSize -= CamDeepSpeed * Time.fixedDeltaTime * 3;
        }
        if(camCurrentSize < minSizeCam)
        {
            camCurrentSize = minSizeCam;
        }
        Camera.main.orthographicSize = camCurrentSize;
    }

}
