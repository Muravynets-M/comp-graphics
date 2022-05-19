using RT.Math.LinearAlgebra;
using RT.Primitives;
using Xunit;

namespace RT.Render.Test;

public class WorldTests
{
    [Fact]
    public void TestFindFirstObject()
    {
        var world = new World();
        
        var normal = new Vector3(0, 0, 1);
        world.Traceables.Add(Plane.PlaneFromNormal(normal, new Point3(0, 0, 0)));
        world.Traceables.Add(Plane.PlaneFromNormal(normal, new Point3(0, 0, -10)));
        world.Traceables.Add(Plane.PlaneFromNormal(normal, new Point3(0, 0, -20)));
        world.Traceables.Add(Plane.PlaneFromNormal(normal, new Point3(0, 0, 20)));

        var result = world.Cast(new Ray(new Point3(0, 0, 10), -normal));
        Assert.NotNull(result);
        Assert.Equal(new Point3(0,0,0), result!.Point);
    }
    
    [Fact]
    public void TestFindAnyObject()
    {
        var world = new World();
        
        var normal = new Vector3(0, 0, 1);
        
        world.Traceables.Add(Plane.PlaneFromNormal(normal, new Point3(0, 0, -10)));
        world.Traceables.Add(Plane.PlaneFromNormal(normal, new Point3(0, 0, -20)));
        world.Traceables.Add(Plane.PlaneFromNormal(normal, new Point3(0, 0, 0)));
        world.Traceables.Add(Plane.PlaneFromNormal(normal, new Point3(0, 0, 20)));
        

        var result = world.CastOnFirstObstacle(new Ray(new Point3(0, 0, 10), -normal), 30);
        Assert.NotNull(result);
        Assert.Equal(new Point3(0,0,-10), result!.Point);
    }
    
    [Fact]
    public void TestFindAnyObject_LowMargin()
    {
        var world = new World();
        
        var normal = new Vector3(0, 0, 1);
        
        world.Traceables.Add(Plane.PlaneFromNormal(normal, new Point3(0, 0, -10)));
        world.Traceables.Add(Plane.PlaneFromNormal(normal, new Point3(0, 0, -20)));
        world.Traceables.Add(Plane.PlaneFromNormal(normal, new Point3(0, 0, 0)));
        world.Traceables.Add(Plane.PlaneFromNormal(normal, new Point3(0, 0, 20)));
        

        var result = world.CastOnFirstObstacle(new Ray(new Point3(0, 0, 0), -normal), 15);
        Assert.NotNull(result);
        Assert.Equal(new Point3(0,0,-10), result!.Point);
    }
}