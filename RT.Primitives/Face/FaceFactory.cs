using RT.Math.LinearAlgebra;
using RT.Primitives.Primitive;
using RT.Texture;

namespace RT.Primitives.Face;

public static class FaceFactory
{
    public static void Build(IEnumerable<Point3> vertices, IEnumerable<UVcoordinates>? textures,
        IEnumerable<Vector3>? normals, List<IFace> faces)
    {
        var listVertices = vertices.ToList();

        if (listVertices.Count > 3)
            BuildPolygon(listVertices, normals!.ToList(), textures!.ToList(), faces);

        if (normals is null)
        {
            faces.Add(new Triangle(listVertices[0], listVertices[1], listVertices[2]));
            return;
        }

        var listNormals = normals.ToList();

        if (textures is null)
        {
            faces.Add(new Triangle(listVertices[0], listVertices[1], listVertices[2],
                listNormals[0], listNormals[1], listNormals[2]));
            return;
        }

        var textureNormals = textures.ToList();

        faces.Add(new Triangle(listVertices[0], listVertices[1], listVertices[2],
            listNormals[0], listNormals[1], listNormals[2],
            textureNormals[0], textureNormals[1], textureNormals[2]));
    }

    private static void BuildPolygon(List<Point3> vertices, List<Vector3> normals,
        List<UVcoordinates> textures, List<IFace> faces)
    {
        faces.Add(new Triangle(
            vertices[0], vertices[1], vertices[2],
            normals[0], normals[1], normals[2],
            textures[0], textures[1], textures[2]));
        faces.Add(new Triangle(
            vertices[0], vertices[2], vertices[3],
            normals[0], normals[2], normals[3],
            textures[0], textures[2], textures[3]));
    }
}