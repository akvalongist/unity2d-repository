# Unity 2D Project Example

This repository contains a small Unity 2D example that builds the entire scene using code.

Scripts located in `Assets/Scripts` create the player, ground, and camera at runtime. Open the project in Unity and press Play to see the automatically generated scene.

## Pixel Simulation

The project now includes a very simple 2D pixel simulation inspired by **Noita**. Pixels can be placed using the mouse:

* **Left click** – place solid pixels
* **Right click** – place water pixels
* **Middle click** – place fire pixels

`PixelSimulation` manages a grid of pixels and applies basic gravity and interactions for water, acid, and fire. `PixelEditor` allows modifying the pixel grid at runtime.
