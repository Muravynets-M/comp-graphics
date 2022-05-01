namespace RT.Render.RenderOutput.ImageWriter;

public interface IImageBuffer
{
    public int Width { get; }
    public int Height { get; }
    
    void Write(char[] buffer);
}