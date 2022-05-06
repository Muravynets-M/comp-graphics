using RT.Primitives.Primitive;
using RT.Primitives.Traceable;

namespace RT.Render.RenderInput;

public interface IRenderInput
{
    IEnumerable<ITraceable> GetWorldInput();
}