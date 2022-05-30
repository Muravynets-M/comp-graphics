namespace RT.Render.RenderOutput;

public interface IImageBufferFactory
{
    public IImageBuffer BuildImageBuffer(string name);
}