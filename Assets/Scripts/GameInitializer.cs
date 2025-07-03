using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    static void Initialize()
    {
        var initializerObj = new GameObject("GameInitializer");
        DontDestroyOnLoad(initializerObj);
        initializerObj.AddComponent<GameInitializer>();
    }

    void Start()
    {
        SetupScene();
    }

    void SetupScene()
    {
        // Create camera if not exists
        if (Camera.main == null)
        {
            var camObj = new GameObject("MainCamera");
            camObj.tag = "MainCamera";
            camObj.AddComponent<Camera>();
            camObj.AddComponent<CameraFollow>();
            camObj.transform.position = new Vector3(0, 0, -10);
        }
        else
        {
            if (Camera.main.GetComponent<CameraFollow>() == null)
                Camera.main.gameObject.AddComponent<CameraFollow>();
        }

        // Create player
        var playerObj = new GameObject("Player");
        var spriteRenderer = playerObj.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = CreateSquareSprite(Color.green);
        playerObj.transform.position = new Vector3(0, 1, 0);

        var rb = playerObj.AddComponent<Rigidbody2D>();
        rb.gravityScale = 1;
        playerObj.AddComponent<BoxCollider2D>();
        playerObj.AddComponent<PlayerController>();

        // Create ground
        var groundObj = new GameObject("Ground");
        var groundRenderer = groundObj.AddComponent<SpriteRenderer>();
        groundRenderer.sprite = CreateSquareSprite(Color.gray);
        groundObj.transform.position = new Vector3(0, -1, 0);
        groundObj.transform.localScale = new Vector3(10, 1, 1);
        groundObj.AddComponent<BoxCollider2D>();
        groundObj.GetComponent<BoxCollider2D>().sharedMaterial = new PhysicsMaterial2D { friction = 0.5f };
        groundObj.AddComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;

        // Pixel simulation
        var simObj = new GameObject("PixelSimulation");
        var simulation = simObj.AddComponent<PixelSimulation>();
        simObj.transform.position = new Vector3(0, 0, 0);

        var editorObj = new GameObject("PixelEditor");
        var editor = editorObj.AddComponent<PixelEditor>();
        editor.simulation = simulation;
    }

    Sprite CreateSquareSprite(Color color)
    {
        var tex = new Texture2D(1, 1);
        tex.SetPixel(0, 0, color);
        tex.Apply();
        return Sprite.Create(tex, new Rect(0, 0, 1, 1), new Vector2(0.5f, 0.5f), 1);
    }
}
