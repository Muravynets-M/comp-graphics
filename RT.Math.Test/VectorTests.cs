using RT.Math.LinearAlgebra;
using Xunit;

namespace RT.Math.Test;

public class VectorTests
{
    [Fact]
    public void TestDotProduct()
    {
        var v1 = new Vector3(1, 2, 3);
        var v2 = new Vector3(3, 2, 1);
        Assert.Equal(10, Vector3.Dot(v1, v2));
    }

    [Fact]
    public void TestCrossProduct()
    {
        var v1 = new Vector3(2, -1, 1);
        var v2 = new Vector3(1, 3, 1);
        Assert.Equal(new Vector3(-4, -1, 7), Vector3.Cross(v1, v2));
    }
}