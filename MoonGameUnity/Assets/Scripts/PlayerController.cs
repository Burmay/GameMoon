using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

// Проверки на противоположные клавиши

public class PlayerController : MonoBehaviour
{
    public float speed, _acceleration, _maxSpeed, _maxTurnSpeed, _currentTurnSpeed, _turnBunus, _maxDashReloadTime, _currentdashReloadTime, _dashDistance, _stopBonus, maxAccSpeed, _accMode, minSizeCam, maxSizeCam, camCurrentSize, CamDeepSpeed, maxAngleTurn, speedWheelsRotation;
    bool _isDashReloadTime, _isDash;
    Accelerator accelerator;
    GameObject cameraTag, wheelsRTag, wheelsLTag, target;
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
        maxAngleTurn = 35;
        speedWheelsRotation = 2;

        cameraTag = GameObject.FindGameObjectWithTag("MainCamera");
        fallowCamera = cameraTag.GetComponent<FallowCamera>();
        camCurrentSize = Camera.main.orthographicSize;

        target = GameObject.FindGameObjectWithTag("Player");
        wheelsRTag = GameObject.FindGameObjectWithTag("WheelsFR");
        wheelsLTag = GameObject.FindGameObjectWithTag("WheelsFL");
    }

    public void Turn(float x)
    {
        //if (Math.Abs(speed) < _maxSpeed)
        //{
        //    _currentTurnSpeed = _maxTurnSpeed * Math.Abs(speed) / _maxSpeed;
        //}
        //else
        //{
        //    _currentTurnSpeed = _maxTurnSpeed;
        //}
        wheelsRTag.transform.rotation = Quaternion.Slerp(wheelsRTag.transform.rotation, Quaternion.Euler(0,0, 270 - maxAngleTurn * x), speedWheelsRotation * Time.fixedDeltaTime);
        wheelsLTag.transform.rotation = Quaternion.Slerp(wheelsRTag.transform.rotation, Quaternion.Euler(0,0, 270 - maxAngleTurn * x), speedWheelsRotation * Time.fixedDeltaTime);

        Debug.Log(wheelsRTag.transform.rotation.eulerAngles.z);
    }

    void TurnWhenDriving()
    {
         if (wheelsRTag.transform.rotation.eulerAngles.z > 270)
         {
             rigidbody2D.rotation += Math.Abs(270 - wheelsRTag.transform.rotation.eulerAngles.z) * Time.fixedDeltaTime * _maxTurnSpeed;
         }
         else
         {
             rigidbody2D.rotation += Math.Abs(270 - wheelsRTag.transform.rotation.eulerAngles.z) * Time.fixedDeltaTime * _maxTurnSpeed;
         }
    }

    public void Riding(float y, bool mode)
    {
        //Debug.Log(speed + " " + y);
        //TurnWhenDriving();
        if(speed == 0)
        {
            if (y > 0)
            {
                speed = 0.1f;
            }
            else
            {
                speed = -0.1f;
            }
        }
        if (y > 0)
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
                speed += accelerator.LineBreak(_acceleration * _stopBonus * _accMode, y);
            }
            else
            {
                speed += accelerator.FastStart(_acceleration, _maxSpeed * _stopBonus, speed, y);
                if (Math.Abs(speed) > _maxSpeed) { speed = _maxSpeed; }
            }
        }
        else if (y < 0)
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
            else if (speed > 0 || Math.Abs(speed) > _maxSpeed)
            {
                speed += accelerator.LineBreak(_acceleration * _accMode * _stopBonus, y);
            }
            else
            {
                speed += accelerator.FastStart(_acceleration, _maxSpeed * _stopBonus, speed, y);
                if (Math.Abs(speed) > _maxSpeed) { speed = -_maxSpeed; }
            }
        }   
        rigidbody2D.MovePosition(-speed * (Vector2)transform.right * Time.fixedDeltaTime + (Vector2)transform.position);
    }

    public void Deceleration()
    {

        speed += accelerator.NullSpeed(_acceleration * _stopBonus, speed);
        if(speed < 0.15f && speed > -0.15f) { speed = 0; }
            rigidbody2D.MovePosition(-speed * Time.fixedDeltaTime * (Vector2)transform.right + (Vector2)transform.position);
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