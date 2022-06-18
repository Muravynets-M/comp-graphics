using RT.Math.LinearAlgebra;

namespace RT.Texture.Color;

public class MatrixTexture : IColorTexture
{
    private MatrixNxM<Vector3> _matrix;
    
    public MatrixTexture(MatrixNxM<Vector3> matrix)
    {
        _matrix = matrix;
    }
    public Vector3 GetUVColor(UVcoordinates uv)
    {
        var r = (int) MathF.Round((_matrix.Rows-1) * uv.U);
        var c = (int) MathF.Round((_matrix.Columns-1) * uv.V);


        return _matrix.Get(r, c);
    }

    public static MatrixTexture AsRainbowTexture()
    {
        return new MatrixTexture(new MatrixNxM<Vector3>(new List<Vector3>()
        {
            Vector3.Rgb(255, 0, 0),
            Vector3.Rgb(255, 128, 0),
            Vector3.Rgb(255, 255, 0),
            Vector3.Rgb(128, 255, 0),
            Vector3.Rgb(0, 255, 0),
            Vector3.Rgb(0, 255, 128),
            Vector3.Rgb(0, 255, 255),
            Vector3.Rgb(0, 128, 255),
            Vector3.Rgb(0, 128, 255),
            Vector3.Rgb(128, 0, 255),
            Vector3.Rgb(255, 0, 255),
            Vector3.Rgb(255, 0, 128),
        }, 12));
    }
}