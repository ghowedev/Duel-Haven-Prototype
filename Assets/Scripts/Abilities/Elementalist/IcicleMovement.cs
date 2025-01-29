using UnityEngine;

public class IcicleMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float horizontalRadius = 3f;
    [SerializeField] private float verticalRadius = 2f;
    [SerializeField] private float rotationSpeed = 2f;

    private float currentAngle = 0f;
    private SpriteRenderer spriteRenderer;
    private TrailRenderer trailRenderer;
    private bool isInFront = true;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        trailRenderer = GetComponentInChildren<TrailRenderer>();
    }

    private void Update()
    {
        // Update angle (negative for clockwise)
        currentAngle -= rotationSpeed * Time.deltaTime;

        // Calculate new position using different radii for x and y
        float x = horizontalRadius * Mathf.Cos(currentAngle);
        float y = verticalRadius * Mathf.Sin(currentAngle);

        // Set position in world space, offset from player position
        transform.position = playerTransform.position + new Vector3(x, y, 0);

        // Check horizontal position using cosine value
        float cosValue = Mathf.Cos(currentAngle);

        // Leftmost point (cos = -1)
        if (cosValue < -0.99f && isInFront)
        {
            spriteRenderer.sortingLayerName = "Behind Player";
            trailRenderer.sortingLayerName = "Behind Player";
            isInFront = false;
        }
        // Rightmost point (cos = 1)
        else if (cosValue > 0.99f && !isInFront)
        {
            spriteRenderer.sortingLayerName = "Front of Player";
            trailRenderer.sortingLayerName = "Front of Player";
            isInFront = true;
        }
    }
}