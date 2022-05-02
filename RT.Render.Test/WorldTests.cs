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
        world.Place(Plane.PlaneFromNormal(normal, new Point3(0, 0, 0)));
        world.Place(Plane.PlaneFromNormal(normal, new Point3(0, 0, -10)));
        world.Place(Plane.PlaneFromNormal(normal, new Point3(0, 0, -20)));
        world.Place(Plane.PlaneFromNormal(normal, new Point3(0, 0, 20)));

        var result = world.Cast(new Ray(new Point3(0, 0, 10), -normal));
        Assert.NotNull(result);
        Assert.Equal(new Point3(0,0,0), result.Value.Point);
    }
}