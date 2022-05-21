namespace RT.Math.LinearAlgebra;

public struct Ray
{
    public Point3 Origin { get; }
    public Vector3 Direction { get; }

    public Ray(Point3 origin, Vector3 direction)
    {
        Origin = origin;
        Direction = Vector3.Unit(direction);
    }

    public Vector3 Cast(float t)
    {
        return Origin + Direction * t;
    }
}