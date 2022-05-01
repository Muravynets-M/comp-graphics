using RT.Math.LinearAlgebra;
using RT.Primitives.Traceable;

namespace RT.Render.RenderOutput.HitResultAdapter;

public class AsciiHitResultAdapter: IHitResultAdapter
{
    private readonly World _world;

    public AsciiHitResultAdapter(World world)
    {
        _world = world;
    }

    public char[] ToChar(HitResult? hitResult)
    {
        if (hitResult is not null)
        {
            var lightPercent = _world.Lights
                .Select(light => Vector3.Dot((Vector3) light.Origin, hitResult.Value.Normal))
                .Where(dot => dot > 0)
                .Sum();

            switch (lightPercent)
            {
                case <= 0:
                    return new[] {' '};
                case <= 0.2f:
                    return new[] {'.'};
                case <= 0.5f:
                    return new[] {'*'};
                case <= 0.8f:
                    return new[] {'O'};
                case > 0.8f:
                    return new[] {'#'};
            }
        }
        
        return new[] {' '};
    }
}