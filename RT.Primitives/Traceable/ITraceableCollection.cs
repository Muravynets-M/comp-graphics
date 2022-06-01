using RT.Math.LinearAlgebra;
using RT.Primitives.Transform;

namespace RT.Primitives.Traceable;

public interface ITraceableCollection
{
    public HitResult? Cast(Ray ray, float minT = float.Epsilon, float maxT = float.PositiveInfinity);
    List<ITransform> Lights { get; set; }
    HitResult? CastOnFirstObstacle(Ray ray, float maxT, float minT = float.Epsilon);
}