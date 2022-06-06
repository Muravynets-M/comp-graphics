using RT.Math.LinearAlgebra;

namespace RT.Texture;

public interface IColorTexture
{
    public Vector3 GetColor(UVcoordinates uv);
}