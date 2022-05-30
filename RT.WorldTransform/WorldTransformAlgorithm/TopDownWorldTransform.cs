using RT.Math.LinearAlgebra;
using RT.Primitives.Primitive;
using RT.Primitives.Traceable;
using RT.Primitives.Transform;
using RT.Render;
using RT.Render.WorldTransformAlgorithm;

namespace RT.WorldTransform.WorldTransformAlgorithm;

public class TopDownWorldTransform: IWorldTransformAlgorithm
{
    private int Edge;
    public TopDownWorldTransform(int edge)
    {
        Edge = edge;
    }
 public World Transform(World world)
 {
     return new World()
        {
            Lights = new List<ITransform>(world.Lights),
            Traceables = new List<ITraceable>
            {
                BuildTree(world.Traceables)
            }
        };
    }

     private Box BuildTree(List<ITraceable> traceables)
     {
         if (traceables.Count <= Edge)
             return new Box(traceables);

         var d= DivideTraceables(traceables);
         
         return new Box(new List<ITraceable> {BuildTree(d.Left), BuildTree(d.Right)});
     }

    private class DivisionResult
    {
        public List<ITraceable> Left;
        public List<ITraceable> Right;
        
        public DivisionResult(List<ITraceable> left, List<ITraceable> right)
        {
            Left = left;
            Right = right;
        }
    }
 
    private DivisionResult DivideTraceables(List<ITraceable> traceables)
    {
        var first = traceables.First();
        
        var maxX = first.Origin.X;
        var maxY = first.Origin.Y;
        var maxZ = first.Origin.Z;
        
        var minX = first.Origin.X;
        var minY = first.Origin.Y;
        var minZ = first.Origin.Z;

        foreach (var item in traceables)
        {
            maxX = System.Math.Max(item.Origin.X, maxX);
            maxY = System.Math.Max(item.Origin.Y, maxY);
            maxZ = System.Math.Max(item.Origin.Z, maxZ);
        
            minX = System.Math.Min(item.Origin.X, minX);
            minY = System.Math.Min(item.Origin.Y, minY);
            minZ = System.Math.Min(item.Origin.Z, minZ);
        }

        var dx = maxX - minX;
        var dy = maxY - minY;
        var dz = maxZ - minZ;
        
        Predicate<ITraceable> isLess = _ => false;

        // divide by x
        if (dx > dy && dx > dz)
        {
            var mid = Point3.DivideSegmentMtoN(minX, maxX, 1, 1);
            
            isLess = tr => tr.Origin.X < mid;
        }
        // divide by y
        else if (dy > dx && dy > dz)
        {
            var mid = Point3.DivideSegmentMtoN(minY, maxY, 1, 1);
            
            isLess = tr => tr.Origin.Y < mid;
        }
        // divide by z
        else if (dz > dx && dz > dy)
        {
            var mid = Point3.DivideSegmentMtoN(minZ, maxZ, 1, 1);

            isLess = tr => tr.Origin.Z < mid;
        }
        
        return Divide(traceables, isLess);
    }

    private DivisionResult Divide(List<ITraceable> traceables, Predicate<ITraceable> isLess)
    {
        var left = new List<ITraceable>();
        var right = new List<ITraceable>();
        
        foreach (var item in traceables)
        {
            if (isLess.Invoke(item))
            {
                left.Add(item);
                continue;
            }
                
            right.Add(item);
        }

        return new DivisionResult(left, right);
    }
 
    public override string ToString()
    {
        return "top_down_" + Edge.ToString();
    }
}

