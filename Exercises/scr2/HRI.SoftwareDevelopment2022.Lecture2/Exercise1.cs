using System.Diagnostics;

namespace HRI.SoftwareDevelopment2022.Lecture2;

public class Exercise1
{
    // private string? firstName { get; }
    // private string? lastName { get; }
    //
    // private static string[]? name { get; set; }

    // public ReverseName(string FirstName, string LastName)
    // {
    //     firstName = FirstName;
    //     lastName = LastName;
    //     name = new[] {firstName, lastName};
    // }
    
    /// <summary>
    /// Prompts the users first name and last name from the console.
    /// Provides the output as first name and last name of the user.
    /// Reverses the order and outputs the name in the reverse order.
    /// </summary>
    public static void GetReversedName()
    {
        Console.WriteLine("Enter your first name.");
        var firstName = Convert.ToString(Console.ReadLine());

        Console.WriteLine("Enter your last name.");
        var lastName = Convert.ToString(Console.ReadLine());
        
        var myName = new[] {firstName, lastName};
        Console.Write("My name is");
        foreach (var element in myName)
        {
            Console.Write(" " + element + " ");
        }
        
        Array.Reverse(myName);
        Console.WriteLine();
        Console.Write("My reversed name is");
        foreach (var element in myName)
        {
            Console.Write(" " + element + " ");
        }
    }
}