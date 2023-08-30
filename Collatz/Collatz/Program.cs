
namespace Collatz
{
    namespace System.Collections.Generic
    {
        public static partial class Program
        {
            public static void Main(string[] args)
            {
                int result = BasicFunctions.Collatz(0, 20, 20);
                Console.WriteLine($"Collatz result: {result}.");
                int result2 = BasicFunctions.CollatzRec(1, 1, 1);
                Console.WriteLine($"CollatzRec result: {result2}.");
                
                bool isGoldCustomer = true;
                //float price = 19.95f;

                float price = (isGoldCustomer) ? 19.95f : 29.95f; // the same as is/else statement above
                Console.WriteLine(price);

                var season = Season.Autumn;
                switch (season)
                {
                    case Season.Autumn:
                    case Season.Summer:
                        Console.WriteLine("It's time to get a promotion."); // if season is Autumn OR Summer, this peace of code will be executed
                        break;
                    case Season.Winter:
                        Console.WriteLine("It's winter.");
                        break;
                    case Season.Spring:
                        Console.WriteLine("It's spring");
                        break;
                    default:
                        Console.WriteLine("I don't understand that season.");
                        break;
                }

                Console.WriteLine("Enter two numbers");
                var integer1 = Convert.ToInt32(Console.ReadLine());
                var integer2 = Convert.ToInt32(Console.ReadLine());
                if (integer1 > integer2)
                {
                    Console.WriteLine($"{integer1} is bigger than {integer2}");
                }
                else if (integer1 < integer2)
                {
                    Console.WriteLine($"{integer2} is bigger than {integer1}");
                }
                else
                {
                    Console.WriteLine($"{integer1} and {integer2} are equal");
                }
            }
        }
    }
}



