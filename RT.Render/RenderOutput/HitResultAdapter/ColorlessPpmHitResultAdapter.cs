using RT.Math.LinearAlgebra;
using RT.Primitives.Traceable;

namespace RT.Render.RenderOutput.HitResultAdapter;

public class ColorlessPpmHitResultAdapter: IHitResultAdapter
{
    private readonly World _world;
    private readonly Vector3 _hitColor;
    private readonly Vector3 _backgroundColor;

    private static Vector3 White = new Vector3(1f, 1f, 1f);
    private static Vector3 Black = new Vector3(0f, 0f, 0f);

    public ColorlessPpmHitResultAdapter(World world, Vector3 hitColor, Vector3 backgroundColor)
    {
        _world = world;
        _hitColor = hitColor;
        _backgroundColor = backgroundColor;
    }

    public char[] ToChar(HitResult? hitResult)
    {
        if (hitResult is null) 
            return ToCharArray(_backgroundColor);
        
        
        var lightPercent = System.Math.Clamp(_world.Lights
            .Select(light => Vector3.Dot((Vector3) light.Origin, hitResult.Value.Normal))
            .Sum(), -1f, 1f);

        if (lightPercent >= 0)
        {
            var color =  Vector3.Lerp(_hitColor, White, lightPercent);

            return ToCharArray(color);
        }
        else
        {
            var color =  Vector3.Lerp(_hitColor, Black, System.Math.Abs(lightPercent));

            return ToCharArray(color);
        }

    }

    private static char[] ToCharArray(Vector3 color)
    {
        return $" {(int) (color.X * 255)} {(int) (color.Y * 255)} {(int) (color.Z * 255)} ".ToCharArray();
    }
}