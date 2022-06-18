using RT.Math.LinearAlgebra;

namespace RT.Texture.Color;

public class OneColorTexture : IColorTexture
{
    public Vector3 Color { get; }
    
    public OneColorTexture(Vector3 color)
    {
        Color = color;
    }
    public Vector3 GetUVColor(UVcoordinates uv)
    {
        return Color;
    }
}