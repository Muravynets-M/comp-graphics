using RT.Math.LinearAlgebra;
using RT.Primitives.Face;
using RT.Primitives.Material;
using RT.Primitives.Primitive;

namespace RT.RenderInput.ObjFile.ObjParser;

public class ObjFileContent
{
    private readonly List<FaceObject> _faceObjects = new();
    private readonly List<IFace> _faces = new();
    private readonly List<Vector3> _normals = new();
    private readonly List<Point3> _vertices = new();

    private string? _currentFaceObject;
    private IMaterial? _currentMaterial;

    public IEnumerable<FaceObject> FaceObjects
    {
        get
        {
            if (_faces.Count > 0)
                BuildFaceObject();

            return new List<FaceObject>(_faceObjects);
        }
    }

    public void AddVertex(float x, float y, float z)
    {
        _vertices.Add(new Point3(x, y, z));
    }

    public void AddNormal(float x, float y, float z)
    {
        _normals.Add(new Vector3(x, y, z));
    }

    public void AddFace(IEnumerable<int> vertices, IEnumerable<int>? normals)
    {
        var faceVertices = vertices.Select(index => _vertices[index - 1]).ToList();
        var faceNormals = (IEnumerable<Vector3>?) null;

        if (normals is not null) faceNormals = normals.Select(index => _normals[index - 1]).ToList();

        _faces.Add(FaceFactory.Build(faceVertices, faceNormals));
    }

    public void AddMaterial(IMaterial material)
    {
        _currentMaterial = material;
    }

    public void AddFaceObject(string name)
    {
        if (_currentFaceObject is not null)
            BuildFaceObject();

        _currentFaceObject = name;
    }

    private void BuildFaceObject()
    {
        _currentFaceObject ??= "unnamed";
        _faceObjects.Add(new FaceObject(new List<IFace>(_faces), _currentFaceObject, _currentMaterial));

        _faces.Clear();
    }
}