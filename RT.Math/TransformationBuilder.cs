using RT.Math.LinearAlgebra;

namespace RT.Math;

public class TransformationBuilder
{
    private Matrix4x4 _translate;
    private Matrix4x4 _scale;
    private Matrix4x4 _rotate;

    public TransformationBuilder()
    {
        _translate = Matrix4x4.One();
        _rotate = Matrix4x4.One();
        _scale = Matrix4x4.One();
    }
    
    public TransformationBuilder WithTranslation(float dx, float dy, float dz)
    {
        _translate *= Matrix4x4.GetTranslationMatrix(dx, dy, dz);
        return this;
    }

    public TransformationBuilder WithScaling(float kx, float ky, float kz)
    {
        _scale *= Matrix4x4.GetScaleMatrix(kx, ky, kz);
        return this;
    }

    public TransformationBuilder WithRotation(float angleX, float angleY, float angleZ)
    {
        _rotate *= Matrix4x4.GetXAxisRotationMatrix(angleX) * 
                   Matrix4x4.GetYAxisRotationMatrix(angleY) * 
                   Matrix4x4.GetZAxisRotationMatrix(angleZ);
        return this;
    }

    public Matrix4x4 Build()
    {
        return _translate * _rotate * _scale;
    }
}