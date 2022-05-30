using RT.Render.WorldTransformAlgorithm;

namespace RT.WorldTransform;

public class WorldTransformAlgorithmFactory: IWorldTransformAlgorithmFactory
{
    public WorldTransformAlgorithmFactory(IWorldTransformAlgorithm[] worldTransformAlgorithms)
    {
        WorldTransformAlgorithms = worldTransformAlgorithms;
    }

    public IWorldTransformAlgorithm[] WorldTransformAlgorithms { get; }
}