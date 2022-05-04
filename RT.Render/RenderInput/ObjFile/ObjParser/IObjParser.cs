using RT.Primitives;
using RT.Primitives.Primitive;

namespace RT.Render.RenderInput.ObjFile.ObjParser;

public interface IObjParser
{
    IEnumerable<FaceObject> Parse(StreamReader content);
}