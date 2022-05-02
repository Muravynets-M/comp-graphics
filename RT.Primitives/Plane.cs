using RT.Math.LinearAlgebra;
using RT.Primitives.Traceable;
using RT.Primitives.Transform;

namespace RT.Primitives;

public class Plane : ITraceable, ITransform
{
    public Point3 Origin { get => PointA; }
    public Point3 PointA { get; }
    public Point3 PointB { get; }
    public Point3 PointC { get; }

    private Vector3? _normal;
    public Vector3 Normal
    {
        get
        {
            if (_normal != null)
            {
                return (Vector3) _normal;
            } 
            return Vector3.Unit(
                Vector3.Cross(Vector3.FromPoints(PointA, PointB), Vector3.FromPoints(PointA, PointC)));
        }
    }

    public Plane(Point3 a, Point3 b, Point3 c)
    {
        if (a.Equals(b) || b.Equals(c) || c.Equals(a))
        {
            throw new ArgumentException("use 3 different points, used {a}, {b}, {c}");
        }
        
        PointA = a;
        PointB = b;
        PointC = c;
    }

    public static Plane PlaneFromNormal(Vector3 normal, Point3 origin)
    {
        var p = new Plane(origin, new Point3(0,1,0), new Point3(0,0,1));
        p._normal = normal;
        return p;
    }
    public HitResult? Hit(Ray r, float minT, float maxT)
    {
        var d = r.Direction;
        var o = r.Origin;
        var c = Origin;
        var n = Normal;
        
        if (Vector3.Dot(d , n) == 0)
        {
            return null;
        }

        var t = Vector3.Dot(c - o,  n) / Vector3.Dot(d, n);
        if (t <= 0)
        {
            return null;
        }

        if (t < minT || t > maxT)
        {
            return null;
        }

        return new HitResult((Point3)r.Cast(t), n, t);
    }
}