using RT.Math.LinearAlgebra;
using RT.Primitives.Traceable;

namespace RT.Render.RenderOutput.HitResultAdapter;

public class AsciiHitResultAdapter: IHitResultAdapter
{
    public char[] ToChar(HitResult? hitResult)
    {
        if (hitResult?.LightSources == null) 
            return new[] {' '};
        
        var lightPercent = hitResult.LightSources
            .Select(light => Vector3.Dot((Vector3) light.Origin, hitResult.Normal))
            .Where(dot => dot > 0)
            .Sum();

        return lightPercent switch
        {
            <= 0 => new[] {' '},
            <= 0.2f => new[] {'.'},
            <= 0.5f => new[] {'*'},
            <= 0.8f => new[] {'O'},
            > 0.8f => new[] {'#'},
            _ => new[] {' '}
        };
    }
}