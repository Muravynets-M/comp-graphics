using RT.Math.LinearAlgebra;
using RT.Primitives.Traceable;

namespace RT.Light;

public class PointLight: Light
{
    private readonly Point3 _point;
    
    public PointLight(Point3 p, ColorRGB color, float intensity) : base(color, intensity)
    {
        _point = p;
    }

    // public Ray Cast(Point3 p, Vector3 surfNormal)
    // {
    //     return new Ray(p, p - _point);
    // }

    public override float CastLightOnSurface(Point3 point, Vector3 surfNormal, ITraceableCollection world)
    {
        var direction = Vector3.Unit(_point - point);

        return LightUpSurface(direction, point, surfNormal, world);
        // var ray = new Ray(point, direction);
        // var intensity = Intensity;
        // var hitResultLight = world.CastOnFirstObstacle(ray, float.PositiveInfinity);
        // if (hitResultLight is not null)
        //     intensity = 0;
        //     // lightPercent += lightPercent >= 0 ? lightPercent * -0.7f : lightPercent * 0.7f;
        //
        // var lightDotProduct = Vector3.Dot(direction, surfNormal);
        // // var color = Vector3.Lerp(Color, Black, lightDotProduct * intensity);
        // return lightDotProduct * intensity;
    }
}