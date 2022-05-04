namespace RT.Math.LinearAlgebra;

public class Vector4
{
    public float X {get; }
    public float Y {get; }
    public float Z {get; }
    public float Scaler { get; }

    public Vector4(float x, float y, float z, float scaler = 1)
    {
        X = x;
        Y = y;
        Z = z;
        Scaler = scaler;
    }

    public Vector3 ToVector3() => new (X/Scaler, Y/Scaler, Z/Scaler);
}