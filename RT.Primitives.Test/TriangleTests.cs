﻿using System;
using RT.Math.LinearAlgebra;
using RT.Primitives.Primitive;
using Xunit;

namespace RT.Primitives.Test;

public class TriangleTests
{
    
    [Fact]
    public void TestRayHitsInTriangle()
    {
        var triangle = new Triangle(new Point3(0, 0, 0), new Point3(2, 0, 0), new Point3(0, 4, 0));
        var o = new Point3(0, 0, 4);
        
        var ray = new Ray(o, Vector3.FromPoints(o, new Point3(1, 1, 0)));
        var result = triangle.Hit(ray, 0, 100);
        Assert.NotNull(result);
    }
    
    [Fact]
    public void TestRayHitsInTriangleTooLowMargin()
    {
        var triangle = new Triangle(new Point3(0, 0, 0), new Point3(2, 0, 0), new Point3(0, 4, 0));
        var o = new Point3(0, 0, 4);
        
        var ray = new Ray(o, Vector3.FromPoints(o, new Point3(1, 1, 0)));
        var result = triangle.Hit(ray, 0, 0.5f);
        Assert.Null(result);
    }
    
    [Fact]
    public void TestRayHitParameters()
    {
        var o = new Point3(5, 0, 0);
        var ray = new Ray(o, Vector3.FromPoints(o, new Point3(3, 0, 0)));
        var triangle = new Triangle(new Point3(1, 0, 0), new Point3(0, 1, 0), new Point3(0, 0, 1));


        var result = triangle.Hit(ray, 0, 100);
        
        Assert.NotNull(result);
        Assert.Equal(new Point3(1, 0, 0), result!.Point);
        
        Assert.Equal(577, System.Math.Floor(result.Normal.X * 1000)); 
        Assert.Equal(577, System.Math.Floor(result.Normal.Y * 1000));
        Assert.Equal(577, System.Math.Floor(result.Normal.Z * 1000));
       
        Assert.Equal(4, result.T);
    }
    
    [Fact]
    public void TestRayHitNormals()
    {
        var triangle = new Triangle(
            new Point3(0, 0, 0), new Point3(2, 0, 0), new Point3(0, 4, 0),
            new Vector3(0,0,1), new Vector3(1,0,0), new Vector3(0,1,0)
        );
        
        // vertex 1
        var o = new Point3(0, 0, 4);
        var ray = new Ray(o, Vector3.FromPoints(o, new Point3(0, 0, 0)));
        var result = triangle.Hit(ray, 0, 100);
        Assert.NotNull(result);
        Assert.Equal(new Vector3(0,0,1), result!.Normal);
        
        // vertex 2
        ray = new Ray(o, Vector3.FromPoints(o, new Point3(2, 0, 0)));
        result = triangle.Hit(ray, 0, 100);
        Assert.NotNull(result);
        Assert.Equal(new Vector3(1,0,0), result!.Normal);
        
        // vertex 3
        ray = new Ray(o, Vector3.FromPoints(o, new Point3(0, 4, 0)));
        result = triangle.Hit(ray, 0, 100);
        Assert.NotNull(result);
        
        Assert.True(MathF.Abs(result!.Normal.X - 0) < 0.00001);
        Assert.True(MathF.Abs(result!.Normal.Y - 1) < 0.00001);
        Assert.True(MathF.Abs(result!.Normal.Z - 0) < 0.00001);
        
        // edge middle
        ray = new Ray(o, Vector3.FromPoints(o, new Point3(0, 2, 0)));
        result = triangle.Hit(ray, 0, 100);
        Assert.NotNull(result);
        Assert.Equal(707, System.Math.Floor(result!.Normal.Z*1000));
        
        // edge middle
        ray = new Ray(o, Vector3.FromPoints(o, new Point3(1, 0, 0)));
        result = triangle.Hit(ray, 0, 100);
        Assert.NotNull(result);
        Assert.Equal(707, System.Math.Floor(result!.Normal.X*1000));
        
        // edge middle
        ray = new Ray(o, Vector3.FromPoints(o, new Point3(1, 2, 0)));
        result = triangle.Hit(ray, 0, 100);
        Assert.NotNull(result);
        Assert.Equal(707, System.Math.Floor(result!.Normal.Y*1000));
        
        // edge quarter
        ray = new Ray(o, Vector3.FromPoints(o, new Point3(0, 1, 0)));
        result = triangle.Hit(ray, 0, 100);
        Assert.NotNull(result);
        Assert.Equal(948, System.Math.Floor(result!.Normal.Z*1000));
        Assert.Equal(316, System.Math.Floor(result.Normal.Y*1000));
    }
    
