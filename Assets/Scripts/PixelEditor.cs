using UnityEngine;

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
        if (Input.GetMouseButton(0))
            PlacePixel(PixelType.Solid, Color.gray);
        if (Input.GetMouseButton(1))
            PlacePixel(PixelType.Water, Color.blue);
        if (Input.GetMouseButton(2))
            PlacePixel(PixelType.Fire, Color.red);
    }

    void PlacePixel(PixelType type, Color color)
    {
        Vector3 world = cam.ScreenToWorldPoint(Input.mousePosition);
        int x = Mathf.FloorToInt(simulation.width / 2 + world.x / simulation.pixelSize);
        int y = Mathf.FloorToInt(simulation.height / 2 + world.y / simulation.pixelSize);
        simulation.SetPixel(x, y, type, color);
    }
}
