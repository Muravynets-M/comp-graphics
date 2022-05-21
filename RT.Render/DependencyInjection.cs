using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RT.Math.LinearAlgebra;
using RT.Render.Render;
using RT.Render.RenderInput;
using RT.Render.RenderInput.ObjFile;
using RT.Render.RenderInput.ObjFile.ObjParser;
using RT.Render.RenderInput.ObjFile.ObjParser.ObjLineParser;
using RT.Render.RenderOutput.HitResultAdapter;
using RT.Render.RenderOutput.ImageBuffer;
using RT.Render.WorldTransformAlgorithm;

namespace RT.Render;

public static class DependencyInjection
{
    public static IServiceCollection AddRenderer(this IServiceCollection services, IConfiguration configuration)
    {
        var renderOutput = configuration["RenderOutput"];
        var width = int.Parse(configuration[$"{renderOutput}:Width"]);
        var height = int.Parse(configuration[$"{renderOutput}:Height"]);
        switch (renderOutput)
        {
            case "Ppm":
            {
                services.AddSingleton<IImageBuffer>(_ =>
                    new PpmImageBuffer(width, height, configuration[$"{renderOutput}:FileName"]));
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

        services.AddSingleton(_ => new ImageBufferFactory(renderOutput,
            width, height));
        services.AddSingleton<IWorldTransformAlgorithm, StubWorldTransformAlgorithm>();


        services.AddSingleton<IRenderInput>(_ => new ObjRenderInput(
            configuration["RenderInput"], _.GetService<IObjParser>()!
        ));
        services.AddSingleton<IObjParser>(_ =>
        {
            var objFileContent = new ObjFileContent();
            return new ObjParser(
                objFileContent,
                new FaceLineParser(objFileContent),
                new VertexLineParser(objFileContent),
                new NormalLineParser(objFileContent),
                new ObjectLineParser(objFileContent)
            );
        });
        services.AddSingleton<IRenderer>(_ => new InfiniteRenderer(
            _.GetService<ImageBufferFactory>()!, 
            _.GetService<IHitResultAdapter>()!,
            new IWorldTransformAlgorithm[]{new SingleBoxWorldTransformAlgorithm()}
        ));

        return services;
    }
}