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
        var minHitResult = (HitResult)null;
        
        foreach (var traceable in Traceables)
        {
            var hitResult = traceable.Hit(ray, minT, maxT);

            if (hitResult is null) 
                continue;
            
            if (minHitResult is null)
                minHitResult = hitResult;
            else if (hitResult.T < minHitResult.T)
                minHitResult = hitResult;
        }

        return minHitResult;
    }
    
    public HitResult? CastOnFirstObstacle(Ray ray, float maxT, float minT = float.Epsilon)
    {
        foreach (var traceable in Traceables)
        {
            var hit = traceable.Hit(ray, minT, maxT);
            if (hit != null)
            {
                return hit;
            }
        }

        return null;
    }
}