using Microsoft.Extensions.DependencyInjection;
using RT.Math.LinearAlgebra;
using RT.Primitives;
using RT.Render;

namespace RT.Main;

public static class RenderSetUp
{
    public static ServiceProvider SetUpDi()
    {
        var services = new ServiceCollection();
        services.AddRenderer();
        return services.BuildServiceProvider();
    }

    public static World SetUpWorld()
    {
        var world = new World();
        // world.Traceables.Add(new Sphere(
        //     new Point3(0.0f, 0f, -1f),
        //     0.5f)
        // );

        world.Lights.Add(new Sphere(
            (Point3) Vector3.Unit(new Vector3(-0.5f, 0.5f, -0f)),
            0.5f
        ));

        return world;
    }

    public static Camera SetUpCamera()
    {
        return new Camera(
            new Point3(2f, 2f, 2f),
            new Vector3(0f, 0f, 0f),
            2f,
            2f
        );
    }
}