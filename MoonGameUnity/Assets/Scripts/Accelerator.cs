using UnityEngine;
using System;

public class Accelerator
{
    public float NullSpeed(float acceleration, float currentSpeed)
    {
        return -acceleration * Time.fixedDeltaTime * Math.Sign(currentSpeed);
    }

    public float LineBreak(float acceleration, float y)
    {
        return acceleration * Time.fixedDeltaTime * y;
    }

    public float FastStart(float acceleration, float maxSpeed, float currentSpeed, float y)
    {
        if (Math.Abs(currentSpeed) < maxSpeed)
        {
            if (maxSpeed - Math.Abs(currentSpeed) > acceleration)
            {
                return acceleration * Time.fixedDeltaTime * y;
            }
            else
            {
                return (maxSpeed - Math.Abs(currentSpeed)) * Time.fixedDeltaTime * y;
            }
        }
        else
        {
            return 0;
        }
    }

    public float FastBreak(float acceleration, float currentSpeed, float maxAccSpeed, float y)
    {
        if (Math.Abs(currentSpeed) > maxAccSpeed / 2)
        {
            return (Math.Abs(currentSpeed) / maxAccSpeed) * acceleration * Time.fixedDeltaTime * y;
        }
        else
        {
            return LineBreak(acceleration, y);
        }
    }
}