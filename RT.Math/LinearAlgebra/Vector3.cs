using System.Runtime.CompilerServices;
using Microsoft.VisualBasic.CompilerServices;

namespace RT.Math.LinearAlgebra;

public struct Vector3
{
    public float X { get; }
    public float Y { get; }
    public float Z { get; }

    public Vector3(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public static Vector3 operator +(Vector3 v, float f) => new (v.X + f, v.Y + f, v.Z + f);
    public static Vector3 operator -(Vector3 v, float f) => v + (-f);
    public static Vector3 operator *(Vector3 v, float f) => new (v.X * f, v.Y * f, v.Z * f);
    public static Vector3 operator /(Vector3 v, float f) => v * (1/f);

    public static Vector3 operator +(Vector3 v) => v;
    public static Vector3 operator -(Vector3 v) => new (-v.X, -v.Y, -v.Z);

    public static Vector3 operator +(Vector3 v, Vector3 u) => new(v.X + u.X, v.Y + u.Y, v.Z + u.Z);
    public static Vector3 operator -(Vector3 v, Vector3 u) => v + (-u);
    
    public static Vector3 operator +(Vector3 v, Point3 p) => new(v.X + p.X, v.Y + p.Y, v.Z + p.Z);
    public static Vector3 operator -(Vector3 v, Point3 p) => v + (-p);
    
    public static explicit operator Point3(Vector3 v) => new (v.X, v.Y, v.Z);

    public float Lenght => MathF.Sqrt(LengthSquared);
    public float LengthSquared => X * X + Y * Y + Z * Z;

    public static Vector3 Zero() => new (0.0f, 0.0f, 0.0f);

    public static float Dot(Vector3 v, Vector3 u) => v.X * u.X + v.Y * u.Y + v.Z * u.Z;

    public static Vector3 Cross(Vector3 v, Vector3 u) => new(
        v.Y * u.Z - v.Z * u.Y,
        v.Z * u.X - v.X * u.Z,
        v.X * u.Y - v.Y * u.X
    );
    public static Vector3 Unit(Vector3 v) => v / v.Lenght;
}