using RT.Primitives.Traceable;
using RT.Primitives.Transform;
using RT.Render;
using RT.Render.WorldTransformAlgorithm;

namespace RT.WorldTransform.WorldTransformAlgorithm;

public class StubWorldTransformAlgorithm: IWorldTransformAlgorithm
{
    public World Transform(World world)
    {
        return new World()
        {
            Lights = new List<ITransform>(world.Lights),
            Traceables = new List<ITraceable>(world.Traceables)
        };
    }

    public override string ToString()
    {
        return "nothing_lol";
    }
}