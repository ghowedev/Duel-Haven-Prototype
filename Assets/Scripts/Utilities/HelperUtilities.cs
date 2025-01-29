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
    public static Vector3 GetMousePlayerPosition(Transform playerTransform)
    {
        if (mainCamera == null) mainCamera = Camera.main;

        Vector3 mouseScreenPosition = Input.mousePosition;

        // Clamp mouse position to screen size. EDIT: THIS CODE CAUSES X VALUE TO FLICKER, RE-ENABLE AND BUG FIX LATER IF NEEDED
        // mouseScreenPosition.x = Mathf.Clamp(mouseScreenPosition.x, 0f, Screen.width);
        // mouseScreenPosition.y = Mathf.Clamp(mouseScreenPosition.y, 0f, Screen.height);

        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(mouseScreenPosition);

        worldPosition -= playerTransform.position;

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
        string direction = "Down";

        angle = (angle + 360) % 360;

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
        else if (angle < 247.5)
            direction = "Down Left";
        else if (angle < 292.5)
            direction = "Down";
        else if (angle < 337.5)
            direction = "Down Right";
        else
            Debug.Log(angle);

        if (direction != null)
            return direction;

        return "Down";
    }


    private static Directions currentDirection = Directions.Right;
    private const float HYSTERESIS = 5f;  // Adjust this value as needed

    public static Directions GetAimDirection(float angle)
    {
        // Normalize angle to 0-360
        angle = (angle + 360) % 360;

        // Get the angle thresholds for the current direction
        float currentLowerThreshold = GetLowerThreshold(currentDirection);
        float currentUpperThreshold = GetUpperThreshold(currentDirection);

        // Add hysteresis to create a "sticky" zone around current direction
        float hysteresisLower = (currentLowerThreshold - HYSTERESIS + 360) % 360;
        float hysteresisUpper = (currentUpperThreshold + HYSTERESIS) % 360;

        // Check if we're still within the hysteresis zone of current direction
        if (IsAngleInRange(angle, hysteresisLower, hysteresisUpper))
        {
            return currentDirection;
        }

        // If we're outside the hysteresis zone, switch to new direction
        currentDirection = GetDirectionFromAngle(angle);
        return currentDirection;
    }

    private static bool IsAngleInRange(float angle, float lower, float upper)
    {
        // Handle wrap-around at 360 degrees
        if (lower > upper)
        {
            return angle >= lower || angle <= upper;
        }
        return angle >= lower && angle <= upper;
    }

    private static float GetLowerThreshold(Directions direction)
    {
        switch (direction)
        {
            case Directions.Right: return 337.5f;
            case Directions.UpRight: return 22.5f;
            case Directions.Up: return 67.5f;
            case Directions.UpLeft: return 112.5f;
            case Directions.Left: return 157.5f;
            case Directions.DownLeft: return 202.5f;
            case Directions.Down: return 247.5f;
            case Directions.DownRight: return 292.5f;
            default: return 337.5f;
        }
    }

    private static float GetUpperThreshold(Directions direction)
    {
        switch (direction)
        {
            case Directions.Right: return 22.5f;
            case Directions.UpRight: return 67.5f;
            case Directions.Up: return 112.5f;
            case Directions.UpLeft: return 157.5f;
            case Directions.Left: return 202.5f;
            case Directions.DownLeft: return 247.5f;
            case Directions.Down: return 292.5f;
            case Directions.DownRight: return 337.5f;
            default: return 22.5f;
        }
    }

    private static Directions GetDirectionFromAngle(float angle)
    {
        if (angle <= 22.5f || angle > 337.5f) return Directions.Right;
        else if (angle <= 67.5f) return Directions.UpRight;
        else if (angle <= 112.5f) return Directions.Up;
        else if (angle <= 157.5f) return Directions.UpLeft;
        else if (angle <= 202.5f) return Directions.Left;
        else if (angle <= 247.5f) return Directions.DownLeft;
        else if (angle <= 292.5f) return Directions.Down;
        else return Directions.DownRight;
    }





    /*
        public static Directions GetAimDirection(float angle)
        {
            // Normalize to 0-360
            angle = (angle + 360) % 360;

            // Now we can use simpler comparisons
            if (angle <= 22.5f || angle > 337.5f)
            {
                return Directions.Right;
            }
            else if (angle <= 67.5f)
            {
                return Directions.UpRight;
            }
            else if (angle <= 112.5f)
            {
                return Directions.Up;
            }
            else if (angle <= 157.5f)
            {
                return Directions.UpLeft;
            }
            else if (angle <= 202.5f)
            {
                return Directions.Left;
            }
            else if (angle <= 247.5f)
            {
                return Directions.DownLeft;
            }
            else if (angle <= 292.5f)
            {
                return Directions.Down;
            }
            else if (angle <= 337.5f)
            {
                return Directions.DownRight;
            }

            return Directions.Right; // Fallback, though we shouldn't reach this
        }
    */


}