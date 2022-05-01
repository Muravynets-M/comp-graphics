using RT.Primitives.Traceable;

namespace RT.Render.RenderOutput.HitResultAdapter;

public interface IHitResultAdapter
{
    char[] ToChar(HitResult? hitResult);
}