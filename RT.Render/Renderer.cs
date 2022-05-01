using RT.Math.LinearAlgebra;
using RT.Render.RenderOutput;
using RT.Render.RenderOutput.HitResultAdapter;
using RT.Render.RenderOutput.ImageWriter;

namespace RT.Render;

public class Renderer
{
    private readonly World _world;
    private readonly IImageBuffer _imageBuffer;
    private readonly IHitResultAdapter _hitResultAdapter;

    public Renderer(World world, IImageBuffer buffer, IHitResultAdapter hitResultAdapter)
    {
        _world = world;
        _imageBuffer = buffer;
        _hitResultAdapter = hitResultAdapter;
    }

    public void Render(Camera camera)
    {
        for (var y = 0f; y < _imageBuffer.Height; y++)
        {
            for (var x = 0f; x < _imageBuffer.Width; x++)
            {
                
                var hitResult = _world.Cast(camera.GetRay(
                    x / _imageBuffer.Width, 
                    1f - y / _imageBuffer.Height));
                
                _imageBuffer.Write(_hitResultAdapter.ToChar(hitResult));
            }
        }
    }
}