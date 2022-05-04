namespace RT.Render.RenderInput.ObjFile.ObjParser.ObjLineParser;

public class NormalLineParser: IObjLineParser
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
        
        _objFileContent.AddNormal(float.Parse(line[1]), float.Parse(line[2]), float.Parse(line[3]));
    }
}