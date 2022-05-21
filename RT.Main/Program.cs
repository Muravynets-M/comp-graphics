// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RT.Main;
using RT.Math;
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

renderer.Render(world, camera);
