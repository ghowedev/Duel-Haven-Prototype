using UnityEngine;

public class HitboxVisualizer : MonoBehaviour
{
    private static Sprite squareSprite;
    private static Sprite circleSprite;

    private Collider2D hitboxCollider;
    private SpriteRenderer visualSprite;

    private void Awake()
    {
        // Get the collider
        hitboxCollider = GetComponent<Collider2D>();
        if (hitboxCollider == null)
        {
            Debug.LogWarning("HitboxVisualizer requires a Collider2D!");
            enabled = false;
            return;
        }

        // Create sprites if they don't exist
        if (squareSprite == null || circleSprite == null)
        {
            CreateSprites();
        }

        // Setup sprite renderer
        visualSprite = gameObject.AddComponent<SpriteRenderer>();
        visualSprite.color = new Color(1f, 0f, 0f, 0.3f); // Semi-transparent red
        visualSprite.sortingOrder = 100; // Render on top of most things

        // Assign appropriate sprite based on collider type
        if (hitboxCollider is CircleCollider2D)
        {
            visualSprite.sprite = circleSprite;
        }
        else if (hitboxCollider is BoxCollider2D)
        {
            visualSprite.sprite = squareSprite;
        }

        UpdateVisualizer();
    }

    private void CreateSprites()
    {
        // Create square sprite
        Texture2D squareTexture = new Texture2D(64, 64);
        Color[] colors = new Color[64 * 64];
        for (int i = 0; i < colors.Length; i++)
        {
            colors[i] = Color.white;
        }
        squareTexture.SetPixels(colors);
        squareTexture.Apply();
        squareSprite = Sprite.Create(squareTexture, new Rect(0, 0, 64, 64), new Vector2(0.5f, 0.5f), 64);

        // Create circle sprite
        Texture2D circleTexture = new Texture2D(64, 64);
        colors = new Color[64 * 64];
        for (int y = 0; y < 64; y++)
        {
            for (int x = 0; x < 64; x++)
            {
                float distanceFromCenter = Vector2.Distance(new Vector2(x, y), new Vector2(31.5f, 31.5f));
                colors[y * 64 + x] = distanceFromCenter <= 31.5f ? Color.white : Color.clear;
            }
        }
        circleTexture.SetPixels(colors);
        circleTexture.Apply();
        circleSprite = Sprite.Create(circleTexture, new Rect(0, 0, 64, 64), new Vector2(0.5f, 0.5f), 64);
    }

    private void UpdateVisualizer()
    {
        if (hitboxCollider is CircleCollider2D circleCollider)
        {
            float diameter = circleCollider.radius * 2;
            transform.localScale = new Vector3(diameter, diameter, 1);
            visualSprite.transform.localPosition = circleCollider.offset;
        }
        else if (hitboxCollider is BoxCollider2D boxCollider)
        {
            transform.localScale = new Vector3(boxCollider.size.x, boxCollider.size.y, 1);
            visualSprite.transform.localPosition = boxCollider.offset;
        }

        // Only show sprite if collider is enabled
        visualSprite.enabled = hitboxCollider.enabled;
    }

    private void Update()
    {
        // Update transform and visibility if collider changes
        UpdateVisualizer();
    }

    // Public method to force visibility regardless of collider state
    public void SetVisibility(bool visible)
    {
        visualSprite.enabled = visible;
    }

    // Public method to toggle visibility based on collider state
    public void SetAutoVisibility(bool autoVisibility)
    {
        if (autoVisibility)
        {
            UpdateVisualizer(); // Will sync with collider.enabled
        }
    }
}