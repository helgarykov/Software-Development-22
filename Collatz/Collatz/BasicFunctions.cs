namespace Collatz;

public class BasicFunctions
{
    /// <summary>
    /// Checks if n reaches 1. 
    /// </summary>
    /// <param name="n"></param>
    /// <param name="maxLen"></param>
    /// <param name="maxSize"></param>
    /// <returns> len </returns>
    public static int Collatz(int n, int maxLen, int maxSize)
    {
        int len = 0;
        while (n > 1 && len < maxLen && n <= maxSize)
        {
            len++;
            if (n % 2 == 0)
            {
                n = n / 2;
            } else
            {
                n = 3 * n + 1;
            }
        }
        if (n == 1) len++;
        return len;
    }
    
    /// <summary>
    /// Checks if n reaches 1, but recursively.
    /// </summary>
    /// <param name="n"></param>
    /// <param name="max_len"></param>
    /// <param name="max_size"></param>
    /// <returns>Number of function calls.</returns>
    public static int CollatzRec(int n, int max_len, int max_size) {
        if(n < 1 || n > max_size || max_len <= 0) {
            return 0;
        } else if (n == 1) {
            return 1;
        } else {
            return 1 + (n % 2 == 0
                ? CollatzRec(n/2,max_len-1,max_size)
                : CollatzRec(3*n+1,max_len-1,max_size));
        }
    }
}
