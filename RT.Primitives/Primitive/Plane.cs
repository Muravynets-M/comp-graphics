using RT.Math.LinearAlgebra;
using RT.Primitives.Material;
using RT.Primitives.Traceable;
using RT.Primitives.Transform;
using RT.Texture;

namespace RT.Primitives.Primitive;

public class Plane : ITraceable, ITransform
{
    public Point3 Origin { get => PointA; }
    public Point3 PointA { get; private set; }
    public Point3 PointB { get; private set; }
    public Point3 PointC { get; private set; }

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
    public Plane(Vector3 normal, Point3 origin)
    {
        _normal = Vector3.Unit(normal);
        PointA = origin;
        // PointB = new Point3(0, 1, 0);
        // PointC = new Point3(0, 0, 1);
    }

    public float MinX => float.MinValue;
    public float MinY => float.MinValue;
    public float MinZ => float.MinValue;
    public float MaxX => float.MaxValue;
    public float MaxY => float.MaxValue;
    public float MaxZ => float.MaxValue;
    
    public IMaterial? Material { get; set; }

    public HitResult? Hit(Ray r, float minT, float maxT)
    {
        var d = r.Direction;
        var o = r.Origin;
        var c = Origin;
        var n = Normal;

        var dn = Vector3.Dot(d, n);
        if (dn == 0)
        {
            return null;
        }

        var t = Vector3.Dot(c - o,  n) / dn;
        if (t <= 0)
        {
            return null;
        }

        if (t < minT || t > maxT)
        {
            return null;
        }

        var p = (Point3) r.Cast(t);

        return (Material is not null) ? new HitResult(p, n, t, Material, GetUV(p)) : new HitResult(p, n, t);
    }

    private UVcoordinates GetUV(Point3 p)
    {
        var a = Vector3.Cross(Normal, Vector3.Up);
        var b = Vector3.Cross(Normal, Vector3.Forward);
        
        var max_ab = Vector3.Dot(a, a) < Vector3.Dot(b, b) ? b : a;
        
        var c = Vector3.Cross(Normal, Vector3.Right);
        
        var texDir = Vector3.Unit(Vector3.Dot(max_ab, max_ab) < Vector3.Dot(c, c) ? c : max_ab);
        
        
        var u = texDir;
        var v = Vector3.Cross(Normal, u);
        
        return new UVcoordinates(
            Vector3.Dot(u, (Vector3) p),
            Vector3.Dot(v, (Vector3) p)
        );
    }
    
    public void ApplyTransformation(Matrix4x4 matrix)
    {
        if (_normal != null)
        {
            _normal = Vector3.Unit((Vector3)(matrix * (Vector4) _normal));
        }

        PointA = (Point3)(matrix * (Vector4) PointA);
        PointB = (Point3)(matrix * (Vector4) PointB);
        PointC = (Point3)(matrix * (Vector4) PointC);
    }
}