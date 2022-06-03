using RT.Primitives.Light;
using RT.Primitives.Primitive;
using RT.Primitives.Traceable;
using RT.Primitives.Transform;
using RT.Render;
using RT.Render.WorldTransformAlgorithm;

namespace RT.WorldTransform.WorldTransformAlgorithm;

public class SingleBoxWorldTransformAlgorithm: IWorldTransformAlgorithm
{
    public World Transform(World world)
    {
        return new World()
        {
            Lights = new List<ILight>(world.Lights),
            Traceables = new List<ITraceable>(){new Box(world.Traceables)}
        };
    }

    public override string ToString()
    {
        return "single_box";
    }
}