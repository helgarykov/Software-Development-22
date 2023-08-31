namespace HRI.SoftwareDevelopment2022.Lecture1;

public class Exercise3
{
    private int width { get; }
    private int breadth { get; }

    public static void GetPortraitOrLandscape()
    {
        Console.WriteLine("Enter a number for width");
        var width = Convert.ToInt32(Console.ReadLine());
        
        Console.WriteLine("Enter a number for breadth");
        var breadth = Convert.ToInt32(Console.ReadLine());

        if (width > breadth || width < breadth)
        {
            Console.WriteLine("You have got a landscape.");
        }
        else
        {
            Console.WriteLine("You have got a portrait.");
        }
    }
    
}