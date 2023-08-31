namespace HRI.SoftwareDevelopment2022.Lecture1;

public class Exercise2
{
    private int integer1 { get; }
    private int integer2 { get; }

    public static void MaxInteger()
    {
        Console.WriteLine("Enter a number");
        var integer1 = Convert.ToInt32(Console.ReadLine());
        
        Console.WriteLine("Enter one more number");
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