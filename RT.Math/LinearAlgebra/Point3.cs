namespace RT.Math.LinearAlgebra;

public struct Point3
{
    public float X { get; }
    public float Y { get; }
    public float Z { get; }
    
    public Point3(float x, float y, float z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public override string ToString()
    {
        return "({x}, {y}, {z})";
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }
        
        if (!(obj is Point3))
        {
            return false;
        }
        
        return X == ((Point3)obj).X && Y == ((Point3)obj).Y && Z == ((Point3)obj).Z;
    }
    
    public static Point3 operator +(Point3 v) => v;
    public static Point3 operator -(Point3 v) => new (-v.X, -v.Y, -v.Z);

    public static Vector3 operator +(Point3 v, Point3 u) => new(v.X + u.X, v.Y + u.Y, v.Z + u.Z);
    public static Vector3 operator -(Point3 v, Point3 u) => v + (-u);
    
    public static Vector3 operator +(Point3 v, Vector3 u) => new(v.X + u.X, v.Y + u.Y, v.Z + u.Z);
    public static Vector3 operator -(Point3 v, Vector3 u) => new(v.X - u.X, v.Y - u.Y, v.Z - u.Z);

    public static explicit operator Vector3(Point3 p) => new(p.X, p.Y, p.Z);
}