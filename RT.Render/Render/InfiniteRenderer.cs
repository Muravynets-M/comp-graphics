using System.Diagnostics;
using RT.Render.RenderOutput.HitResultAdapter;
using RT.Render.RenderOutput.ImageBuffer;
using RT.Render.WorldTransformAlgorithm;

namespace RT.Render.Render;

public class InfiniteRenderer : IRenderer
{
    private readonly ImageBufferFactory _bufferFactory;
    private readonly IHitResultAdapter _hitResultAdapter;
    private readonly IWorldTransformAlgorithm[] _worldAlgorithms;

    public InfiniteRenderer(
        ImageBufferFactory bufferFactory,
        IHitResultAdapter hitResultAdapter,
        params IWorldTransformAlgorithm[] worldAlgorithms
    )
    {
        _bufferFactory = bufferFactory;
        _hitResultAdapter = hitResultAdapter;
        _worldAlgorithms = worldAlgorithms;
    }

    public void Render(World world, Camera camera)
    {
        foreach (var worldAlgorithm in _worldAlgorithms)
        {
            var renderer = new SingularRenderer(_bufferFactory.BuildImageBuffer(worldAlgorithm.ToString()!),
                _hitResultAdapter);
            var watch = new Stopwatch();
            watch.Start();

            var transformedWorld = worldAlgorithm.Transform(world);
            renderer.Render(transformedWorld, camera);

            watch.Stop();

            Console.WriteLine($"Algorithm \"{worldAlgorithm}\" took {watch.Elapsed.Minutes} minutes {watch.Elapsed.Seconds} seconds {watch.Elapsed.Milliseconds} milliseconds");
        }
    }
}