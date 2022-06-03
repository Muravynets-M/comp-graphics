using RT.Math.LinearAlgebra;
using RT.Primitives.Traceable;

namespace RT.Light;

public abstract class Light
{
    public ColorRGB Color {get;}
    public float Intensity { get; }
    protected static readonly ColorRGB Black = new ColorRGB(0, 0, 0);

    public Light(ColorRGB color, float intensity)
    {
        Intensity = intensity;
        Color = color;
    }

    public abstract float CastLightOnSurface(Point3 point, Vector3 surfNormal, ITraceableCollection world);
    protected float LightUpSurface(Vector3 direction, Point3 point, Vector3 surfNormal, ITraceableCollection world)
    {
        var ray = new Ray(point, direction);
        var intensity = Intensity;
        var hitResultLight = world.CastOnFirstObstacle(ray, float.PositiveInfinity);
        if (hitResultLight is not null)
            intensity = 0;

        var lightDotProduct = Vector3.Dot(direction, surfNormal);
        
        return lightDotProduct * intensity;
    }
    // private void Methd()
    // {
        // dependent on light source
        // var direction = Vector3.Unit(light.Origin - hitResult.Point);

        // could be dependent too
        // lightPercent += Vector3.Dot(direction, hitResult.Normal);
    // }
}