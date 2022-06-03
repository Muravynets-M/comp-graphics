using RT.Math.LinearAlgebra;
using RT.Primitives.Traceable;

namespace RT.Light;

public class EnvironmentalLight : LightSource
{
    private Random _random;
    
    public EnvironmentalLight(ColorRGB color, float intensity) : base(color, intensity)
    {
        _random = Random.Shared;
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
        var direction = GenerateHemisphereDirection(surfNormal);

        return LightUpSurface(direction, point, surfNormal, world);
    }

    private Vector3 GenerateHemisphereDirection(Vector3 polarVector)
    {
        var randDir = GenerateRandomDirectionOnSphere();
        
        // // check the same direction
        // if (Vector3.Dot(randDir, polarVector) < 0)
        // {
        //     // try to generate again
        //     return GenerateHemisphereDirection(polarVector);
        // }

        var dotProd = Vector3.Dot(randDir, polarVector);
        while (dotProd < 0)
        {
            randDir = GenerateHemisphereDirection(polarVector);
            dotProd = Vector3.Dot(randDir, polarVector);
        }

        return Vector3.Unit(randDir);
    }
    
    private Vector3 GenerateRandomDirectionOnSphere()
    {
        var random = new Random();
        var u = random.NextSingle();
        var v = random.NextSingle();

        var theta = 2 * MathF.PI * u;
        var phi = 1 / MathF.Cos(2 * v - 1);

        var x = MathF.Sqrt(1 - u * u) * MathF.Cos(theta);
        var y = MathF.Sqrt(1 - u * u) * MathF.Sin(theta);
        var z = MathF.Cos(phi);

        return new Vector3(x, y, z);
    }
}