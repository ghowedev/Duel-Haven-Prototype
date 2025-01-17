using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementProto : MonoBehaviour
{
    public Rigidbody2D rb;
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
    private Directions currentAimDirection = Directions.Down;
    [SerializeField]
    private Transform firePointPivotPoint;
    public bool movementEnabled = true;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
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

        // Previous input and direction values
        previousMoveDirectionSmoothed = currentMoveDirectionSmoothed;
        previousMoveDirectionRaw = currentMoveDirectionRaw;
        previousInputDirection = currentInputDirection;
        previousIsMoving = isMoving;
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
                case Directions.DownRight:
                    firePointPivotPoint.eulerAngles = new Vector3(0f, 0f, 45f);
                    break;
                case Directions.DownLeft:
                    firePointPivotPoint.eulerAngles = new Vector3(0f, 0f, 315f);
                    break;
            }


        }
    }
}
