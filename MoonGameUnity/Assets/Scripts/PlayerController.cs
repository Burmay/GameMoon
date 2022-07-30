using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

// Проверки на противоположные клавиши

public class PlayerController : MonoBehaviour
{
    public float _speed, _acceleration, _maxSpeed, _maxTurnSpeed, _currentTurnSpeed, _turnBunus, _maxDashReloadTime, _currentdashReloadTime, _dashDistance, _stopBonus, maxAccSpeed, _accMode, minSizeCam, maxSizeCam, camCurrentSize, CamDeepSpeed ;
    bool _isDashReloadTime, _isDash;
    Vector3 mousePosition;
    Accelerator accelerator;

    // Слежение за скоростью
    Vector2 _prevPos, _newPos;
    bool _prevDir, _newDir;

    Vector2 move;
    GameObject cameraTag;
    FallowCamera fallowCamera;

    public void Start()
    {
        _speed = 0;
        _maxTurnSpeed = 45;
        _turnBunus = 2;
        _maxSpeed = 10;
        _acceleration = 5;
        _maxDashReloadTime = 1;

        _prevDir = true;
        _newDir = false;
        accelerator = new Accelerator();
        _dashDistance = 10;
        _stopBonus = 4;
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

    public float Turn(float x)
    {
        _currentTurnSpeed = _maxTurnSpeed * _speed / _maxSpeed;

        if (x > 0.5)
        {
            if (_speed != 0)
            {
                return -Time.deltaTime * _maxTurnSpeed * x;
            }
            else
            {
                return -Time.deltaTime * _maxTurnSpeed * _turnBunus * x;
            }
        }
        if (x < -0.5)
        {
            if (_speed != 0)
            {
                return Time.deltaTime * _maxTurnSpeed * -x;
            }
            else
            {
                return Time.deltaTime * _maxTurnSpeed * _turnBunus * -x;

            }
        }
        return 0;
    }

    public float Riding(float y, bool mode)
    {
        Debug.Log(_speed);
        if (y > 0)
        {
            _newDir = true;
            if (_newDir != _prevDir)
            {
                
                if(mode == true || _speed + 1 > _maxSpeed)
                {
                    _speed -= accelerator.FastBreak(_acceleration * _stopBonus * _accMode, _maxSpeed, _speed, y);
                }
                else
                {
                    _speed -= accelerator.FastStart(_acceleration * _stopBonus, _maxSpeed * _stopBonus, _speed, y);
                }
                if (_speed < 0.15f) { _speed = 0; _prevDir = _newDir; }
                return _speed * Time.fixedDeltaTime;
            }
            else
            {
                if (mode == false)
                {
                    _speed += accelerator.FastStart(_acceleration, _maxSpeed, _speed, y);
                }
                else
                {
                    _speed += accelerator.FastStart(_acceleration * _accMode, maxAccSpeed, _speed, y);
                }
                _prevDir = _newDir;
                return -_speed * Time.fixedDeltaTime;
            }
        }
        else if (y < 0)
        {
            _newDir = false;
            if (_newDir != _prevDir)
            {
                if(mode == true || _speed + 1 > _maxSpeed)
                {
                    _speed -= accelerator.FastBreak(_acceleration * _stopBonus * _accMode, _maxSpeed, _speed, y);
                }
                else
                {
                    _speed -= accelerator.FastStart(_acceleration * _stopBonus, _maxSpeed * _stopBonus, _speed, y);
                }
                if (_speed < 0.15f) { _speed = 0; _prevDir = _newDir; }
                return -_speed * Time.fixedDeltaTime;
            }
            else
            {
                if (mode == false)
                {
                    _speed += accelerator.FastStart(_acceleration, _maxSpeed, _speed, y);
                }
                else
                {
                    _speed += accelerator.FastStart(_acceleration * _accMode, maxAccSpeed, _speed, y);
                }
                _prevDir = _newDir;
                return _speed * Time.fixedDeltaTime;
            }
        }
        return 0;
    }

    public float Deceleration()
    {
        if (_speed == 0) { return 0;}
        _speed -= accelerator.LineAcceleration(_acceleration * _stopBonus, _maxSpeed, true, _speed);
        if (_speed < 0.15f) { _speed = 0; }
        if (_newDir == true)
        {
            return -_speed * Time.fixedDeltaTime;
        }
        else
        {
            return _speed * Time.fixedDeltaTime;
        }
    }

    public void ChangeSizeCamera()
    {
        if(camCurrentSize < _speed / maxAccSpeed * maxSizeCam)
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
