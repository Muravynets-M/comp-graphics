using RT.Math.LinearAlgebra;
using RT.Primitives.Traceable;
using RT.Primitives.Transform;
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
            var ray = new Ray(hitResult.Point, direction);
            var hitResultLight = world.Cast(ray, 0.000001f);

            if (hitResultLight is not null && (hitResultLight.Point - hitResult.Point).Lenght < direction.Lenght)
                list.Add(light);
        }
        
        hitResult.LightSources = hitResult.LightSources.Where(light => !list.Contains(light));
    }
    
    
    // alternative shades processing, possible it will be needed in future, if light source will be not a point
    // private void ProcessShades2(World world, HitResult hitResult)
    // {
    //     var list = new List<ITransform>();
    //     foreach (var light in hitResult.LightSources)
    //     {
    //         var direction = light.Origin - hitResult.Point;
    //         var ray = new Ray(hitResult.Point, direction);
    //         var hitResultLight = world.Cast(ray, 0.000001f);
    //         
    //         var hitResultLightCheck = light.Hit(ray, float.Epsilon, float.PositiveInfinity);
    //         const float epsilon = 0.0000001f;
    //         if (hitResultLight is not null && !nearlyEqual(hitResultLight.T, hitResultLightCheck!.T, epsilon)
    //             && hitResultLight.T < hitResultLightCheck!.T)
    //         list.Add(light);
    //     }
    //     
    //     hitResult.LightSources = hitResult.LightSources.Where(light => !list.Contains(light));
    // }
    //
    //
    // public static bool nearlyEqual(float a, float b, float epsilon) {
    //     float absA = System.Math.Abs(a);
    //     float absB = System.Math.Abs(b);
    //     float diff = System.Math.Abs(a - b);
    //
    //     if (a == b) { // shortcut, handles infinities
    //         return true;
    //     } else if (a == 0 || b == 0 || (absA + absB < float.MinValue)) {
    //         // a or b is zero or both are extremely close to it
    //         // relative error is less meaningful here
    //         return diff < (epsilon * float.MinValue);
    //     } else { // use relative error
    //         return diff / System.Math.Min((absA + absB), float.MaxValue) < epsilon;
    //     }
    // }
}