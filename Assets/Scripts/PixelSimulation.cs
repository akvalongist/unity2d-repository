using UnityEngine;

public class PixelSimulation : MonoBehaviour
{
    public int width = 64;
    public int height = 64;
    public float pixelSize = 0.1f;
    public Pixel[,] pixels;
    private Texture2D tex;
    private SpriteRenderer sr;

    void Awake()
    {
        sr = gameObject.AddComponent<SpriteRenderer>();
        tex = new Texture2D(width, height) { filterMode = FilterMode.Point };
        sr.sprite = Sprite.Create(tex, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f), 1 / pixelSize);
        pixels = new Pixel[width, height];
        for (int x = 0; x < width; x++)
            for (int y = 0; y < height; y++)
                pixels[x, y] = new Pixel(PixelType.Empty, Color.black);
        Render();
    }

    void Update()
    {
        Simulate();
        Render();
    }

    public void SetPixel(int x, int y, PixelType type, Color color)
    {
        if (x < 0 || x >= width || y < 0 || y >= height) return;
        pixels[x, y].Type = type;
        pixels[x, y].Color = color;
    }

    void SwapPixels(int x1, int y1, int x2, int y2)
    {
        var temp = pixels[x1, y1];
        pixels[x1, y1] = pixels[x2, y2];
        pixels[x2, y2] = temp;
    }

    void Simulate()
    {
        for (int y = 1; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                var p = pixels[x, y];
                if (p.Type == PixelType.Water)
                {
                    if (pixels[x, y - 1].Type == PixelType.Empty)
                        SwapPixels(x, y, x, y - 1);
                }
                else if (p.Type == PixelType.Acid)
                {
                    if (pixels[x, y - 1].Type == PixelType.Empty)
                        SwapPixels(x, y, x, y - 1);
                    else if (pixels[x, y - 1].Type == PixelType.Solid)
                        SetPixel(x, y - 1, PixelType.Empty, Color.black);
                }
                else if (p.Type == PixelType.Fire)
                {
                    if (pixels[x, y - 1].Type == PixelType.Empty)
                        SwapPixels(x, y, x, y - 1);
                    if (Random.value < 0.01f)
                        SetPixel(x, y, PixelType.Empty, Color.black);
                }
            }
        }
    }

    void Render()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                tex.SetPixel(x, y, pixels[x, y].Color);
            }
        }
        tex.Apply();
    }
}
