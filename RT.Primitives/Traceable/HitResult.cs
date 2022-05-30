using RT.Math.LinearAlgebra;
using RT.Primitives.Transform;

namespace RT.Primitives.Traceable;

public class HitResult
{
    public Point3 Point { get; }
    public Vector3 Normal { get; }
    public float? LightDotProduct { get; set; }
    public float T { get; }

    public HitResult(Point3 point, Vector3 normal, float t)
    {
        Point = point;
        Normal = normal;
        T = t;
        LightDotProduct = null;
    }
}