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
    
    public static explicit operator Point3(Vector4 v) => new (v.X/v.Scaler, v.Y/v.Scaler, v.Z/v.Scaler);
    public static explicit operator Vector3(Vector4 v) => new (v.X/v.Scaler, v.Y/v.Scaler, v.Z/v.Scaler);
}