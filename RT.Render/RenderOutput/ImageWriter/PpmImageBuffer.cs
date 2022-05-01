namespace RT.Render.RenderOutput.ImageWriter;

public class PpmImageBuffer: IImageBuffer, IDisposable
{
    private StreamWriter _file;
    private int _widthCounter;
    
    public PpmImageBuffer(int width, int height, string fileName)
    {
        Width = width;
        Height = height;
        _file = new StreamWriter($"../../../{fileName}.ppm");
        _file.AutoFlush = true;
        _file.Write($"P3\n {Width} {Height} \n255\n");
    }

    public int Width { get; }
    public int Height { get; }
    public void Write(char[] buffer)
    {
        _file.WriteLine(buffer);
    }

    public void Dispose()
    {
        _file.Close();
    }
}