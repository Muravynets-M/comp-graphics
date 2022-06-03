using RT.Math.LinearAlgebra;

namespace RT.Light;

public struct ColorRGB
{
    public float Red { get; }
    public float Green { get; }
    public float Blue { get; }

    public ColorRGB(float red, float green, float blue)
    {
        Red = red;
        Green = green;
        Blue = blue;
    }
    
    public static explicit operator Vector3(ColorRGB c) => new (c.Red, c.Green, c.Blue);
}