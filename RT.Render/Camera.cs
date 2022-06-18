using System.Drawing;
using RT.Math.LinearAlgebra;
using RT.Primitives.Transform;

namespace RT.Render;

public class Camera: ITransform
{
    private readonly Vector3 _viewportWidth;
    private readonly Vector3 _viewportHeight;

    private Point3 _lowerLeftCorner;
    
    private readonly Vector3 _w;
    private readonly Vector3 _u;
    private readonly Vector3 _v;

    private Random _random = new Random();
    
    public Camera(Point3 origin, Vector3 direction, float width, float height)
    {
        Origin = origin;
        Direction = direction;

        _w = Vector3.Unit(origin - direction);
        _u = Vector3.Unit(Vector3.Cross(World.Up, _w));
        _v = Vector3.Unit(Vector3.Cross(_w, _u));
        
        _viewportWidth = _u * width;
        _viewportHeight = _v * height;

        _lowerLeftCorner = (Point3) ( origin - _viewportWidth / 2.0f - _viewportHeight / 2.0f - _w);
    }

    public Point3 Origin { get; }

    public Vector3 Direction { get; }

    public Ray GetRay(float x, float y)
    {
        return new Ray(Origin, _lowerLeftCorner + _viewportWidth * x + _viewportHeight * y - Origin);
    }

    public Ray GetSlightlyRandomRay(float x, float y)
    {
        return new Ray(Origin, _lowerLeftCorner +
                               _viewportWidth * x * NextFloat(500f) +
                               _viewportHeight * y * NextFloat(500f) -
                               Origin);
    }

    private float NextFloat(float scale = 1f)
    {
        return 1f - (float) _random.NextDouble() / scale;
    }
    
    public void ApplyTransformation(Matrix4x4 matrix)
    {
        
    }
}