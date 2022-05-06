namespace RT.Render.RenderOutput.ImageBuffer;

public interface IImageBuffer
{
    public int Width { get; }
    public int Height { get; }
    
    void Write(char[] buffer);
}