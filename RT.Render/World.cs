using RT.Math.LinearAlgebra;
using RT.Primitives.Traceable;
using RT.Primitives.Transform;

namespace RT.Render;

public class World
{
    public static Vector3 Up = new(0.0f, 1.0f, 0.0f);
    public static Vector3 Right = new(1.0f, 0.0f, 0.0f);
    public static Vector3 Forward = new(0.0f, 1.0f, -1.0f);
    
    public List<ITransform> Lights = new();

    public List<ITraceable> Traceables = new();

    public HitResult? Cast(Ray ray, float minT = float.Epsilon, float maxT = float.PositiveInfinity)
    {
        return Traceables
            .Select(traceable => traceable.Hit(ray, minT, maxT))
            .MinBy(hitResult => hitResult?.T);
    }
}