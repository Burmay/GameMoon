using UnityEngine;

public class Accelerator
{
    public float LineAcceleration(float acceleration, float maxSpeed, bool backmove, float currentSpeed = 0.1f)
    {
        if(currentSpeed < maxSpeed || backmove == true)
        {
            return acceleration * Time.fixedDeltaTime;
        }
        else
        {
            return 0;
        }
    }

    public float FastStart(float acceleration, float maxSpeed, bool backmove, float currentSpeed = 0.1f)
    {
        if (currentSpeed < maxSpeed || backmove == true)
        {
            if(maxSpeed - currentSpeed > acceleration)
            {
                return acceleration * Time.fixedDeltaTime;
            }
            else
            {
                return (maxSpeed - currentSpeed) * Time.fixedDeltaTime;
            }
        }
        else
        {
            return 0;
        }
    }
}
