namespace Fundamentals
{
    class Program
    {
        static void Main(string[] args)
        {
            Fundamentals countBackwards = new Fundamentals();

            countBackwards.CountBackwards();

            Fundamentals reversed = new Fundamentals();
            Console.Write("The reversed output: ");
            Console.Write(reversed.ReverseString("stressed :("));
            Console.WriteLine();
            Console.WriteLine();

            Fundamentals remainder = new Fundamentals();
            Console.WriteLine(remainder.GCD(13, 31));
            Console.WriteLine();
            Console.WriteLine(remainder.GCD(-99, 4345));
        }
    }
}


