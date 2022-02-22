using RT.Math.LinearAlgebra;
using RT.Primitives.Traceable;

namespace RT.Primitives;

public class Sphere : ITraceable
{
    public Point3 Center { get; }
    public float Radius { get; }

    public Sphere(Point3 center, float radius)
    {
        Center = center;
        Radius = radius;
    }

    public HitResult? Hit(Ray r, float minT, float maxT)
    {
        var oc = r.Origin - Center;
        var a = Vector3.Dot(r.Direction, r.Direction);
        var b = 2f * Vector3.Dot(oc, r.Direction);
        var c = Vector3.Dot(oc, oc) - Radius * Radius;

        var discriminant = b * b - 4 * a * c;

        if (discriminant < 0)
            return null;

        var sqrtD = MathF.Sqrt(discriminant);
        var root = (-2f * b - sqrtD) / (2 * a);

        if (root < minT || root > maxT)
        {
            root = (-2f * b + sqrtD) / (2 * a);
            if (root < minT || root > maxT)
                return null;
        }

        return new HitResult(Vector3.ToPoint(r.Cast(root)), (r.Cast(root) - Center) / Radius, root);
    }
}