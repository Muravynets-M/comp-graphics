namespace RT.Light;

public struct ColorRGB
{
    public byte Red { get; }
    public byte Green { get; }
    public byte Blue { get; }

    public ColorRGB(byte red, byte green, byte blue)
    {
        Red = red;
        Green = green;
        Blue = blue;
    }
}