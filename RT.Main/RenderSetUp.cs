using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RT.Math.LinearAlgebra;
using RT.Primitives;
using RT.Render;

namespace RT.Main;

public static class RenderSetUp
{
    public static ServiceProvider SetUpDi(IConfiguration config)
    {
        var services = new ServiceCollection();
        services.AddRenderer(config);
        return services.BuildServiceProvider();
    }
    
    public static World SetUpWorld()
    {
        return new World();
    }

    public static Camera SetUpCamera()
    {
        return new Camera(
            new Point3(0.5f, 0.5f, 0.5f),
            new Vector3(0f, 0f, 0f),
            2f,
            2f
        );
    }
}