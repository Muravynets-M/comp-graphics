using RT.Math.LinearAlgebra;
using RT.Primitives.Material;
using RT.Primitives.Traceable;
using Matrix4x4 = RT.Math.LinearAlgebra.Matrix4x4;
using Vector3 = RT.Math.LinearAlgebra.Vector3;

namespace RT.Primitives.Primitive;

public class Circle : ITraceable
{
    public Point3 Origin { get => plane.Origin; }
    public float Radius { get; }
    public Plane plane {get;}
    
    public Circle(Vector3 normal, float radius, Point3 center)
    {
        Radius = radius;
        plane = Plane.PlaneFromNormal(normal, center);
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
        var hit = plane.Hit(r, minT, maxT);
        if (hit == null)
        {
            return null;
        }
        
        if (Vector3.FromPoints(hit.Point, Origin).Lenght > Radius)
        {
            return null;
        }
        
        return hit;
    }
    
    public void ApplyTransformation(Matrix4x4 matrix)
    {
        plane.ApplyTransformation(matrix);
    }
}