using System.Globalization;

namespace RT.RenderInput.ObjFile.ObjParser.ObjLineParser;

public class TextureLineParser: IObjLineParser
{
    private readonly ObjFileContent _objFileContent;

    public TextureLineParser(ObjFileContent objFileContent)
    {
        _objFileContent = objFileContent;
    }
    
    public void ParseLine(string[] line)
    {
        if (line.Length == 0 || line[0] != "vt")
            return;

        _objFileContent.AddTextures(
            float.Parse(line[1], CultureInfo.InvariantCulture),
            float.Parse(line[2], CultureInfo.InvariantCulture)
        );
    }
}