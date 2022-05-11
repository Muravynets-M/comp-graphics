using RT.Math.LinearAlgebra;
using RT.Primitives.Traceable;

namespace RT.Render.RenderOutput.HitResultAdapter;

public class ColorlessPpmHitResultAdapter: IHitResultAdapter
{
    private readonly Vector3 _hitColor;
    private readonly Vector3 _backgroundColor;

    private static Vector3 White = new Vector3(1f, 1f, 1f);
    private static Vector3 Black = new Vector3(0f, 0f, 0f);

    public ColorlessPpmHitResultAdapter(Vector3 hitColor, Vector3 backgroundColor)
    {
        _hitColor = hitColor;
        _backgroundColor = backgroundColor;
    }

    public char[] ToChar(HitResult? hitResult)
    {
        if (hitResult is null) 
            return ToCharArray(_backgroundColor);
        if (!hitResult.LightSources.Any())
            return ToCharArray(Black);

        var lightPercent = System.Math.Clamp(hitResult.LightSources
            .Select(light => 
                System.Math.Abs(Vector3.Dot(Vector3.Unit((Vector3) light.Origin), hitResult.Normal))).Sum(), 0f, 1f);
     
        var color =  Vector3.Lerp(_hitColor, White, lightPercent);

        return ToCharArray(color);
    }

    private static char[] ToCharArray(Vector3 color)
    {
        return $" {(int) (color.X * 255)} {(int) (color.Y * 255)} {(int) (color.Z * 255)} ".ToCharArray();
    }
}