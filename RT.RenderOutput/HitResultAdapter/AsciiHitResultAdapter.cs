using RT.Math.LinearAlgebra;
using RT.Primitives.Traceable;
using RT.Render.RenderOutput;

namespace RT.RenderOutput.HitResultAdapter;

public class AsciiHitResultAdapter: IHitResultAdapter
{
    public char[] ToChar(HitResult? hitResult)
    {
        if (hitResult?.LightDotProduct is null) 
            return new[] {' '};

        return hitResult.LightDotProduct switch
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