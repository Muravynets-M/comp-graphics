using RT.Math.LinearAlgebra;
using RT.Primitives.Traceable;

namespace RT.Primitives.Material;

public interface IMaterial
{
    public ColorResult CalculateColor(Ray originalRay, HitResult hitResult, ITraceableCollection world,
        int recursionCount = 0);
}