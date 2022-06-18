using RT.Math.LinearAlgebra;
using RT.Primitives.Material;
using RT.Primitives.Traceable;
using RT.Texture.Color;

namespace RT.Material;

public class LuminousMaterial : IMaterial
{
    private OneColorTexture ColorTexture { get; }

    public LuminousMaterial(OneColorTexture colorTexture)
    {
        ColorTexture = colorTexture;
    }

    public ColorResult CalculateColor(Ray originalRay, HitResult hitResult, ITraceableCollection world,
        int recursionCount = 0)
    {
        return new ColorResult(ColorTexture.Color);
    }
}