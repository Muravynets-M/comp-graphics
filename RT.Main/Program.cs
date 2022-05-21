// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RT.Main;
using RT.Math;
using RT.Math.LinearAlgebra;
using RT.Render.Render;
using RT.Render.RenderInput;
using RT.Render.RenderInput.InMemorySetup;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var provider = RenderSetUp.SetUpDi(config);
var world = RenderSetUp.SetUpWorld();
var camera = RenderSetUp.SetUpCamera();
RenderSetUp.SetUpLights(world);

var renderer = provider.GetService<IRenderer>()!;
var input = provider.GetService<IRenderInput>()!;

world.Traceables.AddRange(input.GetWorldInput());

// world.Traceables.ForEach(_ =>
//     _.ApplyTransformation(Matrix4x4.GetScaleMatrix(2f, 1f, 1f))
// );
var pi = (float) System.Math.PI;

renderer.Render(world, camera);