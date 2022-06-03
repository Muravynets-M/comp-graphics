using RT.Light;
using RT.Math.LinearAlgebra;
using RT.Primitives.Traceable;

namespace RT.Primitives.Light;

public interface ILight
{
    public ColorRGB Color {get;}
    public float Intensity { get; }
    
    public float CastLightOnSurface(Point3 point, Vector3 surfNormal, ITraceableCollection world);
}