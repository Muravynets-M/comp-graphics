using RT.Math.LinearAlgebra;
using RT.Primitives.Traceable;
using RT.Primitives.Transform;
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
                var hitResult = world.Cast(camera.GetRay(
                    x / _imageBuffer.Width,
                    1f - y / _imageBuffer.Height));

                if (hitResult is not null)
                {
                    ProcessShades(world, hitResult);
                }

                _imageBuffer.Write(_hitResultAdapter.ToChar(hitResult));
            }
        }
    }

    private void ProcessShades(World world, HitResult hitResult)
    {
        var list = new List<ITransform>();
        foreach (var light in world.Lights)
        {
            var direction = light.Origin - hitResult.Point;

            // the figure is shadowed by itself
            if (Vector3.Dot(direction, hitResult.Normal) <= 0)
            {
                continue;
            }

            // to remove the possibility of ray hitting the same figure
            var start = (Point3) (hitResult.Point + hitResult.Normal * 0.00001f);
            var ray = new Ray(start, direction);

            var hitResultLight = world.CastOnFirstObstacle(ray, float.PositiveInfinity);
            if (hitResultLight is null)
                list.Add(light);
        }
        
        if (!list.Any())
            return;

        hitResult.LightDotProduct = System.Math.Clamp(
            list.Aggregate(
                0f, (f, light) =>
                    f + Vector3.Dot(Vector3.Unit((Vector3) light.Origin), hitResult.Normal)),
            0f,
            1f
        );
    }
}