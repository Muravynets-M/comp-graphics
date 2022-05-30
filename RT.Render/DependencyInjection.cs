using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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
            _.GetService<IWorldTransformAlgorithmFactory>()!
        ));

        return services;
    }
}