using RT.Light;
using RT.Math.LinearAlgebra;
using RT.Primitives.Light;
using RT.Primitives.Primitive;
using RT.Primitives.Traceable;
using RT.Primitives.Transform;
using RT.Render;
using RT.Render.WorldTransformAlgorithm;

namespace RT.WorldTransform.WorldTransformAlgorithm;

public class DoubleBoxWorldTransformAlgorithm: IWorldTransformAlgorithm
{
    // TopDownAlgorithm
    public World Transform(World world)
    {
        var first = world.Traceables.First();
        
        var maxX = first.MaxX;
        var maxY = first.MaxY;
        var maxZ = first.MaxZ;
        
        var minX = first.MinX;
        var minY = first.MinY;
        var minZ = first.MinZ;

        foreach (var item in world.Traceables)
        {
            maxX = System.Math.Max(item.Origin.X, maxX);
            maxY = System.Math.Max(item.Origin.X, maxY);
            maxZ = System.Math.Max(item.Origin.X, maxZ);
        
            minX = System.Math.Min(item.Origin.X, minX);
            minY = System.Math.Min(item.Origin.Y, minY);
            minZ = System.Math.Min(item.Origin.Z, minZ);
        }

        var dx = maxX - minX;
        var dy = maxY - minY;
        var dz = maxZ - minZ;

        var left = new List<ITraceable>();
        var right = new List<ITraceable>();
        
        // divide by x
        if (dx > dy && dx > dz)
        {
            var mid = Point3.DivideSegmentMtoN(minX, maxX, 1, 1);
            
            foreach (var item in world.Traceables)
            {
                if (item.Origin.X < mid)
                {
                    left.Add(item);
                    continue;
                }
                
                right.Add(item);
            }
        }
        
        // divide by y
        if (dy > dx && dy > dz)
        {
            var mid = Point3.DivideSegmentMtoN(minY, maxY, 1, 1);
            
            foreach (var item in world.Traceables)
            {
                if (item.Origin.Y < mid)
                {
                    left.Add(item);
                    continue;
                }
                
                right.Add(item);
            }
        }
        
        // divide by z
        if (dz > dx && dz > dy)
        {
            var mid = Point3.DivideSegmentMtoN(minZ, maxZ, 1, 1);

            foreach (var item in world.Traceables)
            {
                if (item.Origin.Z < mid)
                {
                    left.Add(item);
                    continue;
                }
                
                right.Add(item);
            }
        }

        return new World()
        {
            Lights = new List<ILight>(world.Lights),
            Traceables = new List<ITraceable>
            {
                new Box(
                    new List<ITraceable>{new Box(left), new Box(right)}
                )
            }
        };
    }

    public override string ToString()
    {
        return "double_box";
    }
}