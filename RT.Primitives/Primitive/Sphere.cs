using System.Drawing;
using RT.Math.LinearAlgebra;
using RT.Primitives.Traceable;
using RT.Primitives.Transform;

namespace RT.Primitives;

public class Sphere : ITraceable, ITransform
{
    public Point3 Origin { get; }
    public float Radius { get; }

    public Sphere(Point3 origin, float radius)
    {
        Origin = origin;
        Radius = radius;
    }

    public HitResult? Hit(Ray r, float minT, float maxT)
    {
        var oc = r.Origin - Origin;
        var a = Vector3.Dot(r.Direction, r.Direction);
        var b = 2f * Vector3.Dot(oc, r.Direction);
        var c = Vector3.Dot(oc, oc) - Radius * Radius;

        var discriminant = b * b - 4 * a * c;

        if (discriminant < 0)
            return null;

        var sqrtD = MathF.Sqrt(discriminant);
        var root = (-b - sqrtD) / (2 * a);

        if (root < minT || root > maxT)
        {
            root = (-b + sqrtD) / (2 * a);
            if (root < minT || root > maxT)
                return null;
        }

        return new HitResult((Point3) r.Cast(root), (r.Cast(root) - Origin) / Radius, root);
    }
}