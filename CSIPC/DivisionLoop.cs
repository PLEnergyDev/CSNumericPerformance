
namespace CSIPC;

public class DivisionLoop
{
    /*public static ulong Iterations;
    public static ulong LoopIterations;*/
    private static readonly int M = 20;
    
    public static long LeastInteger( ulong LoopIterations)
    {
        long result = 0;
        
        for (ulong i = 0; i < LoopIterations; i++)
        {
            var n = 0;
            var sum = 0.0;
            while (sum < M) {
                n++;
                sum += 1.0/n;
            }

            result += n;
        }
        return result;
    }
}