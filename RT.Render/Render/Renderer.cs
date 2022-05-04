using RT.Render.RenderOutput.HitResultAdapter;
using RT.Render.RenderOutput.ImageBuffer;

namespace RT.Render.Render;

public class Renderer: Render.IRenderer
{
    private readonly IImageBuffer _imageBuffer;
    private readonly IHitResultAdapter _hitResultAdapter;

    public Renderer(IImageBuffer buffer, IHitResultAdapter hitResultAdapter)
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
                
                var hitResult = world.Cast(camera.GetRay(
                    x / _imageBuffer.Width, 
                    1f - y / _imageBuffer.Height));
                if (hitResult is not null)
                    hitResult.LightSources = world.Lights;
                
                _imageBuffer.Write(_hitResultAdapter.ToChar(hitResult));
            }
        }
    }
}