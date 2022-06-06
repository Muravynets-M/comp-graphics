namespace RT.Texture;

public struct UVcoordinates
{
    public float U { get; }
    public float V { get; }

    public UVcoordinates(float u, float v)
    {
        U = System.Math.Clamp(u, 0, 1);
        V = System.Math.Clamp(v, 0, 1);
    }
    
    public static UVcoordinates operator +(UVcoordinates uv1, UVcoordinates uv2) => new (uv1.U + uv2.U, uv1.V + uv2.V);
    public static UVcoordinates operator *(UVcoordinates uv, float f) => new (uv.U * f, uv.V * f);
}