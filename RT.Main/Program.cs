// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RT.Main;
using RT.Material;
using RT.Math.LinearAlgebra;
using RT.Primitives.Primitive;
using RT.Render;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var provider = RenderSetUp.SetUpDi(config);

var world = RenderSetUp.SetUpWorld(provider);
var camera = RenderSetUp.SetUpCamera();

var glassSphere = new Sphere(new Point3(1f, 0.5f, -0.9f), 0.5f)
{
    Material = new MirrorMaterial(
        new Vector3(1f, 0.5f, 0.5f))
};
var lambertSphere = new Sphere(new Point3(1f, 1.5f, -0.9f), 0.5f)
{
    Material = new LambertMaterial(new Vector3(1f, 0f, 0f))
};

var lambertPlane = new Plane(-Vector3.Forward, new Point3(0, -10, 0))
{
    Material = new LambertMaterial(new Vector3(0f, 0.6f, 0.5f)) 
};

world.Traceables.Add(glassSphere);
world.Traceables.Add(lambertSphere);
world.Traceables.Add(lambertPlane);

var renderer = provider.GetService<IRenderer>()!;

renderer.Render(world, camera);