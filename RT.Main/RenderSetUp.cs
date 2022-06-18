using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RT.Light;
using RT.Math.LinearAlgebra;
using RT.Primitives.Primitive;
using RT.Primitives.Transform;
using RT.Render;
using RT.Render.RenderInput;
using RT.RenderInput;
using RT.RenderOutput;
using RT.WorldTransform;

namespace RT.Main;

public static class RenderSetUp
{
    public static ServiceProvider SetUpDi(IConfiguration config)
    {
        var services = new ServiceCollection();
        services.AddRenderer(config);
        services.AddRenderInput(config);
        services.AddRenderOutput(config);
        services.AddWorldTransformAlgorithms();

        return services.BuildServiceProvider();
    }

    public static World SetUpWorld(ServiceProvider serviceProvider)
    {
        var world = new World();
        SetUpLights(world);
        
        var input = serviceProvider.GetService<IRenderInput>()!;

        world.Traceables.AddRange(input.GetWorldInput());

        return world;
    }

    public static Camera SetUpCamera()
    {
        return new Camera(
            new Point3(1f, 1.5f, 1f),
            new Vector3(0f, 0f, 0f),
            16f / 6f,
            9f / 6f
        );
    }

    private static void SetUpLights(World world)
    {
        world.Lights = new List<Primitives.Light.ILight>
        {
            new PointLight(new Point3(-30f, 35f, -30f), new ColorRGB(1, 1, 1), 0.99f),
            // new DirectionalLight(new Vector3(-0.3f, 1.0f, -1.0f), new ColorRGB(1, 1, 1), 0.8f),
            // new DirectionalLight(-(World.Up+World.Forward), new ColorRGB(1, 1, 1), 0.8f),
            // new EnvironmentalLight(new ColorRGB(1, 1, 1), 0.6f)
        };
    }
}