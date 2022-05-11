using System.Text.RegularExpressions;
using RT.Primitives.Primitive;
using RT.Render.RenderInput.ObjFile.ObjParser.ObjLineParser;

namespace RT.Render.RenderInput.ObjFile.ObjParser;

public class ObjParser: IObjParser
{
    private readonly ObjFileContent _objFileContent;
    private readonly List<IObjLineParser>_objLineParsers;

    public ObjParser(ObjFileContent objFileContent, params IObjLineParser[] objLineParsers)
    {
        _objFileContent = objFileContent;
        _objLineParsers = new List<IObjLineParser>(objLineParsers);
    }

    public IEnumerable<FaceObject> Parse(StreamReader content)
    {
        var line = content.ReadLine();
        
        while (line is not null)
        {
            _objLineParsers.ForEach(_ => _.ParseLine(Regex.Split(line, @"\s+")));
            
            line = content.ReadLine();
        }

        return _objFileContent.FaceObjects;
    }
}