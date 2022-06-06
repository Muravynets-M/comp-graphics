using RT.Math.LinearAlgebra;
using RT.Primitives.Material;
using RT.Primitives.Traceable;
using RT.Primitives.Transform;
using RT.Texture;

namespace RT.Primitives.Primitive;

public class Sphere : ITraceable, ITransform
{
    public Point3 Origin { get; private set; }
    public float Radius { get; }

    public Sphere(Point3 origin, float radius)
    {
        Origin = origin;
        Radius = radius;
    }

    public float MinX => Origin.X - Radius;
    public float MinY => Origin.Y - Radius;
    public float MinZ => Origin.Z - Radius;
    public float MaxX => Origin.X + Radius;
    public float MaxY => Origin.Y + Radius;
    public float MaxZ => Origin.Z + Radius;
    
    public IMaterial? Material { get; set; }

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

        var pointVec = r.Cast(root);
        var point = (Point3)(pointVec + (pointVec - Origin) * 0.00001f);

        if (Material is not null)
        {
            return new HitResult(point, (r.Cast(root) - Origin) / Radius, root, Material, GetUV(point));
        }

        return new HitResult(point, (r.Cast(root) - Origin) / Radius, root);
    }

    private UVcoordinates GetUV(Point3 p)
    {
        var theta = MathF.Atan2(-(p.Z - Origin.Z), p.X - Origin.X);
        var phi = MathF.Acos(-(p.Y - Origin.Y) / Radius);

        var u = (theta + MathF.PI) / (2 * MathF.PI);
        var v = phi / MathF.PI;

        return new UVcoordinates(u, v);
    }
    
    public void ApplyTransformation(Matrix4x4 matrix)
    {
        Origin = (Point3) (matrix * (Vector4) Origin);
    }
}