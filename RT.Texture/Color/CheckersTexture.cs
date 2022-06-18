using RT.Math.LinearAlgebra;

namespace RT.Texture.Color;

public class CheckersTexture : IColorTexture
{
    private Vector3 _color1;
    private Vector3 _color2;

    public CheckersTexture(Vector3 color1, Vector3 color2)
    {
        _color1 = color1;
        _color2 = color2;
    }

    public Vector3 GetUVColor(UVcoordinates uv)
    {
        if ((uv.U < 0.5 && uv.V > 0.5) || (uv.U > 0.5 && uv.V < 0.5))
            return _color1;

        return _color2;
    }

    public static MatrixTexture AsMatrixTexture()
    {
        return new MatrixTexture(
            new MatrixNxM<Vector3>(
                new List<Vector3>()
                {
                    new Vector3(1f, 0f, 0f),
                    new Vector3(0f, 1f, 0f),
                    new Vector3(1f, 0f, 0f),
                    new Vector3(0f, 1f, 0f),
                },
                2
            )
        );
    }
}