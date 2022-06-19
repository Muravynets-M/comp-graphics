using RT.Math.LinearAlgebra;

namespace RT.Texture.Color;

public class RainbowTexture : IColorTexture
{
    private MatrixTexture _matrix;
    
    public RainbowTexture()
    {
        _matrix = new MatrixTexture(new MatrixNxM<Vector3>(new List<Vector3>()
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
   

    public Vector3 GetUVColor(UVcoordinates uv)
    {
        return _matrix.GetUVColor(uv);
    }
}