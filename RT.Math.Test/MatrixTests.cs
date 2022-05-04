using RT.Math.LinearAlgebra;
using Xunit;

namespace RT.Math.Test;

public class MatrixTests
{
    [Fact]
    public void TestGetElement()
    {
        var m = new Matrix4x4(new float[]
        {
            1, 2, 3, 4,
            5, 6, 7, 8,
            9, 10, 11, 12,
            13, 14, 15, 16
        });
        
        Assert.Equal(1, m.Get(0, 0));
        Assert.Equal(16, m.Get(3, 3));
        Assert.Equal(10, m.Get(2, 1));
    }

    [Fact]
    public void TestMatrixMultiplyScalar()
    {
        var m = new Matrix4x4(new float[]
        {
            1, 1, 1, 1,
            1, 1, 1, 1,
            1, 1, 1, 1,
            1, 1, 1, 1
        });

        var res = 6 * m;
        for (int r = 0; r < 4; r++)
        {
            for (int c = 0; c < 4; c++)
            {
                Assert.Equal(6, res.Get(r, c));
            }
        }
    }

    [Fact]
    public void TestMatrixMultiplyVector()
    {
        var m = new Matrix4x4(new float[]
        {
            1, 2, 3, 4,
            4, 3, 2, 1,
            1, 2, 3, 4,
            4, 3, 2, 1,
        });
        var v = new Vector4(4, 5, 6, 7);
        var res = m * v;

        Assert.Equal(60, res.X);
        Assert.Equal(50, res.Y);
        Assert.Equal(60, res.Z);
        Assert.Equal(50, res.Scaler);
    }
    
    [Fact]
    public void TestMatrixAddMatrix()
    {
        var m = new Matrix4x4(new float[]
        {
            1, 2, 3, 4,
            5, 6, 7, 8,
            9, 10, 11, 12,
            13, 14, 15, 16
        });

        var res = m + m;
        for (int r = 0; r < 4; r++)
        {
            for (int c = 0; c < 4; c++)
            {
                Assert.Equal(2*(r*4+c+1), res.Get(r, c));
            }
        }
    }
    
    [Fact]
    public void TestMatrixSubtractMatrix()
    {
        var m = new Matrix4x4(new float[]
        {
            1, 2, 3, 4,
            5, 6, 7, 8,
            9, 10, 11, 12,
            13, 14, 15, 16
        });

        var res = m - m;
        for (int r = 0; r < 4; r++)
        {
            for (int c = 0; c < 4; c++)
            {
                Assert.Equal(0, res.Get(r, c));
            }
        }
    }
    
    [Fact]
    public void TestMatrixMultiplyMatrix()
    {
        var m1 = new Matrix4x4(new float[]
        {
            1, 2, 3, 4,
            4, 3, 2, 1,
            1, 2, 3, 4,
            4, 3, 2, 1,
        });
        var m2 = new Matrix4x4(new float[]
        {
            4, 5, 6, 7,
            7, 6, 5, 4,
            4, 5, 6, 7,
            7, 6, 5, 4,
        });
        var res = m1 * m2;

        var exp = new Matrix4x4(new float[]
        {
            58, 56, 54, 52, 
            52, 54, 56, 58, 
            58, 56, 54, 52, 
            52, 54, 56, 58,
        });
        
        for (int r = 0; r < 4; r++)
        {
            for (int c = 0; c < 4; c++)
            {
                Assert.Equal(exp.Get(r, c), res.Get(r, c));
            }
        }
    }
    
    [Fact]
    public void TestMatrixMultiplyMatrixNonCommutativity()
    {
        var m1 = new Matrix4x4(new float[]
        {
            1, 2, 3, 4,
            5, 6, 7, 8,
            1, 2, 3, 4,
            4, 3, 2, 1,
        });
        var m2 = new Matrix4x4(new float[]
        {
            4, 5, 6, 7,
            7, 6, 5, 4,
            4, 5, 6, 7,
            7, 6, 5, 4,
        });
        
        var res1 = m1 * m2;
        var exp1 = new Matrix4x4(new float[]
        {
            58, 56, 54, 52,
            146, 144, 142, 140,
            58, 56, 54, 52,
            52, 54, 56, 58,
        });
        
        for (int r = 0; r < 4; r++)
        {
            for (int c = 0; c < 4; c++)
            {
                Assert.Equal(exp1.Get(r, c), res1.Get(r, c));
            }
        }

        var res2 = m2 * m1;
        var exp2 = new Matrix4x4(new float[]
        {
            63, 71, 79, 87,
            58, 72, 86, 100,
            63, 71, 79, 87,
            58, 72, 86, 100,
        });
        
        for (int r = 0; r < 4; r++)
        {
            for (int c = 0; c < 4; c++)
            {
                Assert.Equal(exp2.Get(r, c), res2.Get(r, c));
            }
        }

    }
}
