using RT.Math.LinearAlgebra;

namespace RT.Primitives.Traceable;

public struct HitResult
{
    public Point3 Point { get; }
    public Vector3 Normal { get; }
    public float T { get; }

    public HitResult(Point3 point, Vector3 normal, float t)
    {
        Point = point;
        Normal = normal;
        T = t;
    }
}