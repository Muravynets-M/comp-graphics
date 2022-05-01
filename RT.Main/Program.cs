// See https://aka.ms/new-console-template for more information

using RT.Math.LinearAlgebra;
using RT.Primitives;
using RT.Render;
using RT.Render.RenderOutput;
using RT.Render.RenderOutput.HitResultAdapter;
using RT.Render.RenderOutput.ImageWriter;

var world = new World();
world.Traceables.Add(new Sphere(
    new Point3(0.0f, 0f, -1f),
    0.5f)
);

world.Lights.Add(new Sphere(
    (Point3) Vector3.Unit(new Vector3(-0.5f, 0.5f, -0f)),
    0.5f
));

var camera = new Camera(
    new Point3(0f, 0f, 0f),
    new Vector3(0f, 0f, -1f),
    2f,
    2f
);

var adapter = new ColorlessPpmHitResultAdapter(world, new Vector3(138 / 255f, 43 / 255f, 226 / 255f),
    new Vector3(255 / 255f, 192 / 255f, 203 / 255f)
);
var buffer = new PpmImageBuffer(600, 600, "sphere");
//var buffer = new ConsoleImageBuffer(10, 10);

var renderer = new Renderer(world, buffer, adapter);

renderer.Render(camera);