using RT.Math.LinearAlgebra;
using RT.Primitives.Primitive;

namespace RT.Primitives.Face;

public static class FaceFactory
{
    public static IFace Build(IEnumerable<Point3> vertices, IEnumerable<Vector3>? normals)
    {
        var listVertices = vertices.ToList();

        if (listVertices.Count != 3)
            throw new NotImplementedException();

        if (normals is null) 
            return new Triangle(listVertices[0], listVertices[1], listVertices[2]);
        
        var listNormals = normals.ToList();
        
        return new Triangle(listVertices[0], listVertices[1], listVertices[2], 
            listNormals[0], listNormals[1], listNormals[2]);
    }
}