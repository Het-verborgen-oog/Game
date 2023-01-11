using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ArduinoValues
{
    private static float desiredMaxValueMovement = 1f;
    private static float desiredMaxValueSpeed = 2.5f;

    public static float xMovement;
    public static float yMovement;

    public static void GetvaluePotXMovement(float potValue)
    {        
        xMovement = -GetCalibrateValue(GlobalPotValues.HorizontalValues, potValue);
    }

    public static void GetvaluePotYMovement(float potValue)
    {
        yMovement = GetCalibrateValue(GlobalPotValues.VerticalValues, potValue);
    }

    public static float GetValuePotSpeed(float potValue)
    {
        float speedPercentage = (potValue - GlobalPotValues.SpeedValues.MinValue) / (GlobalPotValues.SpeedValues.MaxValue - GlobalPotValues.SpeedValues.MinValue);
        if (speedPercentage <= 0)
        {
            speedPercentage = 0;
        }
        return desiredMaxValueSpeed * (speedPercentage + 0.25f);
    }

    private static float GetCalibrateValue(DifferentPotValues differentPotValues, float potValue)
    {
        float i = 0;

        float tempPotValue = potValue;

        if (tempPotValue > differentPotValues.TurnoverValue)
        {
            tempPotValue -= differentPotValues.TurnoverValue;
            float tempMaxValue = differentPotValues.MaxValue - differentPotValues.TurnoverValue;
            i = tempPotValue / tempMaxValue;
        }
        else if (tempPotValue < differentPotValues.TurnoverValue)
        {
            tempPotValue -= differentPotValues.MinValue;
            tempPotValue = differentPotValues.TurnoverValue - tempPotValue;
            float tempTurnoverValue = differentPotValues.TurnoverValue - differentPotValues.MinValue;
            i = tempPotValue / tempTurnoverValue * -1f;
        }
        else if (tempPotValue == differentPotValues.TurnoverValue)
        {
            i = 0;
        }
        return i;
    }

    public static void CheckDirectionalSpeed()
    {
        float maxAngleX = AngleByXCoordinate(xMovement);
        float maxYCoordinateByXAngle = MaxYCoordinate(maxAngleX);

        float maxAngleY = AngleByYCoordinate(yMovement);
        float maxXCoordinateByYAngle = MaxXCoordinate(maxAngleY);

        if (yMovement < maxYCoordinateByXAngle && yMovement > -maxYCoordinateByXAngle)
        {
            return;
        }
        if (xMovement < maxXCoordinateByYAngle && xMovement > -maxXCoordinateByYAngle)
        {
            return;
        }

        float averageAngle = (maxAngleX + maxAngleY) / 2;
        float newXCoordinate = MaxXCoordinate(averageAngle);
        float newYCoordinate = MaxYCoordinate(averageAngle);
        if (newXCoordinate < 0)
        {
            newXCoordinate *= -1;
        }
        if (newYCoordinate < 0)
        {
            newYCoordinate *= -1;
        }

        xMovement *= newXCoordinate;
        yMovement *= newYCoordinate;
    }

    private static float MaxXCoordinate(float angle)
    {
        return Mathf.Cos(angle);
    }

    private static float AngleByXCoordinate(float xCoordinate)
    {
        return Mathf.Round(Mathf.Acos(xCoordinate));
    }

    private static float MaxYCoordinate(float angle)
    {
        return Mathf.Sin(angle);
    }

    private static float AngleByYCoordinate(float yCoordinate)
    {
        return Mathf.Round(Mathf.Asin(yCoordinate));
    }
}
