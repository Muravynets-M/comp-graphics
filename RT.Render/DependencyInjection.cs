using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RT.Math.LinearAlgebra;
using RT.Render.Render;
using RT.Render.RenderOutput;
using RT.Render.WorldTransformAlgorithm;

namespace RT.Render;

public static class DependencyInjection
{
    public static IServiceCollection AddRenderer(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IRenderer>(_ => new InfiniteRenderer(
            _.GetService<IImageBufferFactory>()!, 
            _.GetService<IHitResultAdapter>()!,
            _.GetService<IWorldTransformAlgorithmFactory>()!,
            new Vector3(0 / 255f, 191 / 255f, 255 / 255f)
        ));

        return services;
    }
}