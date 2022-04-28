using RT.Math.LinearAlgebra;

namespace RT.Render.RenderOutput;

public class ConsolePrinter: IPrinter
{
    public int Width { get; }
    public int Height { get; }

    private int _widthCounter = 0;
    
    public ConsolePrinter(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public void Print(float color)
    {
        _widthCounter++;

        switch (color)
        {
            case <= 0:
                Console.Write(' ');
                break;
            case <= 0.2f:
                Console.Write('.');
                break;
            case <= 0.5f:
                Console.Write('*');
                break;
            case <= 0.8f:
                Console.Write('O');
                break;
            case > 0.8f:
                Console.Write('#');
                break;
        }

        if (_widthCounter < Width) return;
        
        _widthCounter = 0;
        Console.Write('\n');
    }
}