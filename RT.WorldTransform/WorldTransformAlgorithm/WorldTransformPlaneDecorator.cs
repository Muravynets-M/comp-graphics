using System.Net;
using RT.Primitives.Primitive;
using RT.Primitives.Traceable;
using RT.Render;
using RT.Render.WorldTransformAlgorithm;

namespace RT.WorldTransform.WorldTransformAlgorithm;

public class WorldTransformPlaneDecorator: IWorldTransformAlgorithm
{
    private readonly IWorldTransformAlgorithm decorated;
    
    public WorldTransformPlaneDecorator(IWorldTransformAlgorithm algorithm)
    {
        decorated = algorithm;
    }
    
    public World Transform(World world)
    {
        var traceables = new List<ITraceable>();
        var plane = new List<ITraceable>();
        
        foreach (var obj in world.Traceables)
        {
            if (obj is Plane)
            {
                plane.Add((obj as Plane)!);
                continue;
            }
            
            traceables.Add(obj);
        }

        var w = new World
        {
            Lights = world.Lights,
            Traceables = traceables,
        };

        var wTransformed = decorated.Transform(w);
        wTransformed.Traceables.AddRange(plane);

        // var wTransformed = new World()
        // {
        //     Lights = world.Lights,
        //     Traceables = plane
        // };
        
        return wTransformed;
    }
    
    public override string ToString()
    {
        return decorated.ToString();
    }
}