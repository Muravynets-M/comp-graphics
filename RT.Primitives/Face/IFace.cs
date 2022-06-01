using RT.Primitives.Primitive;
using RT.Primitives.Traceable;

namespace RT.Primitives.Face;

public interface IFace: ITraceable
{
    public FaceObject FaceObject { get; set; }
}