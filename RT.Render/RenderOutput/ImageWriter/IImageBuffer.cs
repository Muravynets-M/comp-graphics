using System.Xml;

namespace RT.Render.RenderOutput;

public interface IImageBuffer
{
    public int Width { get; }
    public int Height { get; }
    
    void Write(char[] buffer);
}