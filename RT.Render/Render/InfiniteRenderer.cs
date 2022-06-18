using System.Diagnostics;
using RT.Math.LinearAlgebra;
using RT.Render.RenderOutput;
using RT.Render.WorldTransformAlgorithm;

namespace RT.Render.Render;

public class InfiniteRenderer : IRenderer
{
    private readonly IImageBufferFactory _bufferFactory;
    private readonly IHitResultAdapter _hitResultAdapter;
    private readonly IWorldTransformAlgorithmFactory _worldAlgorithms;
    private readonly Vector3 _backgroundColor;

    public InfiniteRenderer(
        IImageBufferFactory bufferFactory,
        IHitResultAdapter hitResultAdapter,
        IWorldTransformAlgorithmFactory worldAlgorithms,
        Vector3 backgroundColor
    )
    {
        _bufferFactory = bufferFactory;
        _hitResultAdapter = hitResultAdapter;
        _worldAlgorithms = worldAlgorithms;
        _backgroundColor = backgroundColor;
    }

    public void Render(World world, Camera camera)
    {
        foreach (var worldAlgorithm in _worldAlgorithms.WorldTransformAlgorithms)
        {
            var renderer = new MultipleRayRenderer(_bufferFactory.BuildImageBuffer(worldAlgorithm.ToString()!),
                _hitResultAdapter, 10, _backgroundColor);
            var watch = new Stopwatch();
            watch.Start();

            var transformedWorld = worldAlgorithm.Transform(world);
            renderer.Render(transformedWorld, camera);

            watch.Stop();

            Console.WriteLine($"Algorithm \"{worldAlgorithm}\" took {watch.Elapsed.Minutes} minutes {watch.Elapsed.Seconds} seconds {watch.Elapsed.Milliseconds} milliseconds");
        }
    }
}