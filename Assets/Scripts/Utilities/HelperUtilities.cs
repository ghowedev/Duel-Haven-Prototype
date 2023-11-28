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

    public static AimDirection GetAimDirection(float angle)
    {        
        AimDirection aimDirection;
        
        //Up Right
        if (angle >= 22f && angle <= 67f)
        {
            aimDirection = AimDirection.UpRight;
        }
        // Up
        else if (angle > 67f && angle <= 112f)
        {
            aimDirection = AimDirection.Up;
        }
        // Up Left
        else if (angle > 112f && angle <= 158f)
        {
            aimDirection = AimDirection.UpLeft;
        }
        // Left
        else if ((angle <= 180f && angle > 158f) || (angle > -180 && angle <= -135f))
        {
            aimDirection = AimDirection.Left;
        }
        // Down
        else if ((angle > -135f && angle <= -45f))
        {
            aimDirection = AimDirection.Down;
        }
        // Right
        else if ((angle > -45f && angle <= 0f) || (angle > 0 && angle < 22f))
        {
            aimDirection = AimDirection.Right;
        }
        else
        {
            aimDirection = AimDirection.Right;
        }
        
    return aimDirection;
    
    }
    
    
}
