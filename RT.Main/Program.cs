// See https://aka.ms/new-console-template for more information

using RT.Math.LinearAlgebra;
using RT.Primitives;
using RT.Render;
using RT.Render.RenderOutput;

var world = new World();
world.Place(new Sphere(
    new Point3(0.0f, 0f, -1f),
    0.5f)
);

world.Light = new Sphere(
    (Point3)Vector3.Unit(new Vector3(0.5f, 0.5f, 0f)),
    0.5f
);

var camera = new Camera(
    new Point3(0f, 0f, 0f),
    new Vector3(0f, 0f, -1f),
    2f,
    2f
);

var printer = new ConsolePrinter(120, 40);

var renderer = new Renderer(camera, world, printer);

renderer.Render();