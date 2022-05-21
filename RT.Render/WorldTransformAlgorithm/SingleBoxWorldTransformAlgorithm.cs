using RT.Primitives.Primitive;
using RT.Primitives.Traceable;
using RT.Primitives.Transform;

namespace RT.Render.WorldTransformAlgorithm;

public class SingleBoxWorldTransformAlgorithm: IWorldTransformAlgorithm
{
    public World Transform(World world)
    {
        return new World()
        {
            Lights = new List<ITransform>(world.Lights),
            Traceables = new List<ITraceable>(){new Box(world.Traceables)}
        };
    }

    public override string ToString()
    {
        return "single_box";
    }
}