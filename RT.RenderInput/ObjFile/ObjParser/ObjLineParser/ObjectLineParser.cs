namespace RT.RenderInput.ObjFile.ObjParser.ObjLineParser;

public class ObjectLineParser : IObjLineParser
{
    private readonly ObjFileContent _objFileContent;

    public ObjectLineParser(ObjFileContent objFileContent)
    {
        _objFileContent = objFileContent;
    }

    public void ParseLine(string[] line)
    {
        if (line.Length == 0 || line[0] != "o")
            return;

        _objFileContent.AddFaceObject(line[1]);
    }
}