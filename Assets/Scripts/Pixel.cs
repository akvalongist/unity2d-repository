using UnityEngine;

public class Pixel
{
    public PixelType Type;
    public Color Color;

    public Pixel(PixelType type, Color color)
    {
        Type = type;
        Color = color;
    }
}
