using UnityEngine;
using UnityEngine.InputSystem;

public class PixelEditor : MonoBehaviour
{
    public PixelSimulation simulation;
    public Camera cam;

    void Awake()
    {
        if (cam == null)
            cam = Camera.main;
    }

    void Update()
    {
        if (simulation == null) return;
        if (Mouse.current.leftButton.isPressed)
            PlacePixel(PixelType.Solid, Color.gray);
        if (Mouse.current.rightButton.isPressed)
            PlacePixel(PixelType.Water, Color.blue);
        if (Mouse.current.middleButton.isPressed)
            PlacePixel(PixelType.Fire, Color.red);
    }

    void PlacePixel(PixelType type, Color color)
    {
        Vector3 world = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        int x = Mathf.FloorToInt(simulation.width / 2 + world.x / simulation.pixelSize);
        int y = Mathf.FloorToInt(simulation.height / 2 + world.y / simulation.pixelSize);
        simulation.SetPixel(x, y, type, color);
    }
}
