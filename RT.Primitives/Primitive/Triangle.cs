using RT.Math.LinearAlgebra;
using RT.Primitives.Traceable;

namespace RT.Primitives.Primitive;

public class Triangle : IFace
{
    public Point3 Vertex1 => Plane.PointA;

    public Point3 Vertex2 => Plane.PointB;

    public Point3 Vertex3 => Plane.PointC;

    public Vector3 Normal1 { get; private set; }
    public Vector3 Normal2 { get; private set; }
    public Vector3 Normal3 { get; private set; }

    public Point3 Origin => Plane.Origin;

    private Plane Plane { get; }

    public Triangle(Point3 vertex1, Point3 vertex2, Point3 vertex3)
    {
        Plane = new Plane(vertex1, vertex2, vertex3);
        
        if (!DoTriangleExist(vertex1, vertex2, vertex3))
        {
            throw new ArgumentException($"such triangle ({vertex1}, {vertex2}, {vertex3}) cannot exist");
        }

        Normal1 = Plane.Normal;
        Normal2 = Plane.Normal;
        Normal3 = Plane.Normal;
    }

    public Triangle(
        Point3 vertex1, Point3 vertex2, Point3 vertex3,
        Vector3 normal1, Vector3 normal2, Vector3 normal3)
    {
        Plane = new Plane(vertex1, vertex2, vertex3);
        
        if (!DoTriangleExist(vertex1, vertex2, vertex3))
        {
            throw new ArgumentException($"such triangle ({vertex1}, {vertex2}, {vertex3}) cannot exist");
        }

        Normal1 = normal1;
        Normal2 = normal2;
        Normal3 = normal3;
    }

    public float MinX => MathF.Min(MathF.Min(Vertex1.X, Vertex2.X), Vertex3.X);
    public float MinY => MathF.Min(MathF.Min(Vertex1.Y, Vertex2.Y), Vertex3.Y);
    public float MinZ => MathF.Min(MathF.Min(Vertex1.Z, Vertex2.Z), Vertex3.Z);
    public float MaxX => MathF.Max(MathF.Max(Vertex1.X, Vertex2.X), Vertex3.X);
    public float MaxY => MathF.Max(MathF.Max(Vertex1.Y, Vertex2.Y), Vertex3.Y);
    public float MaxZ => MathF.Max(MathF.Max(Vertex1.Z, Vertex2.Z), Vertex3.Z);

    public HitResult? Hit(Ray r, float minT, float maxT)
    {
        var hit = RayIntersectsTriangle(r);
        if (hit == null)
        {
            return null;
        }

        var t = hit.T;
        if (t < minT || t > maxT)
        {
            return null;
        }

        return hit;
    }

    public void ApplyTransformation(Matrix4x4 matrix)
    {
        Plane.ApplyTransformation(matrix);
        Normal1 = Vector3.Unit((Vector3)(matrix * (Vector4) Normal1));
        Normal2 = Vector3.Unit((Vector3)(matrix * (Vector4) Normal2));
        Normal3 = Vector3.Unit((Vector3)(matrix * (Vector4) Normal3));
    }
    
    private HitResult? RayIntersectsTriangle(Ray ray)
    {
        // Möller–Trumbore intersection algorithm
        const float epsilon = 0.0000001f;

        // find vectors for two edges sharing vert1
        var edge1 = Vector3.FromPoints(Vertex1, Vertex2);
        var edge2 = Vector3.FromPoints(Vertex1, Vertex3);

        Vector3 h, s, q;
        // begin calculating determinant - also used to calculate u parameter
        h = Vector3.Cross(ray.Direction, edge2);

        float a, f, u, v;
        // if determinant is near zero, ray lies in plane of triangle
        a = Vector3.Dot(edge1, h);

        // This ray is parallel to this triangle.
        if (a > -epsilon && a < epsilon)
        {
            return null;
        }

        f = 1.0f / a;
        // calculate distance from vert1 to ray origin
        s = ray.Origin - Vertex1;
        // calculate u parameter and test bounds
        u = f * Vector3.Dot(s, h);
        if (u < 0 || u > 1)
        {
            return null;
        }

        // prepare to test v parameter
        q = Vector3.Cross(s, edge1);
        // calculate v parameter and test bounds
        v = f * Vector3.Dot(ray.Direction, q);
        if (v < 0 || u + v > 1)
        {
            return null;
        }

        // At this stage we can compute t to find out where the intersection point is on the line.
        var t = f * Vector3.Dot(edge2, q);

        // This means that there is a line intersection but not a ray intersection.
        if (!(t > epsilon))
        {
            return null;
        }

        // b1*n1 + b2*n2 + b3*n3 normal interpolation
        var n = Vector3.Unit(Normal1 * (1 - u - v) + Normal2 * u + Normal3 * v);

        // ray intersection
        var p = (Point3) ray.Cast(t);

        return new HitResult(p, n, t);
    }

    // private HitResult? RayIntersectsTriangleCulling(Ray ray)
    // {
    //     // Möller–Trumbore one-sided intersection algorithm
    //     const float epsilon = 0.0000001f;
    //
    //     // find vectors for two edges sharing vert1
    //     var edge1 = Vector3.FromPoints(Vertex1, Vertex2);
    //     var edge2 = Vector3.FromPoints(Vertex1, Vertex3);
    //     
    //     Vector3 h, s, q;
    //     // begin calculating determinant - also used to calculate u parameter
    //     h = Vector3.Cross(ray.Direction, edge2);
    //     
    //     float a, f, u, v;
    //     // if determinant is near zero, ray lies in plane of triangle
    //     a = Vector3.Dot(edge1, h);
    //
    //     // This ray is parallel to this triangle.
    //     if (a < epsilon)
    //     {
    //         return null;
    //     }
    //     
    //     // calculate distance from vert1 to ray origin
    //     s = ray.Origin - Vertex1;
    //     // calculate u parameter and test bounds
    //     u = Vector3.Dot(s, h);
    //     if (u < 0 || u > a)
    //     {
    //         return null;
    //     }
    //
    //     // prepare to test v parameter
    //     q = Vector3.Cross(s, edge1);
    //     // calculate v parameter and test bounds
    //     v = Vector3.Dot(ray.Direction, q);
    //     if (v < 0 || u + v > a)
    //     {
    //         return null;
    //     }
    //
    //     f = 1 / a;
    //
    //     // At this stage we can compute t to find out where the intersection point is on the line.
    //     var t = f * Vector3.Dot(edge2, q);
    //
    //     // ray intersection
    //     return new HitResult((Point3)ray.Cast(t), plane.Normal, t);
    // }

    private static bool DoTriangleExist(Point3  vertex1, Point3  vertex2, Point3  vertex3)
    {
        var edge1 = (vertex1 - vertex2).Lenght;
        var edge2 = (vertex1 - vertex3).Lenght;
        var edge3 = (vertex2 - vertex3).Lenght;
        
        return (edge1 + edge2 > edge3)
               && (edge1 + edge3 > edge2)
               && (edge2 + edge3 > edge1);
    }
}