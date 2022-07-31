using UnityEngine;
using System;

public class Accelerator
{
    public float LineBreak(float acceleration, float currentSpeed, float y = 1)
    {
        if (Math.Sign(acceleration * Time.fixedDeltaTime * Math.Sign(currentSpeed)) != Math.Sign(currentSpeed) )
        {
            return 0;
        }
        else
        {
            return acceleration * Time.fixedDeltaTime * Math.Sign(currentSpeed) * Math.Abs(y);
        }
    }

    public float FastStart(float acceleration, float maxSpeed, float currentSpeed, float y)
    {
        if (Math.Abs(currentSpeed) < maxSpeed)
        {
            if (maxSpeed - Math.Abs(currentSpeed) > acceleration)
            {
                return acceleration * Time.fixedDeltaTime * -y;
            }
            else
            {
                return (maxSpeed - Math.Abs(currentSpeed)) * Time.fixedDeltaTime * -y;
            }
        }
        else
        {
            return 0;
        }
    }

    public float FastBreak(float acceleration, float currentSpeed, float maxAccSpeed, float y)
    {
        if(Math.Abs(currentSpeed) > maxAccSpeed / 2)
        {
            return Math.Abs(currentSpeed) / maxAccSpeed * acceleration * Time.fixedDeltaTime * Math.Abs(y);
        }
        else
        {
            return - LineBreak(acceleration,currentSpeed,y);
        }
    }
}