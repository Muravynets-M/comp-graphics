using RT.Primitives.Primitive;

namespace RT.RenderInput.ObjFile.ObjParser;

public interface IObjParser
{
    IEnumerable<FaceObject> Parse(StreamReader content);
}