using RT.Math.LinearAlgebra;
using RT.Primitives.Material;
using RT.Primitives.Traceable;
using RT.Texture;

namespace RT.Material;

public class LambertMaterial : IMaterial
{
    
    private static readonly Vector3 White = new Vector3(1f, 1f, 1f);
    private static readonly Vector3 Black = new Vector3(0f, 0f, 0f);

    private IColorTexture ColorTexture { get; }
    
    public LambertMaterial(IColorTexture colorTexture)
    {
        ColorTexture = colorTexture;
    }

    public ColorResult CalculateColor(Ray originalRay, HitResult hitResult, ITraceableCollection world,
        int recursionCount = 0)
    {
        var lights = ProcessLights(world, hitResult);

        Vector3 c = ColorTexture.GetUVColor(hitResult.UVcoordinates);
     
        var color = new Vector3(
            c.X * lights.lightColor.X,
            c.Y * lights.lightColor.Y,
            c.Z * lights.lightColor.Z
        ); 
        
        return new ColorResult(Vector3.Lerp(Black, color, lights.lightPercent), lights.lightPercent);
    }

    private struct Light
    {
        public float lightPercent;
        public Vector3 lightColor;

        public Light(Vector3 color, float percent)
        {
            lightPercent = percent;
            lightColor = color;
        }
    }
    
    private Light ProcessLights(ITraceableCollection world, HitResult hitResult)
    {
        var lightPercent = 0f;
        var c = Vector3.Zero();
    
        foreach (var light in world.Lights)
        {
            // to remove the possibility of ray hitting the same figure
            var start = (Point3) (hitResult.Point + hitResult.Normal * 0.00001f);

            var lp = light.CastLightOnSurface(start, hitResult.Normal, world);

            lightPercent += lp;
            c += ((Vector3) light.Color) * lp;
        }
        
        c /= world.Lights.Count;
        
        return new Light(c, lightPercent);
    }
}