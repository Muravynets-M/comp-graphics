using RT.Math.LinearAlgebra;
using RT.Primitives.Traceable;

namespace RT.Primitives.Primitive;

public class Box : ITraceable
{
    private readonly List<ITraceable> _traceables;
    public Point3 Origin => (Point3) ((_maxVertex + _minVertex) / 2);

    private readonly Point3 _minVertex;
    private readonly Point3 _maxVertex;

    public Box(List<ITraceable> traceables)
    {
        _traceables = traceables;
        _minVertex = new Point3(
            traceables.MinBy(_ => _.MinX)!.MinX,
            traceables.MinBy(_ => _.MinY)!.MinY,
            traceables.MinBy(_ => _.MinZ)!.MinZ
        );
        _maxVertex = new Point3(
            traceables.MaxBy(_ => _.MaxX)!.MaxX,
            traceables.MaxBy(_ => _.MaxY)!.MaxY,
            traceables.MaxBy(_ => _.MaxZ)!.MaxZ
        );
    }

    public void ApplyTransformation(Matrix4x4 matrix)
    {
        _traceables.ForEach(f => f.ApplyTransformation(matrix));
    }

    public float MinX => _minVertex.X;
    public float MinY => _minVertex.Y;
    public float MinZ => _minVertex.Z;
    public float MaxX => _maxVertex.X;
    public float MaxY => _maxVertex.Y;
    public float MaxZ => _maxVertex.Z;

    public HitResult? Hit(Ray r, float minT, float maxT)
    {
        if (IsBoxHit(r))
        {
            return _traceables
                .Select(f => f.Hit(r, minT, maxT))
                .Where(_ => _ != null)
                .MinBy(result => result!.T);
        }

        return null;
    }

    private bool IsBoxHit(Ray r)
    {
        // Y planes
        if (Vector3.Dot(r.Direction, Vector3.Up) < 0f && r.Origin.Y > MaxY)
        {
            var minRect = new Point3(MinX, MaxY, MinZ);
            var maxRect = new Point3(MaxX, MaxY, MaxZ);
            var planeOrigin = new Vector3(r.Origin.X, MaxY, r.Origin.Z);

            if (IsRectHit(minRect, maxRect, RectHitPoint(planeOrigin - r.Origin, r)))
            {
                return true;
            }
        }
        else if (r.Origin.Y < MinY)
        {
            var minRect = new Point3(MinX, MinY, MinZ);
            var maxRect = new Point3(MaxX, MinY, MaxZ);
            var planeOrigin = new Vector3(r.Origin.X, MinY, r.Origin.Z);

            if (IsRectHit(minRect, maxRect, RectHitPoint(planeOrigin - r.Origin, r)))
            {
                return true;
            }
        }

        // X planes
        if (Vector3.Dot(r.Direction, Vector3.Right) < 0f && r.Origin.X > MaxX)
        {
            var minRect = new Point3(MaxX, MinY, MinZ);
            var maxRect = new Point3(MaxX, MaxY, MaxZ);
            var planeOrigin = new Vector3(MaxX, r.Origin.Y, r.Origin.Z);

            if (IsRectHit(minRect, maxRect, RectHitPoint(planeOrigin - r.Origin, r)))
            {
                return true;
            }
        }
        else if (r.Origin.X < MinX)
        {
            var minRect = new Point3(MinX, MinY, MinZ);
            var maxRect = new Point3(MinX, MaxY, MaxZ);
            var planeOrigin = new Vector3(MinX, r.Origin.Y, r.Origin.Z);

            if (IsRectHit(minRect, maxRect, RectHitPoint(planeOrigin - r.Origin, r)))
            {
                return true;
            }
        }

        // Z planes
        if (Vector3.Dot(r.Direction, Vector3.Forward) < 0f && r.Origin.Z > MaxZ)
        {
            var minRect = new Point3(MinX, MinY, MaxZ);
            var maxRect = new Point3(MaxX, MaxY, MaxZ);
            var planeOrigin = new Vector3(r.Origin.X, r.Origin.Y, MaxZ);

            if (IsRectHit(minRect, maxRect, RectHitPoint(planeOrigin - r.Origin, r)))
            {
                return true;
            }
        }
        else if (r.Origin.Z < MinZ)
        {
            var minRect = new Point3(MinX, MinY, MinZ);
            var maxRect = new Point3(MaxX, MaxY, MinZ);
            var planeOrigin = new Vector3(r.Origin.X, r.Origin.Y, MinZ);

            if (IsRectHit(minRect, maxRect, RectHitPoint(planeOrigin - r.Origin, r)))
            {
                return true;
            }
        }

        return false;
    }

    private Point3 RectHitPoint(Vector3 planeOrigin, Ray r)
    {
        var scale = Vector3.Dot(r.Direction, planeOrigin);
        return (Point3) r.Cast(r.Direction.Lenght * planeOrigin.LengthSquared / (scale));
    }

    private bool IsRectHit(Point3 minRect, Point3 maxRect, Point3 planeHitPoint)
    {
        if (System.Math.Abs(minRect.X - maxRect.X) < 0.0001f)
        {
            return FloatBetween(minRect.Y, maxRect.Y, planeHitPoint.Y) &&
                   FloatBetween(minRect.Z, maxRect.Z, planeHitPoint.Z);
        }

        if (System.Math.Abs(minRect.Y - maxRect.Y) < 0.0001f)
        {
            return FloatBetween(minRect.X, maxRect.X, planeHitPoint.X) &&
                   FloatBetween(minRect.Z, maxRect.Z, planeHitPoint.Z);
        }

        if (System.Math.Abs(minRect.Z - maxRect.Z) < 0.0001f)
        {
            return FloatBetween(minRect.X, maxRect.X, planeHitPoint.X) &&
                   FloatBetween(minRect.Y, maxRect.Y, planeHitPoint.Y);
        }

        return false;
    }

    public bool FloatBetween(float a, float b, float x)
    {
        return (a - x) < -0.0001f && x - b < -0.0001f;
    }
}