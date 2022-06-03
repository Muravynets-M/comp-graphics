using RT.Math.LinearAlgebra;
using RT.Primitives.Traceable;

namespace RT.Light;

public class DirectionalLight : LightSource
{
    private Vector3 Direction;
    
    public DirectionalLight(Vector3 direction, ColorRGB color, float intensity) : base(color, intensity)
    {
        Direction = Vector3.Unit(direction);
    }
    
    public override float CastLightOnSurface(Point3 point, Vector3 surfNormal, ITraceableCollection world)
    {
        var direction = -Direction;

        return LightUpSurface(direction, point, surfNormal, world);
    }
}