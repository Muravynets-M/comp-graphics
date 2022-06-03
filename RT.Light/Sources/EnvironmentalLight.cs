using RT.Math.LinearAlgebra;
using RT.Primitives.Traceable;

namespace RT.Light;

public class EnvironmentalLight : Light
{
    public EnvironmentalLight(ColorRGB color, float intensity) : base(color, intensity)
    {
    }
    
    public override float CastLightOnSurface(Point3 point, Vector3 surfNormal, ITraceableCollection world)
    {
        float sum = 0;
        for (var i = 0; i < 4; i++)
        {
            sum += CastSingleRayOnSurface(point, surfNormal, world);
        }

        return sum / 4;
    }

    private float CastSingleRayOnSurface(Point3 point, Vector3 surfNormal, ITraceableCollection world)
    {
        var direction = GenerateHemisphereDirection(surfNormal);

        return LightUpSurface(direction, point, surfNormal, world);
        var ray = new Ray(point, direction);
        var intensity = Intensity;
        var hitResultLight = world.CastOnFirstObstacle(ray, float.PositiveInfinity);
        if (hitResultLight is not null)
            intensity = 0;

        var lightDotProduct = Vector3.Dot(direction, surfNormal);
        
        return lightDotProduct * intensity;
    }
    
    // public Ray Cast(Point3 point, Vector3 surfNormal)
    // {
    //     var n = Vector3.Unit(surfNormal);
    //     var l = GenerateHemisphereDirection(n);
    //     
    //     return new Ray(point, l);
    // }

    private Vector3 GenerateHemisphereDirection(Vector3 polarVector)
    {
        var randDir = GenerateRandomDirectionOnSphere();
        
        // check the same direction
        if (Vector3.Dot(randDir, polarVector) < 0)
        {
            // try to generate again
            return GenerateHemisphereDirection(polarVector);
        }

        return randDir;
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