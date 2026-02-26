using System;
using System.Collections.Generic;

public class Row {
    private double[] values;
    public int length { get { return values.Length; } }
    public Row(int size) { values = new double[size]; }
    public double getValue(int j) { return values[j]; }
    public void setValue(int j, double value) { values[j] = value; }
}

public class Matrix {
    private int rows;
    private int cols;
    private Row[] rowData;
    public Matrix(int r, int c) {
        rows = r; cols = c;
        rowData = new Row[rows];
        for(int i = 0; i < rows; i++) rowData[i] = new Row(cols);
    }
    public int getRows() { return rows; }
    public int getCols() { return cols; }
    public double getValue(int i, int j) { return rowData[i].getValue(j); }
    public void setValue(int i, int j, double value) { rowData[i].setValue(j, value); }
    public void display() {
        for(int i = 0; i < rows; i++) {
            for(int j = 0; j < cols; j++) Console.Write(getValue(i, j) + " ");
            Console.WriteLine();
        }
    }
}

public class Vector {
    private int size;
    private double[] data;
    public Vector(int s) { size = s; data = new double[size]; }
    public int getSize() { return size; }
    public double getValue(int i) { return data[i]; }
    public void setValue(int i, double value) { data[i] = value; }
    public void display() {
        for(int i = 0; i < size; i++) Console.Write(getValue(i) + " ");
        Console.WriteLine();
    }
}

public class DimensionValidator {
    public bool validateMatrixAddition(Matrix A, Matrix B) {
        return A.getRows() == B.getRows() && A.getCols() == B.getCols();
    }
    public bool validateVectorAddition(Vector A, Vector B) {
        return A.getSize() == B.getSize();
    }
}

public class MatrixCalculator {
    private DimensionValidator validator;
    public MatrixCalculator(DimensionValidator v) { validator = v; }
    public Matrix add(Matrix A, Matrix B) {
        if(!validator.validateMatrixAddition(A, B)) throw new Exception("invalid dimensions");
        Matrix res = new Matrix(A.getRows(), A.getCols());
        for(int i = 0; i < A.getRows(); i++)
            for(int j = 0; j < A.getCols(); j++)
                res.setValue(i, j, A.getValue(i, j) + B.getValue(i, j));
        return res;
    }
}

public class VectorCalculator {
    private DimensionValidator validator;
    public VectorCalculator(DimensionValidator v) { validator = v; }
    public Vector add(Vector A, Vector B) {
        if(!validator.validateVectorAddition(A, B)) throw new Exception("invalid dimensions");
        Vector res = new Vector(A.getSize());
        for(int i = 0; i < A.getSize(); i++)
            res.setValue(i, A.getValue(i) + B.getValue(i));
        return res;
    }
}

public class MatrixRepository {
    private List<Matrix> matrices = new List<Matrix>();
    public void addMatrix(Matrix m) { matrices.Add(m); }
    public Matrix getMatrix(int index) { return matrices[index]; }
}

public class VectorRepository {
    private List<Vector> vectors = new List<Vector>();
    public void addVector(Vector v) { vectors.Add(v); }
    public Vector getVector(int index) { return vectors[index]; }
}

public class InputHandler {
    public Matrix inputMatrix() {
        Console.Write("rows: ");
        int r = int.Parse(Console.ReadLine());
        Console.Write("cols: ");
        int c = int.Parse(Console.ReadLine());
        Matrix m = new Matrix(r, c);
        for(int i = 0; i < r; i++)
            for(int j = 0; j < c; j++) {
                Console.Write($"[{i},{j}]: ");
                m.setValue(i, j, double.Parse(Console.ReadLine()));
            }
        return m;
    }
    public Vector inputVector() {
        Console.Write("size: ");
        int s = int.Parse(Console.ReadLine());
        Vector v = new Vector(s);
        for(int i = 0; i < s; i++) {
            Console.Write($"[{i}]: ");
            v.setValue(i, double.Parse(Console.ReadLine()));
        }
        return v;
    }
}

public class Program {
    public static void Main() {
        InputHandler input = new InputHandler();
        DimensionValidator validator = new DimensionValidator();
        MatrixCalculator mCalc = new MatrixCalculator(validator);
        VectorCalculator vCalc = new VectorCalculator(validator);

        Console.WriteLine("matrix a:");
        Matrix mA = input.inputMatrix();
        Console.WriteLine("matrix b:");
        Matrix mB = input.inputMatrix();
        Matrix mRes = mCalc.add(mA, mB);
        Console.WriteLine("matrix result:");
        mRes.display();

        Console.WriteLine("vector a:");
        Vector vA = input.inputVector();
        Console.WriteLine("vector b:");
        Vector vB = input.inputVector();
        Vector vRes = vCalc.add(vA, vB);
        Console.WriteLine("vector result:");
        vRes.display();
    }
}