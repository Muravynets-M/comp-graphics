using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RT.Math.LinearAlgebra;
using RT.Render.RenderOutput;
using RT.RenderOutput.HitResultAdapter;
using RT.RenderOutput.ImageBuffer;

namespace RT.RenderOutput;

public static class DependencyInjection
{
    public static IServiceCollection AddRenderOutput(this IServiceCollection services, IConfiguration configuration)
    {
        var renderOutput = configuration["Output:RenderOutput"];
        var width = int.Parse(configuration[$"Output:{renderOutput}:Width"]);
        var height = int.Parse(configuration[$"Output:{renderOutput}:Height"]);
        switch (renderOutput)
        {
            case "Ppm":
            {
                services.AddSingleton<IImageBuffer>(_ =>
                    new PpmImageBuffer(width, height, configuration[$"Output:{renderOutput}:FileName"]));
                services.AddSingleton<IHitResultAdapter>(_ => new ColorlessPpmHitResultAdapter(
                    new Vector3(138 / 255f, 43 / 255f, 226 / 255f),
                    new Vector3(255 / 255f, 192 / 255f, 203 / 255f)
                ));
                break;
            }
            case "Console":
            {
                services.AddSingleton<IImageBuffer>(_ => new ConsoleImageBuffer(width, height));
                services.AddSingleton<IHitResultAdapter>(_ => new AsciiHitResultAdapter());
                break;
            }
            default:
                throw new InvalidDataException();
        }

        services.AddSingleton<IImageBufferFactory>(_ => new ImageBufferFactory(renderOutput,
            width, height));
        
        return services;
    }
}