using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementProto : MonoBehaviour
{
    public Text debugText;
    public Rigidbody2D rb;
    public Animator animator;
    public StateManager stateManager;
    public WeaponSocket WeaponSocket;

    public float MOVESPEED = 5f;
    private Vector2 currentMoveDirectionSmoothed = Vector2.zero;
    private Vector2 currentMoveDirectionRaw = Vector2.zero;
    private Vector2 previousMoveDirectionSmoothed = Vector2.zero;
    private Vector2 previousMoveDirectionRaw = Vector2.zero;

    private float currentInputAngle;
    private int currentInputDirection;
    private int previousInputDirection;
    private bool isMoving;
    private bool previousIsMoving;

    private int currentPlayerAimDirection = 2;
    private int currentMouseAimDirection = 2;

    private const float HYSTERESIS = 5f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stateManager = GetComponent<StateManager>();

        animator.SetInteger("Direction", 2);
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleAim();
    }

    void FixedUpdate()
    {
        if (!stateManager.CanMove) return;

        MovePlayer(currentMoveDirectionSmoothed, MOVESPEED);
    }

    public void MovePlayer(Vector2 direction, float speed)
    {

        rb.linearVelocity = direction * speed;
    }

    void HandleMovement()
    {
        if (!stateManager.CanMove)
        {
            TurnOffMoveVariables();
            return;
        }

        float isMovingThreshold = 0.8f;

        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        float xInputRaw = Input.GetAxisRaw("Horizontal");
        float yInputRaw = Input.GetAxisRaw("Vertical");

        // Current input values
        Vector2 inputDirection = new Vector2(xInput, yInput);
        Vector2 inputDirectionRaw = new Vector2(xInputRaw, yInputRaw);
        currentMoveDirectionSmoothed = (inputDirection.magnitude > 1 ? inputDirection.normalized : inputDirection);
        currentMoveDirectionRaw = (inputDirectionRaw.magnitude > 1 ? inputDirectionRaw.normalized : inputDirectionRaw);

        // Current input direction     
        currentInputAngle = Mathf.Atan2(yInput, xInput) * Mathf.Rad2Deg;
        currentInputDirection = GetNearestDirectionFromAngleInt(currentInputAngle);

        // Is moving or not
        isMoving = (currentMoveDirectionSmoothed.sqrMagnitude > (isMovingThreshold * isMovingThreshold));

        HandleMovementIdleAnimation();

        // Previous input and direction values
        previousMoveDirectionSmoothed = currentMoveDirectionSmoothed;
        previousMoveDirectionRaw = currentMoveDirectionRaw;
        previousInputDirection = currentInputDirection;
        previousIsMoving = isMoving;
    }

    void HandleMovementIdleAnimation()
    {
        bool switchedAimDirections = (currentMouseAimDirection != currentPlayerAimDirection);
        bool switchedToMoving = !previousIsMoving && isMoving;
        bool switchedToIdle = previousIsMoving && !isMoving;

        // Set animations if switched directions or switched to idle
        if (isMoving || switchedToMoving || (!isMoving && switchedToIdle))
        {
            animator.SetBool("Moving", isMoving);
            animator.SetInteger("Direction", currentMouseAimDirection);
            animator.SetTrigger("WeaponBobReset");
            WeaponSocket.UpdateSocketPosition(currentMouseAimDirection);
        }

    }

    void HandleAim()
    {
        Vector3 mousePosition = HelperUtilities.GetMousePlayerPosition(this.transform);

        float angle = HelperUtilities.GetAngleFromVector(mousePosition);

        currentMouseAimDirection = GetAimDirection(angle, currentMouseAimDirection);

        if (currentPlayerAimDirection != currentMouseAimDirection)
        {
            currentPlayerAimDirection = currentMouseAimDirection;
        }
    }

    void TurnOffMoveVariables()
    {
        currentMoveDirectionRaw = Vector2.zero;
        currentMoveDirectionSmoothed = Vector2.zero;
        isMoving = false;
        animator.SetBool("Moving", false);
        return;
    }
    private int GetAimDirection(float angle, int direction)
    {
        // Normalize angle to 0-360
        angle = (angle + 360) % 360;

        // Get the angle thresholds for the current direction
        float currentLowerThreshold = GetLowerThreshold(direction);
        float currentUpperThreshold = GetUpperThreshold(direction);

        // Add hysteresis to create a "sticky" zone around current direction
        float hysteresisLower = (currentLowerThreshold - HYSTERESIS + 360) % 360;
        float hysteresisUpper = (currentUpperThreshold + HYSTERESIS) % 360;

        // Check if we're still within the hysteresis zone of current direction
        if (IsAngleInRange(angle, hysteresisLower, hysteresisUpper))
        {
            return direction;
        }

        // If we're outside the hysteresis zone, switch to new direction
        direction = GetDirectionFromAngle(angle);
        return direction;
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

    private static float GetLowerThreshold(int direction)
    {
        switch (direction)
        {
            case 6: return 337.5f; //right
            case 9: return 22.5f; //upright
            case 8: return 67.5f; //up
            case 7: return 112.5f; //upleft
            case 4: return 157.5f; //left
            case 1: return 202.5f; //downleft
            case 2: return 247.5f; //down
            case 3: return 292.5f; //downright
            default: return 337.5f;
        }
    }

    private static float GetUpperThreshold(int direction)
    {
        switch (direction)
        {
            case 6: return 22.5f; //right
            case 9: return 67.5f; //upright
            case 8: return 112.5f; //up
            case 7: return 157.5f; //upleft
            case 4: return 202.5f; //left
            case 1: return 247.5f; //downleft
            case 2: return 292.5f; //down
            case 3: return 337.5f; //downright
            default: return 22.5f;
        }
    }

    private static int GetDirectionFromAngle(float angle)
    {
        if (angle <= 22.5f || angle > 337.5f) return 6; //right
        else if (angle <= 67.5f) return 9; //upright
        else if (angle <= 112.5f) return 8; //up
        else if (angle <= 157.5f) return 7; //upleft
        else if (angle <= 202.5f) return 4; //left
        else if (angle <= 247.5f) return 1; //downleft
        else if (angle <= 292.5f) return 2; //down
        else return 3;
    }

    public static int GetNearestDirectionFromAngleInt(float angle)
    {
        int direction = 2;

        angle = (angle + 360) % 360;

        if (angle < 22.5 || angle >= 337.5)
            // direction = "Right";
            direction = 6;
        else if (angle < 67.5)
            // direction = "Up Right";
            direction = 7;
        else if (angle < 112.5)
            // direction = "Up";
            direction = 8;
        else if (angle < 157.5)
            // direction = "Up Left";
            direction = 7;
        else if (angle < 202.5)
            // direction = "Left";
            direction = 4;
        else if (angle < 247.5)
            // direction = "Down Left";
            direction = 1;
        else if (angle < 292.5)
            // direction = "Down";
            direction = 2;
        else if (angle < 337.5)
            // direction = "Down Right";
            direction = 3;
        else
            Debug.Log(angle);

        return direction;
    }

    public static float GetAngleFromVector(Vector3 vector)
    {
        float radians = Mathf.Atan2(vector.y, vector.x);

        float degrees = radians * Mathf.Rad2Deg;

        return degrees;

    }

}