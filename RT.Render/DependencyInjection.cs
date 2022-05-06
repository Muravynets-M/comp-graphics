using Microsoft.Extensions.DependencyInjection;
using RT.Math.LinearAlgebra;
using RT.Render.Render;
using RT.Render.RenderInput;
using RT.Render.RenderInput.ObjFile;
using RT.Render.RenderInput.ObjFile.ObjParser;
using RT.Render.RenderInput.ObjFile.ObjParser.ObjLineParser;
using RT.Render.RenderOutput.HitResultAdapter;
using RT.Render.RenderOutput.ImageBuffer;

namespace RT.Render;

public static class DependencyInjection
{
    public static IServiceCollection AddRenderer(this IServiceCollection services)
    {
        services.AddSingleton<IImageBuffer>(_ => new PpmImageBuffer(600, 600, "renderOutput"));
        services.AddSingleton<IHitResultAdapter>(_ => new ColorlessPpmHitResultAdapter(
            new Vector3(138 / 255f, 43 / 255f, 226 / 255f),
            new Vector3(255 / 255f, 192 / 255f, 203 / 255f)
        ));

        services.AddSingleton<IRenderInput>(_ => new ObjRenderInput(
            "renderInput", _.GetService<IObjParser>()!
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
        services.AddSingleton<IRenderer, Renderer>();

        return services;
    }
}