using RT.Math.LinearAlgebra;
using RT.Primitives.Primitive;
using RT.Primitives.Traceable;
using RT.Render.RenderInput;

namespace RT.RenderInput.InMemory;

public class InMemoryInput : IRenderInput
{
    public IEnumerable<ITraceable> GetWorldInput()
    {
         return new List<ITraceable>
        {
            new Sphere(
                new Point3(-5f, 5f, -7f),
                0.5f
            )
        };
    }
}