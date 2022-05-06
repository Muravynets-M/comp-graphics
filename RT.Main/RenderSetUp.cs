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
        return new World();
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