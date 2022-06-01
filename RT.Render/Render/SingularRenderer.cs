using RT.Render.RenderOutput;

namespace RT.Render.Render;

public class SingularRenderer : IRenderer
{
    private readonly IImageBuffer _imageBuffer;
    private readonly IHitResultAdapter _hitResultAdapter;

    public SingularRenderer(IImageBuffer buffer, IHitResultAdapter hitResultAdapter)
    {
        _imageBuffer = buffer;
        _hitResultAdapter = hitResultAdapter;
    }

    public void Render(World world, Camera camera)
    {
        for (var y = 0f; y < _imageBuffer.Height; y++)
        {
            for (var x = 0f; x < _imageBuffer.Width; x++)
            {
                var ray = camera.GetRay(
                    x / _imageBuffer.Width,
                    1f - y / _imageBuffer.Height);
                
                var hitResult = world.Cast(ray);

                var colorResult = hitResult?.Material?.CalculateColor(ray, hitResult, world);
                
                _imageBuffer.Write(_hitResultAdapter.ToChar(colorResult));
            }
        }
    }
}