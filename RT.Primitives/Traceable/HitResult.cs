using RT.Math.LinearAlgebra;
using RT.Primitives.Material;
using RT.Primitives.Transform;
using RT.Texture;

namespace RT.Primitives.Traceable;

public class HitResult
{
    public Point3 Point { get; }
    public Vector3 Normal { get; }
    public IMaterial? Material { get; }
    public UVcoordinates UVcoordinates { get; }
    public float T { get; }

    public HitResult(Point3 point, Vector3 normal, float t)
    {
        Point = point;
        Normal = normal;
        T = t;
        UVcoordinates = new UVcoordinates(0, 0);
    }
    
    public HitResult(Point3 point, Vector3 normal, float t, IMaterial material) : this(point, normal, t)
    {
        Material = material;
    }
    
    public HitResult(Point3 point, Vector3 normal, float t, IMaterial material, UVcoordinates uv) : this(point, normal, t, material)
    {
        UVcoordinates = uv;
    }
}