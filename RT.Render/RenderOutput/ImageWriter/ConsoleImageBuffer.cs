namespace RT.Render.RenderOutput;

public class ConsoleImageBuffer: IImageBuffer
{
    private int _widthCounter;
    
    public ConsoleImageBuffer(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public int Width { get; }
    public int Height { get; }

    public void Write(char[] buffer)
    {
        _widthCounter++;

        Console.Write(buffer);
        
        if (_widthCounter < Width) return;
        
        _widthCounter = 0;
        Console.Write('\n');
    }
}