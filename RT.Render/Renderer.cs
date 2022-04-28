using RT.Math.LinearAlgebra;
using RT.Render.RenderOutput;

namespace RT.Render;

public class Renderer
{
    private readonly Camera _camera;
    private readonly World _world;
    private readonly IPrinter _printer;

    public Renderer(Camera camera, World world, IPrinter printer)
    {
        _camera = camera;
        _world = world;
        _printer = printer;
    }

    public void Render()
    {
        for (var y = 0f; y < _printer.Height; y++)
        {
            for (var x = 0f; x < _printer.Width; x++)
            {
                
                var hitResult = _world.Cast(_camera.GetRay(
                    x / _printer.Width, 
                    1f - y / _printer.Height));

                var color = 0f;
                if (hitResult is not null)
                {
                    if (_world.Light is not null)
                    {
                        color = Vector3.Dot((Vector3) _world.Light.Origin, hitResult.Value.Normal);
                        color += 0.5f / (color + 1.7f);
                    }
                    else
                    {
                        color = 1f;
                    }
                }

                _printer.Print(color);
            }
        }
    }
}