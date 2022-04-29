using System.Runtime.InteropServices;
using RT.Math.LinearAlgebra;
using RT.Primitives.Transform;

namespace RT.Primitives.Traceable;

public interface ITraceable: ITransform
{
    public HitResult? Hit(Ray r, float minT, float maxT);
}