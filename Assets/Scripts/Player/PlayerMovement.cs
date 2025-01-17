using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Text debugText;
    public float MOVESPEED = 5f;

    public Rigidbody2D rb;
    public Animator animator;

    private Vector2 currentMoveDirectionSmoothed = Vector2.zero;
    private Vector2 currentMoveDirectionRaw = Vector2.zero;
    private Vector2 previousMoveDirectionSmoothed = Vector2.zero;
    private Vector2 previousMoveDirectionRaw = Vector2.zero;

    private float currentInputAngle;
    private string currentInputDirection;
    private string previousInputDirection;

    private bool isMoving;
    private bool previousIsMoving;

    private Directions currentAimDirection = Directions.Down;
    [SerializeField]
    private Transform firePointPivotPoint;

    //FLAGS
    public bool movementEnabled = true;



    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovement();
        HandleAim();
    }

    void FixedUpdate()
    {
        if (!movementEnabled) return;

        MovePlayer(currentMoveDirectionSmoothed, MOVESPEED);
    }

    public void MovePlayer(Vector2 direction, float speed)
    {

        rb.velocity = direction * speed;
    }

    void HandleMovement()
    {
        if (!movementEnabled) return;

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
        currentInputDirection = HelperUtilities.GetNearestDirectionFromAngle(currentInputAngle);

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
        bool switchedDirections = (previousInputDirection != currentInputDirection);
        bool switchedToMoving = !previousIsMoving && isMoving;
        bool switchedToIdle = previousIsMoving && !isMoving;

        // Set animations if switched directions or switched to idle
        if ((isMoving && (switchedDirections || switchedToMoving)) || (!isMoving && switchedToIdle))
        {
            animator.SetBool("Moving", isMoving);
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
            animator.SetBool("Up Left", false);
            animator.SetBool("Up Right", false);
            animator.SetBool("Down Left", false);
            animator.SetBool("Down Right", false);
            animator.SetBool(currentInputDirection, true);
        }
    }

    void HandleAim()
    {
        Vector3 mousePosition = HelperUtilities.GetMousePlayerPosition(this.transform);

        float angle = HelperUtilities.GetAngleFromVector(mousePosition);
        Directions currentMouseAimDirection = HelperUtilities.GetAimDirection(angle);

        if (currentAimDirection != currentMouseAimDirection)
        {
            currentAimDirection = currentMouseAimDirection;


            switch (currentAimDirection)
            {
                case Directions.Right:
                    firePointPivotPoint.eulerAngles = new Vector3(0f, 0f, 90f);
                    break;
                case Directions.Down:
                    firePointPivotPoint.eulerAngles = new Vector3(0f, 0f, 0f);
                    break;
                case Directions.Left:
                    firePointPivotPoint.eulerAngles = new Vector3(0f, 0f, 270f);
                    break;
                case Directions.Up:
                    firePointPivotPoint.eulerAngles = new Vector3(0f, 0f, 180f);
                    break;
                case Directions.UpRight:
                    firePointPivotPoint.eulerAngles = new Vector3(0f, 0f, 135f);
                    break;
                case Directions.UpLeft:
                    firePointPivotPoint.eulerAngles = new Vector3(0f, 0f, 225f);
                    break;
            }

        }
    }


}


/*
if (previousMoveDirectionRaw != currentMoveDirectionRaw && isMoving)
{
    string movingDirectionParameter;

    animator.SetBool("Moving", isMoving);
    animator.SetBool("Moving Up", false);
    animator.SetBool("Moving Down", false);
    animator.SetBool("Moving Left", false);
    animator.SetBool("Moving Right", false);

    if (Mathf.Abs(currentMoveDirectionSmoothed.x) > 0)
    {
        movingDirectionParameter = currentMoveDirectionSmoothed.x > 0 ? "Moving Right" : "Moving Left";
        animator.SetBool(movingDirectionParameter, true);
    }
    if (Mathf.Abs(currentMoveDirectionSmoothed.y) > 0)
    {
        movingDirectionParameter = currentMoveDirectionSmoothed.x > 0 ? "Moving Up" : "Moving Down";
        animator.SetBool(movingDirectionParameter, true);
    }
}

// handle idle direction
if (currentMoveDirectionRaw == Vector2.zero && previousMoveDirectionRaw != Vector2.zero && !isMoving)
{
    animator.SetBool("Idle Up", false);
    animator.SetBool("Idle Down", false);
    animator.SetBool("Idle Left", false);
    animator.SetBool("Idle Right", false);

    string idleDirectionParameter;

    if (Mathf.Abs(previousMoveDirectionSmoothed.x) > threshold)
    {
        idleDirectionParameter = previousMoveDirectionSmoothed.x > 0 ? "Idle Right" : "Idle Left";
        animator.SetBool(idleDirectionParameter, true);
    }

    if (Mathf.Abs(previousMoveDirectionSmoothed.y) > threshold)
    {
        idleDirectionParameter = previousMoveDirectionSmoothed.y > 0 ? "Idle Up" : "Idle Down";
        animator.SetBool(idleDirectionParameter, true);
    }
}
*/