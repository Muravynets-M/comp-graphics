using System.Runtime.InteropServices;
using RT.Math.LinearAlgebra;
using RT.Primitives.Material;
using RT.Primitives.Transform;

namespace RT.Primitives.Traceable;

public interface ITraceable: ITransform
{
    public float MinX { get; }
    public float MinY { get; }
    public float MinZ { get; }
    public float MaxX { get; }
    public float MaxY { get; }
    public float MaxZ { get; }
    
    public IMaterial? Material { get; }
    
    public HitResult? Hit(Ray r, float minT, float maxT);
}