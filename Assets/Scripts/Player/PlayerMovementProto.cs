using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovementProto : MonoBehaviour
{
    public Text debugText;
    public Rigidbody2D rb;
    public Animator animator;

    public float MOVESPEED = 5f;
    private Vector2 currentMoveDirectionSmoothed = Vector2.zero;
    private Vector2 currentMoveDirectionRaw = Vector2.zero;
    private Vector2 previousMoveDirectionSmoothed = Vector2.zero;
    private Vector2 previousMoveDirectionRaw = Vector2.zero;

    private float currentInputAngle;
    private string currentInputDirection;
    private string previousInputDirection;

    private bool isMoving;
    private bool previousIsMoving;
    private Directions currentPlayerAimDirection = Directions.Down;
    private Directions currentMouseAimDirection = Directions.Down;

    public bool movementEnabled = true;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
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
        bool switchedAimDirections = (currentMouseAimDirection != currentPlayerAimDirection);
        bool switchedToMoving = !previousIsMoving && isMoving;
        bool switchedToIdle = previousIsMoving && !isMoving;

        // Set animations if switched directions or switched to idle
        if (isMoving || switchedToMoving || (!isMoving && switchedToIdle))
        {
            animator.SetBool("Moving", isMoving);
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);
            animator.SetBool("UpLeft", false);
            animator.SetBool("UpRight", false);
            animator.SetBool("DownLeft", false);
            animator.SetBool("DownRight", false);
            animator.SetBool(currentMouseAimDirection.ToString(), true);
        }

    }

    void HandleAim()
    {
        Vector3 mousePosition = HelperUtilities.GetMousePlayerPosition(this.transform);

        float angle = HelperUtilities.GetAngleFromVector(mousePosition);
        currentMouseAimDirection = HelperUtilities.GetAimDirection(angle);

        debugText.text = angle.ToString();


        if (currentPlayerAimDirection != currentMouseAimDirection)
        {
            currentPlayerAimDirection = currentMouseAimDirection;
        }
    }
}
