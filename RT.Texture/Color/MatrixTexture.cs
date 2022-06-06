using RT.Math.LinearAlgebra;

namespace RT.Texture.Color;

public class MatrixTexture : IColorTexture
{
    private MatrixNxM<Vector3> _matrix;
    
    public MatrixTexture(MatrixNxM<Vector3> matrix)
    {
        _matrix = matrix;
    }
    public Vector3 GetColor(UVcoordinates uv)
    {
        var r = (int) MathF.Round(_matrix.Rows * uv.U);
        var c = (int) MathF.Round(_matrix.Columns * uv.V);

        return _matrix.Get(r, c);
    }
}