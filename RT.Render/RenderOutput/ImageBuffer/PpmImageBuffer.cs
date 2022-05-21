namespace RT.Render.RenderOutput.ImageBuffer;

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

    bool disposed;

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                _file.Flush();
                _file.Close();
            }
        }
        //dispose unmanaged resources
        disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}