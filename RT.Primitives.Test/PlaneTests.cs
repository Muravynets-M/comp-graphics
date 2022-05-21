using RT.Math.LinearAlgebra;
using RT.Primitives.Primitive;
using Xunit;

namespace RT.Primitives.Test;

public class PlaneTests
{
    private Plane _plane;
    
    public PlaneTests()
    {
    _plane = new Plane(new Point3(1, 0, 0), new Point3(0, 1, 0), new Point3(0, 0, 1));
    }

    [Fact]
    public void TestRayHitsPlane()
    {
        var o = new Point3(5, 0, 0);
        var ray = new Ray(o, Vector3.FromPoints(o, new Point3(3, 0, 0)));

        var result = _plane.Hit(ray, 0, 100);
        
        Assert.NotNull(result);
        Assert.Equal(new Point3(1, 0, 0), result!.Point);
        
        Assert.Equal(577, System.Math.Floor(result.Normal.X * 1000)); 
        Assert.Equal(577, System.Math.Floor(result.Normal.Y * 1000));
        Assert.Equal(577, System.Math.Floor(result.Normal.Z * 1000));
       
        Assert.Equal(2, result.T);
        
        Assert.Equal( Vector3.Unit(new Vector3(1, 1, 1)), result.Normal);
    }

    [Fact]
    public void TestRayHitsPlaneTooLowMargin()
    {
        var o = new Point3(5, 0, 0);
        var ray = new Ray(o, Vector3.FromPoints(o, new Point3(3, 0, 0)));

        var result = _plane.Hit(ray, 0, 5);
    }
    
    [Fact]
    public void TestRayInOtherDirection()
    {
        var o = new Point3(0, 0, 0);
        var ray = new Ray(o, Vector3.FromPoints(o, new Point3(-1, 0, 0)));
        
        Assert.Null(_plane.Hit(ray, 0, 100));
    }
    
    [Fact]
    public void TestRayIsParallel()
    {
        var pA = new Point3(1, 0, -1);
        var ray = new Ray(pA, Vector3.FromPoints(pA, new Point3(-1, 0, 1)));
        
        Assert.Null(_plane.Hit(ray, 0, 100));
    }
    
    [Fact]
    public void TestRayIsParallelIncludedInPlane()
    {
        var ray = new Ray(_plane.PointA, Vector3.FromPoints(_plane.PointA, _plane.PointC));
        
        Assert.Null(_plane.Hit(ray, 0, 100));
    }

    [Fact]
    public void TestApplyTransformation()
    {
        var plane = new Plane(new Point3(1, 0, 0), new Point3(0, 1, 0), new Point3(0, 0, 1));

        var t = Matrix4x4.GetTranslationMatrix(1, 2, 3);
        plane.ApplyTransformation(t);
        
        Assert.Equal(new Point3(2, 2, 3), plane.PointA);
        Assert.Equal(new Point3(1, 3, 3), plane.PointB);
        Assert.Equal(new Point3(1, 2, 4), plane.PointC);

        var n = plane.Normal;
        t = Matrix4x4.GetScaleMatrix(1, 2, 3);
        plane.ApplyTransformation(t);
        
        Assert.NotEqual(n, plane.Normal);
    }
}