using RT.Math;
using RT.Math.LinearAlgebra;
using RT.Primitives.Traceable;

namespace RT.Light;

public class EnvironmentalLight : LightSource
{
    private RandomDotOnSemisphere _random;
    
    public EnvironmentalLight(ColorRGB color, float intensity) : base(color, intensity)
    {
        _random = new RandomDotOnSemisphere();
    }
    
    public override float CastLightOnSurface(Point3 point, Vector3 surfNormal, ITraceableCollection world)
    {
        float sum = 0;
        float samples = 3;
        for (var i = 0; i < samples; i++)
        {
            sum += CastSingleRayOnSurface(point, surfNormal, world);
        }

        return sum / samples;
    }

    private float CastSingleRayOnSurface(Point3 point, Vector3 surfNormal, ITraceableCollection world)
    {
        var direction = _random.NextVector3(surfNormal);

        return LightUpSurface(direction, point, surfNormal, world);
    }
}