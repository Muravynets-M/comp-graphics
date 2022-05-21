using System;
using RT.Math.LinearAlgebra;
using RT.Primitives.Primitive;
using Xunit;

namespace RT.Primitives.Test;

public class SphereTests
{
    private Sphere _sphere;

    public SphereTests()
    {
        _sphere = new Sphere((Point3) Vector3.Zero(), 2f);
    }

    [Fact]
    public void TestSphereIntersect()
    {
        var ray = new Ray(new Point3(2f, 2f, 2f), new Vector3(-1f, -1f, 0f));
        
        Assert.NotNull(_sphere.Hit(ray, 0f, 100f));
    }
    
    [Fact]
    public void TestSphereIntersectCorrectHitResult()
    {
        var ray = new Ray(new Point3(2f, 2f, 2f), new Vector3(-1f, -1f, 0f));

        var hitResult = _sphere.Hit(ray, 0f, 100f);

        Assert.NotNull(hitResult);
        Assert.Equal(new Point3(0f, 0f, 2f), hitResult!.Point);
        Assert.Equal(2, hitResult.T);
        Assert.Equal(new Vector3(0f, 0f, 1f), hitResult.Normal);
    }
    
    [Fact]
    public void TestSphereNotIntersectSmallT()
    {
        var ray = new Ray(new Point3(2f, 2f, 2f), new Vector3(-1f, -1f, 0f));
        
        Assert.Null(_sphere.Hit(ray, 0f, 0.5f));
    }
    
    [Fact]
    public void TestSphereNotIntersectWrongDirection()
    {
        var ray = new Ray(new Point3(2f, 2f, 2f), new Vector3(1f, 1f, 0f));
        
        Assert.Null(_sphere.Hit(ray, 0f, 100f));
    }
    
}