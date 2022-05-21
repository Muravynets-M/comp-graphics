using RT.Primitives.Primitive;
using RT.Primitives.Traceable;
using RT.Render.RenderInput.ObjFile.ObjParser;

namespace RT.Render.RenderInput.ObjFile;

public class ObjRenderInput: IRenderInput
{
    private readonly string _fileName;
    private readonly IObjParser _objParser;

    public ObjRenderInput(string fileName, IObjParser objParser)
    {
        _fileName = fileName;
        _objParser = objParser;
    }
    
    public IEnumerable<ITraceable> GetWorldInput()
    {
        using var streamReader = new StreamReader($"../../../{_fileName}");
        
        return _objParser.Parse(streamReader).SelectMany(_ => _.Faces);
    }
}