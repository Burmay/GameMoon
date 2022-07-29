using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

// �������� �� ��������������� �������

public class PlayerController
{
    public float _speed, _acceleration, _maxSpeed, _maxTurnSpeed, _currentTurnSpeed, _turnBunus, _maxDashReloadTime, _currentdashReloadTime, _dashDistance, _stopBonus, _speedMode, _accMode;
    bool _isDashReloadTime, _isDash;
    Vector3 mousePosition;
    Accelerator accelerator;

    // �������� �� ���������
    Vector2 _prevPos, _newPos;
    bool _prevDir, _newDir;

    Vector2 move;

    //[SerializeField] NavigationBar;

    public PlayerController(Vector2 prevPos, Vector2 newPos)
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
        _speedMode = 5;
        _accMode = 1.5f;
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
                    _speed += accelerator.FastStart(_acceleration * _accMode, _maxSpeed * _speedMode, _speed, y);
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
                    _speed += accelerator.FastStart(_acceleration * _accMode, _maxSpeed * _speedMode, _speed, y);
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

}
