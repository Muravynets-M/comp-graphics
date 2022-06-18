using RT.Math.LinearAlgebra;
using RT.Primitives.Traceable;
using RT.Render.RenderOutput;

namespace RT.Render.Render;

public class MultipleRayRenderer : IRenderer
{
    private readonly IImageBuffer _imageBuffer;
    private readonly IHitResultAdapter _hitResultAdapter;
    private readonly int _rays;
    private readonly Vector3 _backgroundColor;

    public MultipleRayRenderer(IImageBuffer buffer, IHitResultAdapter hitResultAdapter, int rays,
        Vector3 backgroundColor)
    {
        _imageBuffer = buffer;
        _hitResultAdapter = hitResultAdapter;
        _rays = rays;
        _backgroundColor = backgroundColor;
    }

    public void Render(World world, Camera camera)
    {
        for (var y = 0f; y < _imageBuffer.Height; y++)
        {
            for (var x = 0f; x < _imageBuffer.Width; x++)
            {
                var colorSum = new ColorResult(Vector3.Zero());
                for (var r = 0f; r < _rays; r++)
                {
                    var ray = camera.GetSlightlyRandomRay(
                        x / _imageBuffer.Width,
                        1f - y / _imageBuffer.Height);

                    var hitResult = world.Cast(ray);

                    var colorResult = hitResult?.Material?.CalculateColor(ray, hitResult, world);
                    if (colorResult is not null)
                    {
                        colorSum = new ColorResult(colorSum.Color + colorResult.Color);
                    }
                    else
                    {
                        colorSum = new ColorResult(colorSum.Color + _backgroundColor);
                    }
                }

                _imageBuffer.Write(
                    _hitResultAdapter.ToChar(new ColorResult(colorSum.Color / _rays)));
            }
        }
    }
}