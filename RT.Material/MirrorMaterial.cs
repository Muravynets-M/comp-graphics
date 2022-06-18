using System.Drawing;
using RT.Math.LinearAlgebra;
using RT.Primitives.Material;
using RT.Primitives.Traceable;
using RT.Texture;

namespace RT.Material;

public class MirrorMaterial : IMaterial
{
    private IColorTexture TintTexture { get; }

    private static readonly Vector3 Black = new Vector3(0f, 0f, 0f);

    public MirrorMaterial(IColorTexture tintTexture)
    {
        TintTexture = tintTexture;
    }

    public ColorResult CalculateColor(
        Ray originalRay,
        HitResult hitResult,
        ITraceableCollection world,
        int recursionCount = 0
    )
    {
        if (recursionCount > 8)
            return new ColorResult(Black);

        var reflectDirection = hitResult.Point + originalRay.Direction + hitResult.Normal * 2;

        var reflectRay = new Ray(hitResult.Point, reflectDirection);

        var reflectHitResult = world.Cast(reflectRay);

        if (reflectHitResult is not null)
        {
            return CombineColors(
                reflectHitResult.Material!.CalculateColor(
                    reflectRay,
                    reflectHitResult,
                    world,
                    recursionCount++).Color,
                TintTexture.GetUVColor(hitResult.UVcoordinates)
            );
        }

        return new ColorResult(Black);
    }
    
    private ColorResult CombineColors(Vector3 reflectColor, Vector3 tint)
    {
        return new ColorResult(new Vector3(
            tint.X * reflectColor.X,
            tint.Y * reflectColor.Y,
            tint.Z * reflectColor.Z
        ));
    }
}