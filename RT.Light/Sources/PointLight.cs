using RT.Math.LinearAlgebra;
using RT.Primitives.Traceable;

namespace RT.Light;

public class PointLight: LightSource
{
    private readonly Point3 _point;
    
    public PointLight(Point3 p, ColorRGB color, float intensity) : base(color, intensity)
    {
        _point = p;
    }

    public override float CastLightOnSurface(Point3 point, Vector3 surfNormal, ITraceableCollection world)
    {
        var direction = Vector3.Unit(_point - point);

        return LightUpSurface(direction, point, surfNormal, world);
    }
}