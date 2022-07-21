using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

// Проверки на противоположные клавиши

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rigidbody = new Rigidbody2D();
    public float _speed, _acceleration, _maxSpeed, _maxTurnSpeed, _currentTurnSpeed, _turnBunus, _maxDashReloadTime, _currentdashReloadTime, _dashDistance, _stopBonus;
    bool _isDashReloadTime, _isDash;
    Vector3 mousePosition;
    Accelerator accelerator;

    // Слежение за скоростью
    Vector2 _prevPos, _newPos;
    bool _prevDir, _newDir;

    // Пробрасываем ссылку
    PlayerControllerGamepad controllerGamepad;
    Vector2 move;

    //[SerializeField] NavigationBar;

    private void Start()
    {
        _speed = 0;
        _maxTurnSpeed = 45;
        _turnBunus = 2;
        _maxSpeed = 10;
        _acceleration = 5;
        _maxDashReloadTime = 1;
        rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.rotation = -90;
        _prevPos = transform.position;
        _newPos = transform.position;
        _prevDir = true;
        _newDir = false;
        accelerator = new Accelerator();
        _dashDistance = 10;
        _stopBonus = 4;
        controllerGamepad = new PlayerControllerGamepad();
        controllerGamepad.Init(this);
        Debug.Log("Отправили");
    }

    public void Turn(float x)
    {
     _currentTurnSpeed = _maxTurnSpeed * _speed / _maxSpeed;

        if (x > 0)
        {
            if(_speed != 0)
            {
                rigidbody.rotation += -Time.deltaTime * _maxTurnSpeed;
            }
            else
            {
                rigidbody.rotation += -Time.deltaTime * _maxTurnSpeed * _turnBunus;
            }
        }
        if (x < 0)
        {
            if (_speed != 0)
            {
                rigidbody.rotation += Time.deltaTime * _maxTurnSpeed;
            }
            else
            {
                rigidbody.rotation += Time.deltaTime * _maxTurnSpeed * _turnBunus;

            }
        }
    }

    public bool Riding(float y)
    {
        if (y > 0)
        {
            _newDir = true;
            if (_newDir != _prevDir)
            {
                _speed -= accelerator.FastStart(_acceleration * _stopBonus, _maxSpeed * _stopBonus, true, _speed);
                rigidbody.MovePosition((Vector2)transform.right * _speed * Time.fixedDeltaTime + (Vector2)transform.position);
                if (_speed < 0.15f) { _speed = 0; _prevDir = _newDir; }
            }
            else
            {
                _speed += accelerator.FastStart(_acceleration, _maxSpeed, false, _speed);
                _prevDir = _newDir;
                rigidbody.MovePosition(-(Vector2)transform.right * _speed * Time.fixedDeltaTime + (Vector2)transform.position);
            }
            return true;
        }
        else if (y < 0)
        {
            _newDir = false;
            if (_newDir != _prevDir)
            {
                _speed -= accelerator.FastStart(_acceleration * _stopBonus, _maxSpeed * _stopBonus, true, _speed);
                rigidbody.MovePosition(-(Vector2)transform.right * _speed * Time.fixedDeltaTime + (Vector2)transform.position);
                if (_speed < 0.15f) { _speed = 0; _prevDir = _newDir;  }
            }
            else
            {
                _speed += accelerator.FastStart(_acceleration, _maxSpeed, false, _speed);
                _prevDir = _newDir;
                rigidbody.MovePosition((Vector2)transform.right * _speed * Time.fixedDeltaTime  + (Vector2)transform.position);
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Deceleration()
    {
        _speed -= accelerator.LineAcceleration(_acceleration * _stopBonus, _maxSpeed, true, _speed);
        if (_speed < 0.15f) { _speed = 0; }
        if (_newDir == true)
        {
            rigidbody.MovePosition(-(Vector2)transform.right * _speed * Time.fixedDeltaTime + (Vector2)transform.position);
        }
        else
        {
            rigidbody.MovePosition((Vector2)transform.right * _speed * Time.fixedDeltaTime + (Vector2)transform.position);
        }
    }

}
