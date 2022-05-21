using CsharpRAPL.Benchmarking.Attributes;

namespace EnergyTest.Benchmarks;

public class MatrixMultiplication
{
    public static ulong Iterations;
    public static ulong LoopIterations;
    
    //Initialize arrays
    static int size = 80;
    static readonly Random r = new();
    static double[,] R = new double[size,size], A = InitMatrix(size), B = InitMatrix(size);
    static double[][] RR = InitArray(size, true), AA = InitArray(size), BB = InitArray(size);

    private static int 
        aCols = A.GetLength(1),
        rRows = R.GetLength(0),
        rCols = R.GetLength(1),
        bCols = B.GetLength(1);
    

    private static double[,] InitMatrix(int size)
    {
        var result = new double[size, size];
        for (var i = 0; i < size; i++)
        {
            for (var j = 0; j < size; j++)
            {
                result[i, j] = r.NextDouble();
            }
        }
        return result;
    }

    private static double[][] InitArray(int size, bool zero = false)
    {
        var result = new double [size][];
        for (var i = 0; i < size; i++)
        {
            var row = new double[size];
            for (var j = 0; j < size; j++)
            {
                row[j] = zero ? 0 : r.NextDouble();
            }

            result[i] = row;
        }

        return result;
    }

    [Benchmark("Matrix multiplication", "Straightforward matrix multiplication")]
    public static double[,] Standard()
    {
        for (ulong i = 0; i < LoopIterations; i++)
        {
            for (var r = 0; r < rRows; r++)
            {
                for (var c = 0; c < rCols; c++)
                {
                    var sum = 0.0;
                    for (var k = 0; k < aCols; k++)
                        sum += A[r, k] * B[k, c];
                    R[r, c] = sum;
                }
            }
        }
        return R;
    }

    [Benchmark("Matrix multiplication", "Unsafe matrix multiplication")]
    public static double[,] Unsafe()
    {
        for (ulong i = 0; i < LoopIterations; i++)
        {
            for (var r = 0; r<rRows; r++) {
                for (var c=0; c<rCols; c++) {
                    var sum = 0.0;
                    unsafe {
                        fixed (double* abase = &A[r,0], bbase = &B[0,c]) {
                            for (var k=0; k<aCols; k++)
                                sum += abase[k] * bbase[k*bCols];
                        }
                        R[r,c] = sum;
                    }
                }
            }
        }
        return R;
    }

    [Benchmark("Matrix multiplication", "Java like matrix multiplication")]
    public static double[][] JavaLike()
    {
        for (ulong i = 0; i < LoopIterations; i++)
        {
            for (var r=0; r<rRows; r++) {
                double[] Ar = AA[r], Rr = RR[r];
                for (var c=0; c<rCols; c++) {
                    var sum = 0.0;
                    for (int k=0; k<aCols; k++)
                        sum += Ar[k]*BB[k][c];
                    Rr[c] = sum;
                }
            }
        }

        return RR;

    }
}