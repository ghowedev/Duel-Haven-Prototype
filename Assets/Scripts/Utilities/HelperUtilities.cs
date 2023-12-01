using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperUtilities
{
    public static Camera mainCamera;

    public static Vector3 GetMouseWorldPosition()
    {
        if (mainCamera == null) mainCamera = Camera.main;

        Vector3 mouseScreenPosition = Input.mousePosition;

        // Clamp mouse position to screen size. EDIT: THIS CODE CAUSES X VALUE TO FLICKER, RE-ENABLE AND BUG FIX LATER IF NEEDED
        // mouseScreenPosition.x = Mathf.Clamp(mouseScreenPosition.x, 0f, Screen.width);
        // mouseScreenPosition.y = Mathf.Clamp(mouseScreenPosition.y, 0f, Screen.height);

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);

        worldPosition.z = 0f;

        return worldPosition;

    }

    public static float GetAngleFromVector(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);

        float degrees = radians * Mathf.Rad2Deg;

        return degrees;

    }

    public static Vector3 GetDirectionVectorFromAngle(float angle)
    {
        Vector3 directionVector = new Vector3(Mathf.Cos(Mathf.Deg2Rad * angle), Mathf.Sin(Mathf.Deg2Rad * angle), 0f);
        return directionVector;
    }

    public static string GetNearestDirectionFromAngle(float angle)
    {
        string direction;

        if (angle < 0)
        {
            angle += 360;
        }

        if (angle < 22.5 || angle >= 337.5)
            direction = "Right";
        else if (angle < 67.5)
            direction = "Up Right";
        else if (angle < 112.5)
            direction = "Up";
        else if (angle < 157.5)
            direction = "Up Left";
        else if (angle < 202.5)
            direction = "Left";
        else
            direction = "Down";

        return direction;
    }

    public static Directions GetAimDirection(float angle)
    {
        Directions aimDirection;

        //Up Right
        if (angle >= 22f && angle <= 67f)
        {
            aimDirection = Directions.UpRight;
        }
        // Up
        else if (angle > 67f && angle <= 112f)
        {
            aimDirection = Directions.Up;
        }
        // Up Left
        else if (angle > 112f && angle <= 158f)
        {
            aimDirection = Directions.UpLeft;
        }
        // Left
        else if ((angle <= 180f && angle > 158f) || (angle > -180 && angle <= -135f))
        {
            aimDirection = Directions.Left;
        }
        // Down
        else if ((angle > -135f && angle <= -45f))
        {
            aimDirection = Directions.Down;
        }
        // Right
        else if ((angle > -45f && angle <= 0f) || (angle > 0 && angle < 22f))
        {
            aimDirection = Directions.Right;
        }
        else
        {
            aimDirection = Directions.Right;
        }

        return aimDirection;

    }


}
