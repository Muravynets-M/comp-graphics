using RT.Math.LinearAlgebra;
using RT.Primitives.Traceable;
using RT.Render.RenderOutput;

namespace RT.RenderOutput.HitResultAdapter;

public class MaterialPpmColorResultAdapter: IHitResultAdapter
{
    private readonly Vector3 _backgroundColor;

    public MaterialPpmColorResultAdapter(Vector3 backgroundColor)
    {
        _backgroundColor = backgroundColor;
    }
    public char[] ToChar(ColorResult? hitResult)
    {
        return ToCharArray(hitResult?.Color ?? _backgroundColor);
    }
    
    private static char[] ToCharArray(Vector3 color)
    {
        return $" {(int) (color.X * 255)} {(int) (color.Y * 255)} {(int) (color.Z * 255)} ".ToCharArray();
    }
}