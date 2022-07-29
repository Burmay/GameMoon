using UnityEngine;
using System;

public class Accelerator
{
    public float LineAcceleration(float acceleration, float maxSpeed, bool backmove, float currentSpeed = 0.1f)
    {
        if (currentSpeed < maxSpeed || backmove == true)
        {
            return acceleration * Time.fixedDeltaTime;
        }
        else
        {
            return 0;
        }
    }

    public float FastStart(float acceleration, float maxSpeed, float currentSpeed = 0.1f, float y = 1f)
    {
        if (currentSpeed < maxSpeed)
        {
            if (maxSpeed - currentSpeed > acceleration)
            {
                return Math.Abs(acceleration * Time.fixedDeltaTime * Math.Abs(y));
            }
            else
            {
                return Math.Abs((maxSpeed - currentSpeed) * Time.fixedDeltaTime * Math.Abs(y));
            }
        }
        else
        {
            return 0;
        }
    }

    public float FastBreak(float acceleration, float maxSpeed, float currentSpeed = 0.1f, float y = 1f)
    {
        if (currentSpeed < maxSpeed)
        {
            return FastStart(acceleration, maxSpeed, currentSpeed, Math.Abs(y));
        }
        else
        {
            return Math.Abs(acceleration * Time.fixedDeltaTime * Math.Abs(y));
        }
    }
}