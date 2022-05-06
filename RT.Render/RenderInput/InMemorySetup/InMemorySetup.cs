using RT.Math.LinearAlgebra;
using RT.Primitives;
using RT.Primitives.Traceable;

namespace RT.Render.RenderInput.InMemorySetup;

public class InMemorySetup : IRenderInput
{
    public IEnumerable<ITraceable> GetWorldInput()
    {
         return new List<ITraceable>
        {
            // new Sphere(
            // new Point3(0.0f, 0f, -1f),
            // 0.5f
            // ),
            new Sphere(
                (Point3) Vector3.Unit(new Vector3(-0.5f, 0.5f, -0f)),
                0.5f
            )
        };
    }
}