using System.Globalization;

namespace RT.RenderInput.ObjFile.ObjParser.ObjLineParser;

public class VertexLineParser : IObjLineParser
{
    private readonly ObjFileContent _objFileContent;

    public VertexLineParser(ObjFileContent objFileContent)
    {
        _objFileContent = objFileContent;
    }

    public void ParseLine(string[] line)
    {
        if (line.Length == 0 || line[0] != "v")
            return;


        _objFileContent.AddVertex(
            float.Parse(line[1], CultureInfo.InvariantCulture),
            float.Parse(line[2], CultureInfo.InvariantCulture),
            float.Parse(line[3], CultureInfo.InvariantCulture)
        );
    }
}