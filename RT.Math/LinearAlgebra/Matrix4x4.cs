namespace RT.Math.LinearAlgebra;

public class Matrix4x4
{
    private float[] _matrix;

    public Matrix4x4()
    {
        _matrix = new float[16]
        {
            0, 0, 0, 0,
            0, 0, 0, 0,
            0, 0, 0, 0,
            0, 0, 0, 0
        };
    }

    public Matrix4x4(float[] matrix)
    {
        if (matrix.Length != 16)
        {
            throw new ArgumentException("matrix 4x4 should contain 16 elements");
        }

        _matrix = matrix;
    }

    // Starts from 0 index
    public float Get(int row, int column)
    {
        return _matrix[row * 4 + column];
    }
    
    // Starts from 0 index
    public void Set(int row, int column, float num)
    {
        _matrix[row * 4 + column] = num;
    }

    public override bool Equals(object? obj)
    {
        if (obj == null)
        {
            return false;
        }
        
        if (!(obj is Matrix4x4))
        {
            return false;
        }

        Matrix4x4 m = (Matrix4x4) obj;
        var equal = true;
        for (int i = 0; i < 16; i++)
        {
            equal &= m._matrix[i] == _matrix[i];
        }

        return equal;
    }

    public static Matrix4x4 operator +(Matrix4x4 m) => m;
    public static Matrix4x4 operator -(Matrix4x4 m) => -1 * m;

    public static Matrix4x4 operator *(float c, Matrix4x4 m)
    {
        var matrix = new Matrix4x4();
        for (int i = 0; i < 16; i++)
        {
            matrix._matrix[i] = c * m._matrix[i];
        }

        return matrix;
    }

    public static Vector4 operator *(Matrix4x4 m, Vector4 v)
    {
        float[] vector = {0, 0, 0, 0};
        for (var row = 0; row < 4; row++)
        {
            vector[row] = m.Get(row, 0) * v.X + 
                          m.Get(row, 1) * v.Y + 
                          m.Get(row, 2) * v.Z + 
                          m.Get(row, 3) * v.Scaler;
        }
        
        return new Vector4(vector[0], vector[1],vector[2],vector[3]);
    }

    public static Matrix4x4 operator +(Matrix4x4 m1, Matrix4x4 m2)
    {
        var matrix = new Matrix4x4();
        for (int i = 0; i < 16; i++)
        {
            matrix._matrix[i] = m1._matrix[i] + m2._matrix[i];
        }

        return matrix;
    }
    public static Matrix4x4 operator -(Matrix4x4 m1, Matrix4x4 m2) => m1 + (-m2);

    public static Matrix4x4 operator *(Matrix4x4 m1, Matrix4x4 m2)
    {
        var matrix = new Matrix4x4();
        for (int row = 0; row < 4; row++)
        {
            for (int column = 0; column < 4; column++)
            {
                var sum = 0f;
                for (int i = 0; i < 4; i++)
                {
                    sum += m1.Get(row, i) * m2.Get(i, column);
                }
                
                matrix.Set(row, column, sum);
            }
        }

        return matrix;
    }

    public static Matrix4x4 Zero()
    {
        var m = new Matrix4x4
        {
            _matrix = new float[16]
            {
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0,
                0, 0, 0, 0
            }
        };

        return m;
    }
    
    public static Matrix4x4 One()
        {
            var m = new Matrix4x4
            {
                _matrix = new float[16]
                {
                    1, 0, 0, 0,
                    0, 1, 0, 0,
                    0, 0, 1, 0,
                    0, 0, 0, 1
                }
            };
    
            return m;
        }

    public static Matrix4x4 GetTranslationMatrix(float dx, float dy, float dz)
    {
        var m = new Matrix4x4
        {
            _matrix = new float[16]
            {
                1, 0, 0, dx,
                0, 1, 0, dy,
                0, 0, 1, dz,
                0, 0, 0, 1,
            }
        };
    
        return m;
    }

    public static Matrix4x4 GetScaleMatrix(float kx, float ky, float kz)
    {
        var m = new Matrix4x4
        {
            _matrix = new float[16]
            {
                kx, 0, 0, 0,
                0, ky, 0, 0,
                0, 0, kz, 0,
                0, 0, 0, 1
            }
        };
    
        return m;
    }

    public static Matrix4x4 GetXAxisRotationMatrix(float gamma)
    {
        float c = (float) System.Math.Cos(gamma);
        float s = (float) System.Math.Sin(gamma);
        var m = new Matrix4x4
        {
            _matrix = new float[16]
            {
                1, 0, 0, 0,
                0, c, -s, 0,
                0, s, c, 0,
                0, 0, 0, 1
            }
        };
    
        return m;
    }
    
    public static Matrix4x4 GetYAxisRotationMatrix(float beta)
    {
        float c = (float) System.Math.Cos(beta);
        float s = (float) System.Math.Sin(beta);
        var m = new Matrix4x4
        {
            _matrix = new float[16]
            {
                c, 0, s, 0,
                0, 1, 0, 0,
                -s, 0, c, 0,
                0, 0, 0, 1
            }
        };
    
        return m;
    }
    
    public static Matrix4x4 GetZAxisRotationMatrix(float alpha)
    {
        float c = (float) System.Math.Cos(alpha);
        float s = (float) System.Math.Sin(alpha);
        var m = new Matrix4x4
        {
            _matrix = new float[16]
            {
                c, -s, 0, 0,
                s, c, 0, 0,
                0, 0, 1, 0,
                0, 0, 0, 1
            }
        };
    
        return m;
    }
}