namespace RT.Render.WorldTransformAlgorithm;

public interface IWorldTransformAlgorithmFactory
{
    public IWorldTransformAlgorithm[] WorldTransformAlgorithms { get; }
}