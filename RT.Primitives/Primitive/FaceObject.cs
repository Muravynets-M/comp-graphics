using RT.Math.LinearAlgebra;
using RT.Primitives.Face;
using RT.Primitives.Traceable;

namespace RT.Primitives.Primitive;

public class FaceObject : ITraceable
{
    public readonly List<IFace> Faces;

    public FaceObject(List<IFace> faces, string name)
    {
        Name = name;
        Faces = faces;
    }

    public Point3 Origin { get; }
    
    public string Name { get; }

    public float MinX => Faces.MinBy(_ => _.MinX)!.MinX;
    public float MinY => Faces.MinBy(_ => _.MinY)!.MinY;
    public float MinZ => Faces.MinBy(_ => _.MinZ)!.MinZ;
    public float MaxX => Faces.MaxBy(_ => _.MaxX)!.MaxX;
    public float MaxY => Faces.MaxBy(_ => _.MaxY)!.MaxY;
    public float MaxZ => Faces.MaxBy(_ => _.MaxZ)!.MaxZ;

    public HitResult? Hit(Ray r, float minT, float maxT)
    {
        return Faces
            .Select(f => f.Hit(r, minT, maxT))
            .Where(_ => _ != null)
            .MinBy(result => result!.T);
    }
    
    public void ApplyTransformation(Matrix4x4 matrix)
    {
        Faces.ForEach(f => f.ApplyTransformation(matrix));
    }
}