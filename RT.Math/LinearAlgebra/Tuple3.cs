namespace RT.Math.LinearAlgebra;

public abstract class Tuple3
{
    public float X { get; }
    public float Y { get; }
    public float Z { get; }

    protected Tuple3(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }
}