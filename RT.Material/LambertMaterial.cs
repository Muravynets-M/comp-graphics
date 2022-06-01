using RT.Math.LinearAlgebra;
using RT.Primitives.Material;
using RT.Primitives.Traceable;

namespace RT.Material;

public class LambertMaterial : IMaterial
{
    private readonly Vector3 _color;
    private static readonly Vector3 White = new Vector3(1f, 1f, 1f);
    private static readonly Vector3 Black = new Vector3(0f, 0f, 0f);

    public LambertMaterial(Vector3 color)
    {
        _color = color;
    }

    public ColorResult CalculateColor(Ray originalRay, HitResult hitResult, ITraceableCollection world,
        int recursionCount = 0)
    {
        var shades = ProcessShades(world, hitResult);
       
        return new ColorResult(shades >= 0f
                ? Vector3.Lerp(_color, White, 0.8f * shades)
                : Vector3.Lerp(_color, Black, -shades),
            shades);
    }

    private static float ProcessShades(ITraceableCollection world, HitResult hitResult)
    {
        var lightPercent = 0f;
        foreach (var light in world.Lights)
        {
            var direction = Vector3.Unit(light.Origin - hitResult.Point);

            lightPercent += Vector3.Dot(direction, hitResult.Normal);

            // to remove the possibility of ray hitting the same figure
            var start = (Point3) (hitResult.Point + hitResult.Normal * 0.00001f);
            var ray = new Ray(start, direction);

            var hitResultLight = world.CastOnFirstObstacle(ray, float.PositiveInfinity);
            if (hitResultLight is not null)
                lightPercent += lightPercent >= 0 ? lightPercent * -0.7f : lightPercent * 0.7f;
        }

        return lightPercent;
    }
}