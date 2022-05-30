using System.Globalization;

namespace RT.RenderInput.ObjFile.ObjParser.ObjLineParser;

public class NormalLineParser : IObjLineParser
{
    private readonly ObjFileContent _objFileContent;

    public NormalLineParser(ObjFileContent objFileContent)
    {
        _objFileContent = objFileContent;
    }

    public void ParseLine(string[] line)
    {
        if (line.Length == 0 || line[0] != "vn")
            return;

        _objFileContent.AddNormal(
            float.Parse(line[1], CultureInfo.InvariantCulture),
            float.Parse(line[2], CultureInfo.InvariantCulture),
            float.Parse(line[3], CultureInfo.InvariantCulture)
        );
    }
}