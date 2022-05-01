using RT.Math.LinearAlgebra;

namespace RT.Render.RenderOutput;

public interface IResultBufferPrinter
{
    public int Width { get; }
    public int Height { get; }
    
    public void Print(float color);
}