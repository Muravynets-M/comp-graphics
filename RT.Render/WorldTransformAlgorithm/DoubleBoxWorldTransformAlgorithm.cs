using RT.Math.LinearAlgebra;
using RT.Primitives.Primitive;
using RT.Primitives.Traceable;
using RT.Primitives.Transform;

namespace RT.Render.WorldTransformAlgorithm;

public class DoubleBoxWorldTransformAlgorithm: IWorldTransformAlgorithm
{
    public World Transform(World world)
    {
        return world;
    }

    public override string ToString()
    {
        return "double_box";
    }
}