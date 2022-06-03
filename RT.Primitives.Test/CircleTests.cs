using System;
using RT.Math.LinearAlgebra;
using RT.Primitives.Primitive;
using Xunit;

namespace RT.Primitives.Test;

public class CircleTests
{
    private Circle _circle;
    
    public CircleTests()
    {
        var testedPlane = new Plane(new Point3(1, 0, 0), new Point3(0, 1, 0), new Point3(0, 0, 1));
        _circle = new Circle(testedPlane.Normal, 1, new Point3(1, 0, 0));
    }

    [Fact]
    public void TestRayHitsPlane()
    {
        var o = new Point3(5, 0, 0);
        var ray = new Ray(o, Vector3.FromPoints(o, new Point3(3, 0, 0)));
    
        var result = _circle.Hit(ray, 0, 100);
        
        Assert.NotNull(result);
        Assert.Equal(new Point3(1, 0, 0), result!.Point);
        
        Assert.Equal(577, System.Math.Floor(result.Normal.X * 1000)); 
        Assert.Equal(577, System.Math.Floor(result.Normal.Y * 1000));
        Assert.Equal(577, System.Math.Floor(result.Normal.Z * 1000));
       
        Assert.Equal(4, result.T);
    }
   
    [Fact]
    public void TestRayHitsPlaneOnBorder()
    {
        var circle = new Circle(new Vector3(0, 0, 1), 1, new Point3(0,0,0));
        var o = new Point3(0, 1, 1);
        var ray = new Ray(o, Vector3.FromPoints(o, new Point3(0, 1, 0)));
    
        var result = circle.Hit(ray, 0, 100);
        Assert.NotNull(result);
    }
    
    [Fact]
    public void TestRayHitsPlaneOutOfRadius()
    {
        var o = new Point3(5, 0, 0);
        var ray = new Ray(o, Vector3.FromPoints(o, new Point3(0, 0, -2)));
    
        var result = _circle.Hit(ray, 0, 100);
        Assert.Null(result);
    }
}