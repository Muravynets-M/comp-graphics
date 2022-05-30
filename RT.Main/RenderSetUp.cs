using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            new Point3(-1.5f, 1.5f, -1.5f),
            new Vector3(0f, 0f, 0f),
            2f,
            2f
        );
    }

    private static void SetUpLights(World world)
    {
        world.Lights = new List<ITransform>
        {
            new Sphere(
                new Point3(-5f, 5f, -7f),
                0.5f
            )
        };
    }
}