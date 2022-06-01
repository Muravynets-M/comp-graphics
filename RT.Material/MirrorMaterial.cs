using System.Drawing;
using RT.Math.LinearAlgebra;
using RT.Primitives.Material;
using RT.Primitives.Traceable;

namespace RT.Material;

public class MirrorMaterial : IMaterial
{
    private readonly Vector3 _tint;

    private static readonly Vector3 Black = new Vector3(0f, 0f, 0f);

    public MirrorMaterial(Vector3 tint)
    {
        _tint = tint;
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
                    recursionCount++).Color
            );
        }

        return new ColorResult(Black);
    }
    
    private ColorResult CombineColors(Vector3 reflectColor)
    {
        return new ColorResult(new Vector3(
            _tint.X * reflectColor.X,
            _tint.Y * reflectColor.Y,
            _tint.Z * reflectColor.Z
        ));
    }
}