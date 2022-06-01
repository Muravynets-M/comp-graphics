using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RT.Render.RenderInput;
using RT.RenderInput.InMemory;
using RT.RenderInput.ObjFile;
using RT.RenderInput.ObjFile.ObjParser;
using RT.RenderInput.ObjFile.ObjParser.ObjLineParser;

namespace RT.RenderInput;

public static class DependencyInjection
{
    public static IServiceCollection AddRenderInput(this IServiceCollection services, IConfiguration configuration)
    {
        var renderInput = configuration["Input:RenderInput"];
        switch (renderInput)
        {
            case "Obj":
            {
                services.AddSingleton<IRenderInput>(_ => new ObjRenderInput(
                    configuration[$"Input:{renderInput}:Filepath"], _.GetService<IObjParser>()!
                ));
                services.AddSingleton<IObjParser>(_ =>
                {
                    var objFileContent = new ObjFileContent();
                    return new ObjParser(
                        objFileContent,
                        new FaceLineParser(objFileContent),
                        new VertexLineParser(objFileContent),
                        new NormalLineParser(objFileContent),
                        new ObjectLineParser(objFileContent),
                        new MaterialLineParser(objFileContent)
                    );
                });
                break;
            }
            case "InMemory":
            {
                services.AddSingleton<IRenderInput, InMemoryInput>();
                break;
            }
            default:
                throw new InvalidDataException();
        }
        
        return services;
    }
}