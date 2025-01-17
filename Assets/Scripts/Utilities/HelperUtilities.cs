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

    public static Directions GetAimDirection(float angle)
    {
        Directions aimDirection;

        if (angle > -22.5f && angle <= 22.5f)
        {
            aimDirection = Directions.Right;
        }
        // Up-Right (Northeast): 22.5° to 67.5°
        else if (angle > 22.5f && angle <= 67.5f)
        {
            aimDirection = Directions.UpRight;
        }
        // Up (North): 67.5° to 112.5°
        else if (angle > 67.5f && angle <= 112.5f)
        {
            aimDirection = Directions.Up;
        }
        // Up-Left (Northwest): 112.5° to 157.5°
        else if (angle > 112.5f && angle <= 157.5f)
        {
            aimDirection = Directions.UpLeft;
        }
        // Left (West): 157.5° to 180° OR -180° to -157.5°
        else if (angle > 157.5f || angle <= -157.5f)
        {
            aimDirection = Directions.Left;
        }
        // Down-Left (Southwest): -157.5° to -112.5°
        else if (angle > -157.5f && angle <= -112.5f)
        {
            aimDirection = Directions.DownLeft;
        }
        // Down (South): -112.5° to -67.5°
        else if (angle > -112.5f && angle <= -67.5f)
        {
            aimDirection = Directions.Down;
        }
        // Down-Right (Southeast): -67.5° to -22.5°
        else if (angle > -67.5f && angle <= -22.5f)
        {
            aimDirection = Directions.DownRight;
        }
        else
        {
            aimDirection = Directions.Right;
        }

        return aimDirection;

    }


}
