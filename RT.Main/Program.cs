// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RT.Main;
using RT.Math;
using RT.Math.LinearAlgebra;
using RT.Primitives;
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

var renderer = provider.GetService<IRenderer>()!;
var input = provider.GetService<IRenderInput>()!;

world.Lights.AddRange(new InMemorySetup().GetWorldInput());

world.Traceables.AddRange(input.GetWorldInput());

var pi = (float) System.Math.PI;
world.Traceables[0].ApplyTransformation(new TransformationBuilder()
     .WithScaling(1.25f, 1, 1)
     .WithRotation(0f, pi/3, 0f)
     .WithTranslation(0f, 0.2f, 0f)
     .Build()
);

renderer.Render(world, camera);
