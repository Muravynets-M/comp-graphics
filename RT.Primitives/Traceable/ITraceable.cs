using System.Runtime.InteropServices;
using RT.Math.LinearAlgebra;

namespace RT.Primitives.Traceable;

public interface ITraceable
{
    public HitResult? Hit(Ray r, float minT, float maxT);
}