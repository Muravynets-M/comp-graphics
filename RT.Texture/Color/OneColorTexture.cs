using RT.Math.LinearAlgebra;

namespace RT.Texture.Color;

public class OneColorTexture : IColorTexture
{
    private Vector3 _color;
    
    public OneColorTexture(Vector3 color)
    {
        _color = color;
    }
    public Vector3 GetColor(UVcoordinates uv)
    {
        return _color;
    }
}