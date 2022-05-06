using RT.Math.LinearAlgebra;

namespace RT.Primitives.Transform;

public interface ITransform
{
    public Point3 Origin { get; }
    public void ApplyTransformation(Matrix4x4 matrix);
}