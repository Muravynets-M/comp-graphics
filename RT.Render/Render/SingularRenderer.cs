using RT.Math.LinearAlgebra;
using RT.Primitives.Traceable;
using RT.Primitives.Transform;
using RT.Render.RenderOutput.HitResultAdapter;
using RT.Render.RenderOutput.ImageBuffer;

namespace RT.Render.Render;

public class SingularRenderer: IRenderer
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
                    hitResult.LightSources = world.Lights;
                
                    ProcessShades(world, hitResult);
                }

                _imageBuffer.Write(_hitResultAdapter.ToChar(hitResult));
            }
        }
    }

    private void ProcessShades(World world, HitResult hitResult)
    {
        var list = new List<ITransform>();
        foreach (var light in hitResult.LightSources)
        {
            var direction = light.Origin - hitResult.Point;
            
            // the figure is shadowed by itself
            // if (Vector3.Dot(direction, hitResult.Normal) <= 0)
            // {
            //     list.Add(light);
            //     continue;
            // }
            
            // to remove the possibility of ray hitting the same figure
            var start = (Point3) (hitResult.Point + hitResult.Normal * 0.000001f);
            var ray = new Ray(start, direction);
            
            var hitResultLight = world.CastOnFirstObstacle(ray, hitResult.T);
            if (hitResultLight is not null)
                list.Add(light);
        }
        
        hitResult.LightSources = hitResult.LightSources.Where(light => !list.Contains(light));
    }
}