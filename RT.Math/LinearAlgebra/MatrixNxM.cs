namespace RT.Math.LinearAlgebra;

public class MatrixNxM<T>
{
    private List<T> _matrix;
    private int _inRow;
    public int Columns { get; }
    public int Rows { get; }

    public MatrixNxM(List<T> matrix, int inRow)
    {
        _matrix = matrix;
        _inRow = inRow;
        Columns = inRow; 
        Rows = matrix.Count / inRow;
    }
    
    // Starts from 0 index
    public T Get(int row, int column)
    {
        return _matrix[row * _inRow + column];
    }
    
    // Starts from 0 index
    public void Set(int row, int column, T num)
    {
        _matrix[row * _inRow + column] = num;
    }
}