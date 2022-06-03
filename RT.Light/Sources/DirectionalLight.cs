using RT.Math.LinearAlgebra;
using RT.Primitives.Traceable;

namespace RT.Light;

public class DirectionalLight : Light
{
    private Vector3 Direction;
    
    public DirectionalLight(Vector3 direction, ColorRGB color, float intensity) : base(color, intensity)
    {
        Direction = direction;
    }

    // public Ray Cast(Point3 point, Vector3 surfNormal)
    // {
    //     return new Ray(point, -Direction);
    // }

    public override float CastLightOnSurface(Point3 point, Vector3 surfNormal, ITraceableCollection world)
    {
        var direction = -Direction;

        return LightUpSurface(direction, point, surfNormal, world);
        // var ray = new Ray(point, direction);
        // var intensity = Intensity;
        // var hitResultLight = world.CastOnFirstObstacle(ray, float.PositiveInfinity);
        // if (hitResultLight is not null)
        //     intensity = 0;
        //
        // var lightDotProduct = Vector3.Dot(direction, surfNormal);
        //
        // return lightDotProduct * intensity;
    }
}