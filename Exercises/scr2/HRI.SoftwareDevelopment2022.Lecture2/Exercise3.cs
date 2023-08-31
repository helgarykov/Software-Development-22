namespace HRI.SoftwareDevelopment2022.Lecture2;

public class Exercise3
{
    /// <summary>
    /// Get the roots of the quadratic equation.
    /// </summary>
    /// <param name="a"> the 1. coefficient </param>
    /// <param name="b"> the 2. coefficient </param>
    /// <param name="c"> the 3. coefficient </param>
    public static void GetRoots(double a, double b, double c)
    {
        if (a == 0)
        {
            if (b != 0 && c != 0)
            {
                var root = c * -1 / b;
                Console.WriteLine("\n The equation with a = 0 has only one solution " + root);
            }
            else if (b != 0 && c == 0)
            {
                var root = 0;
                Console.WriteLine("\n The equation with a = 0 and c = 0 has only one zero solution " + root);
            }
            else
            {
                Console.WriteLine("\n The equation has no solutions because both a and b equal zero.");
            }
        }
        else
        {
            var discriminant = Math.Pow(b, 2) - 4 * a * c;
            switch (discriminant)
            {
                case > 0:
                {
                    var root1 = (b * -1 + Math.Sqrt(discriminant)) / 2 * a;
                    var root2 = (b * -1 - Math.Sqrt(discriminant)) / 2 * a;
                    Console.WriteLine("\n The roots of the equation are " + root1 + " and " + root2);
                    break;
                }
                case 0:
                {
                    var root = b * -1 / (2 * a);
                    Console.WriteLine("\n The equation has only one solution " + root);
                    break;
                }
                default:
                    Console.WriteLine("\n The equations has no solutions.");
                    break;
            }
        }
    }
}


