using System.Drawing;
using RT.Material;
using RT.Math.LinearAlgebra;

namespace RT.RenderInput.ObjFile.ObjParser.ObjLineParser;

public class MaterialLineParser : IObjLineParser
{
    private readonly ObjFileContent _objFileContent;

    public MaterialLineParser(ObjFileContent objFileContent)
    {
        _objFileContent = objFileContent;
    }

    public void ParseLine(string[] line)
    {
        if (line.Length == 0 || line[0] != "m")
            return;

        switch (line[1])
        {
            case "lambert":
            {
                var color = ColorTranslator.FromHtml(line[2]);
                _objFileContent.AddMaterial(new LambertMaterial(new Vector3(
                        color.R / 255f,
                        color.G / 255f,
                        color.B / 255f
                    )
                ));
                
                break;
            }
            case "mirror":
            {
                var color = ColorTranslator.FromHtml(line[2]);
                _objFileContent.AddMaterial(new MirrorMaterial(new Vector3(
                        color.R / 255f,
                        color.G / 255f,
                        color.B / 255f
                    )
                ));
                
                break;
            }
            default:
                throw new InvalidDataException();
        }
    }
}