    [Fact]
    public void TestRayHitsOnVertex()
    {
        var v1 = new Point3(0, 0, 0);
        var v2 = new Point3(2, 0, 0);
        var v3 = new Point3(0, 4, 0);
        var triangle = new Triangle(v1, v2, v3);
        var o = new Point3(0, 0, 4);
        
        var ray = new Ray(o, Vector3.FromPoints(o, v1));
        var result = triangle.Hit(ray, 0, 100);
        Assert.NotNull(result);
        
        ray = new Ray(o, Vector3.FromPoints(o, v2));
        result = triangle.Hit(ray, 0, 100);
        Assert.NotNull(result);
        
        ray = new Ray(o, Vector3.FromPoints(o, v3));
        result = triangle.Hit(ray, 0, 100);
        Assert.NotNull(result);
    }
    
    [Fact]
    public void TestRayHitsOnEdge()
    {
        var triangle = new Triangle(new Point3(0, 0, 0), new Point3(2, 0, 0), new Point3(0, 4, 0));
        var o = new Point3(0, 0, 4);
        
        var ray = new Ray(o, Vector3.FromPoints(o, new Point3(1, 0, 0)));
        var result = triangle.Hit(ray, 0, 100);
        Assert.NotNull(result);
        
        ray = new Ray(o, Vector3.FromPoints(o, new Point3(1, 2, 0)));
        result = triangle.Hit(ray, 0, 100);
        Assert.NotNull(result);
        
        ray = new Ray(o, Vector3.FromPoints(o, new Point3(0, 2, 0)));
        result = triangle.Hit(ray, 0, 100);
        Assert.NotNull(result);
    }
    
    [Fact]
    public void TestRayPassesTriangle()
    {
        var triangle = new Triangle(new Point3(0, 0, 0), new Point3(2, 0, 0), new Point3(0, 4, 0));
        var o = new Point3(0, 0, 4);
        
        var ray = new Ray(o, Vector3.FromPoints(o, new Point3(-1, 0, 0)));
        var result = triangle.Hit(ray, 0, 100);
        Assert.Null(result);
    }
    
    [Fact]
    public void TestRayInOtherDirection()
    {
        var triangle = new Triangle(new Point3(1, 0, 0), new Point3(0, 1, 0), new Point3(0, 0, 1));
        var o = new Point3(0, 0, 0);
        var ray = new Ray(o, Vector3.FromPoints(o, new Point3(-1, 0, 0)));
        
        Assert.Null(triangle.Hit(ray, 0, 100));
    }
    
    [Fact]
    public void TestRayIsParallel()
    {
        var triangle = new Triangle(new Point3(1, 0, 0), new Point3(0, 1, 0), new Point3(0, 0, 1));
        var pA = new Point3(1, 0, -1);
        var ray = new Ray(pA, Vector3.FromPoints(pA, new Point3(-1, 0, 1)));
        
        Assert.Null(triangle.Hit(ray, 0, 100));
    }
    
    [Fact]
    public void TestRayIsParallelIncludedInPlane()
    {
        var triangle = new Triangle(new Point3(1, 0, 0), new Point3(0, 1, 0), new Point3(0, 0, 1));
        var ray = new Ray(triangle.Vertex1, Vector3.FromPoints(triangle.Vertex1, triangle.Vertex3));
        
        Assert.Null(triangle.Hit(ray, 0, 100));
    }
    
    [Fact]
    public void TestApplyTransformation()
    {
        var triangle = new Triangle(new Point3(1, 0, 0), new Point3(0, 1, 0), new Point3(0, 0, 1));

        var t = Matrix4x4.GetTranslationMatrix(1, 2, 3);
        triangle.ApplyTransformation(t);
        
        Assert.Equal(new Point3(2, 2, 3), triangle.Vertex1);
        Assert.Equal(new Point3(1, 3, 3), triangle.Vertex2);
        Assert.Equal(new Point3(1, 2, 4), triangle.Vertex3);

        var n = triangle.Normal1;
        t = Matrix4x4.GetScaleMatrix(1, 2, 3);
        triangle.ApplyTransformation(t);
        
        Assert.NotEqual(n, triangle.Normal1);
        Assert.NotEqual(n, triangle.Normal2);
        Assert.NotEqual(n, triangle.Normal3);
    }
}