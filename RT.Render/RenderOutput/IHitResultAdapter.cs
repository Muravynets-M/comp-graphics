using RT.Primitives.Traceable;

namespace RT.Render.RenderOutput;

public interface IHitResultAdapter
{
    char[] ToChar(ColorResult? hitResult);
}