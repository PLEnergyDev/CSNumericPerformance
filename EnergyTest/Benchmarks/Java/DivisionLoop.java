public class DivisionLoop {
    public static long Iterations;
    public static long LoopIterations;
    private static int M = 20;

    /***
     * Benchmark for a Division Intensive Loop
     */
    public static void LeastInteger(int LoopIterations)
    {
        long result = 0;
        for(int i = 0; i < LoopIterations; i++) {

            var n = 0;
            var sum = 0.0;
            while (sum < M) {
                n++;
                sum += 1.0 / n;
            }
            result += n;
        }

    }
}
