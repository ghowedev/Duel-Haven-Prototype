using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Animator animator;
    private Vector2 currentMoveDirectionSmoothed = Vector2.zero;
    private Vector2 currentMoveDirectionRaw = Vector2.zero;
    private Vector2 currentMoveDirectionNormalized = Vector2.zero;
    private Vector2 previousMoveDirectionSmoothed = Vector2.zero;
    private Vector2 previousMoveDirectionRaw = Vector2.zero;
    private Vector2 previousMoveDirectionNormalized = Vector2.zero;
    private Vector2 idleDirection = Vector2.zero;
    private Vector2 lastMoveDirection;
    private AimDirection currentAimDirection = AimDirection.Down;
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
        if (!movementEnabled)
        {
            rb.velocity = Vector2.zero;
            return;
        }
        rb.velocity = currentMoveDirectionRaw * moveSpeed;
    }

    void HandleMovement()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");
        float xInputRaw = Input.GetAxisRaw("Horizontal");
        float yInputRaw = Input.GetAxisRaw("Vertical");


        currentMoveDirectionSmoothed = new Vector2(xInput, yInput);
        currentMoveDirectionRaw = new Vector2(xInputRaw, yInputRaw).normalized;
        currentMoveDirectionNormalized = currentMoveDirectionSmoothed.normalized;

        SetMovementAnimations();

        previousMoveDirectionSmoothed = new Vector2(xInput, yInput);
        previousMoveDirectionRaw = new Vector2(xInputRaw, yInputRaw);
        previousMoveDirectionNormalized = previousMoveDirectionSmoothed;
    }

    void SetMovementAnimations()
    {
        animator.SetFloat("Horizontal", currentMoveDirectionNormalized.x);
        animator.SetFloat("Vertical", currentMoveDirectionNormalized.y);
        animator.SetFloat("Speed", currentMoveDirectionRaw.magnitude);

        if (currentMoveDirectionRaw == Vector2.zero && previousMoveDirectionRaw != Vector2.zero)
        {

            // check if diagonal and set to its cardinal diagonal
            if (previousMoveDirectionSmoothed.x != 0 && previousMoveDirectionSmoothed.y != 0)
            {
                idleDirection.x = previousMoveDirectionSmoothed.x > 0 ? 1 : -1;
                idleDirection.y = previousMoveDirectionSmoothed.y > 0 ? 1 : -1;
            }
            else if (previousMoveDirectionNormalized.x != 0)
            {
                idleDirection.x = previousMoveDirectionSmoothed.x > 0 ? 1 : -1;
                idleDirection.y = 0;
            }
            else if (previousMoveDirectionNormalized.y != 0)
            {
                idleDirection.y = previousMoveDirectionSmoothed.y > 0 ? 1 : -1;
                idleDirection.x = 0;
            }

            animator.SetFloat("AnimLastMoveX", idleDirection.x);
            animator.SetFloat("AnimLastMoveY", idleDirection.y);
        }

        int directionFloat = 0;
        if (lastMoveDirection.x == 0 && lastMoveDirection.y > 0) directionFloat = 0;
        if (lastMoveDirection.x > 0 && lastMoveDirection.y > 0) directionFloat = 1;
        if (lastMoveDirection.x > 0 && lastMoveDirection.y == 0) directionFloat = 2;
        if (lastMoveDirection.x == 0 && lastMoveDirection.y < 0) directionFloat = 3;
        if (lastMoveDirection.x < 0 && lastMoveDirection.y == 0) directionFloat = 4;
        if (lastMoveDirection.x < 0 && lastMoveDirection.y > 0) directionFloat = 5;

    }

    void HandleAim()
    {
        Vector3 mousePosition = HelperUtilities.GetMouseWorldPosition();
        float angle = HelperUtilities.GetAngleFromVector(mousePosition);
        AimDirection currentMouseAimDirection = HelperUtilities.GetAimDirection(angle);

        if (currentAimDirection != currentMouseAimDirection)
        {
            currentAimDirection = currentMouseAimDirection;


            switch (currentAimDirection)
            {
                case AimDirection.Right:
                    firePointPivotPoint.eulerAngles = new Vector3(0f, 0f, 90f);
                    break;
                case AimDirection.Down:
                    firePointPivotPoint.eulerAngles = new Vector3(0f, 0f, 0f);
                    break;
                case AimDirection.Left:
                    firePointPivotPoint.eulerAngles = new Vector3(0f, 0f, 270f);
                    break;
                case AimDirection.Up:
                    firePointPivotPoint.eulerAngles = new Vector3(0f, 0f, 180f);
                    break;
                case AimDirection.UpRight:
                    firePointPivotPoint.eulerAngles = new Vector3(0f, 0f, 135f);
                    break;
                case AimDirection.UpLeft:
                    firePointPivotPoint.eulerAngles = new Vector3(0f, 0f, 225f);
                    break;
            }

        }
    }


}