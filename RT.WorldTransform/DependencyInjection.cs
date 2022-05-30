using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RT.Render.WorldTransformAlgorithm;
using RT.WorldTransform.WorldTransformAlgorithm;

namespace RT.WorldTransform;

public static class DependencyInjection
{
    public static IServiceCollection AddWorldTransformAlgorithms
        (this IServiceCollection services)
    {
        services.AddSingleton<IWorldTransformAlgorithmFactory>(_ => new WorldTransformAlgorithmFactory(
            new IWorldTransformAlgorithm[]
            {
                new TopDownWorldTransform(30),
            }
        ));

        return services;
    }
}