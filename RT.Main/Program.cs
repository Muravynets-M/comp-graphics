// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using RT.Main;
using RT.Render.Render;
using RT.Render.RenderInput;

var provider = RenderSetUp.SetUpDi();
var world = RenderSetUp.SetUpWorld();
var camera = RenderSetUp.SetUpCamera();

var renderer = provider.GetService<IRenderer>()!;
var input = provider.GetService<IRenderInput>()!;

world.Traceables.AddRange(input.GetWorldInput());

renderer.Render(world, camera);
