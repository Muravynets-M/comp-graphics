using RT.Math.LinearAlgebra;
using RT.Primitives.Traceable;

namespace RT.Primitives.Primitive;

public class FaceObject : ITraceable
{
    private readonly List<IFace> _faces;

    public FaceObject(List<IFace> faces, string name)
    {
        Name = name;
        _faces = faces;
    }

    public Point3 Origin { get; }
    
    public string Name { get; }

    public HitResult? Hit(Ray r, float minT, float maxT)
    {
        return _faces
            .Select(f => f.Hit(r, minT, maxT))
            .Where(_ => _ != null)
            .MinBy(result => result!.T);
    }
}