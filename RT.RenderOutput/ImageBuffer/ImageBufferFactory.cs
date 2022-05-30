using RT.Render.RenderOutput;

namespace RT.RenderOutput.ImageBuffer;

public class ImageBufferFactory: IImageBufferFactory
{
    private readonly string _bufferType;
    private readonly int _width;
    private readonly int _height;

    public ImageBufferFactory(string bufferType, int width, int height)
    {
        _bufferType = bufferType;
        _width = width;
        _height = height;
    }
    public IImageBuffer BuildImageBuffer(string name)
    {
        return _bufferType switch
        {
            "Ppm" => new PpmImageBuffer(_width, _height, $"{name}"),
            "Console" => new ConsoleImageBuffer(_width, _height),
            _ => throw new ArgumentException()
        };
    }
}