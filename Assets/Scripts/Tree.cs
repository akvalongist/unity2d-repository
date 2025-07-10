using UnityEngine;

public class Tree : MonoBehaviour
{
    [Range(0f, 100f)]
    public float progress = 0f; // 0 - no damage, 100 - fully chopped

    public Sprite normalSprite; // sprite without cracks
    public Sprite crackedSprite; // sprite with cracks appearing at 25%
    public GameObject logPrefab; // prefab to spawn when tree is destroyed

    private SpriteRenderer spriteRenderer;
    private bool cracked = false;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogWarning("Tree requires a SpriteRenderer component.");
        }
        if (normalSprite == null && spriteRenderer != null)
        {
            normalSprite = spriteRenderer.sprite;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // example input
        {
            AddProgress(25f);
        }
    }

    // Call this method to apply damage to the tree
    public void AddProgress(float amount)
    {
        if (progress >= 100f)
            return;

        progress += amount;
        progress = Mathf.Clamp(progress, 0f, 100f);

        if (!cracked && progress >= 25f && crackedSprite != null)
        {
            cracked = true;
            spriteRenderer.sprite = crackedSprite;
        }

        if (progress >= 100f)
        {
            SpawnLog();
        }
    }

    void SpawnLog()
    {
        if (logPrefab != null)
        {
            Instantiate(logPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
