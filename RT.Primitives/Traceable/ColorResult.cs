using RT.Math.LinearAlgebra;

namespace RT.Primitives.Traceable;

public class ColorResult
{
    public Vector3 Color { get; set; }
    public float LightDotProduct { get; set; }
    public ColorResult(Vector3 color, float lightDotProduct)
    {
        Color = color;
        LightDotProduct = lightDotProduct;
    }
    
    public ColorResult(Vector3 color)
    {
        Color = color;
    }
}