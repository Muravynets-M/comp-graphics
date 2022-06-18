namespace RT.RenderInput.ObjFile.ObjParser.ObjLineParser;

public class FaceLineParser : IObjLineParser
{
    private readonly ObjFileContent _objFileContent;

    public FaceLineParser(ObjFileContent objFileContent)
    {
        _objFileContent = objFileContent;
    }

    public void ParseLine(string[] line)
    {
        if (line.Length == 0 || line[0] != "f")
            return;

        var firstIndex = line[1].Split("/");
        var vertices = new List<int>();
        var normals = firstIndex.Length > 2 ? new List<int>() : null;
        var textures = firstIndex.Length > 3 ? new List<int>() : null;

        foreach (var indexes in line.Skip(1))
        {
            if (string.IsNullOrEmpty(indexes))
                continue;
            var index = indexes.Split("/");
            vertices.Add(int.Parse(index[0]));
            textures?.Add(int.Parse(index[1]));
            normals?.Add(int.Parse(index[2]));
        }
        
        _objFileContent.AddFace(vertices, textures, normals);
    }
}