// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RT.Main;
using RT.Render;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var provider = RenderSetUp.SetUpDi(config);

var world = RenderSetUp.SetUpWorld(provider);
var camera = RenderSetUp.SetUpCamera();

var renderer = provider.GetService<IRenderer>()!;

renderer.Render(world, camera);