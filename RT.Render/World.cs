using RT.Math.LinearAlgebra;
using RT.Primitives.Traceable;
using RT.Primitives.Transform;

namespace RT.Render;

public class World
{
    public static Vector3 Up = new(0.0f, 1.0f, 0.0f);
    public static Vector3 Right = new(1.0f, 0.0f, 0.0f);
    public static Vector3 Forward = new(0.0f, 1.0f, -1.0f);
    
    public ITransform Light;

    private List<ITransform> _items = new();

    private List<ITraceable> _traceables = new();

    public void Place(ITransform item)
    {
        _items.Add(item);

        if (item is ITraceable traceable)
        {
            _traceables.Add(traceable);
        }
    }

    public HitResult? Cast(Ray ray, float minT = float.Epsilon, float maxT = float.PositiveInfinity)
    {
        return _traceables
            .Select(traceable => traceable.Hit(ray, minT, maxT))
            .MinBy(hitResult => hitResult?.T);
    }
}