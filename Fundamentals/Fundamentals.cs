using System;
namespace Fundamentals;

public class Fundamentals
{
    public void CountBackwards()
    {
        Console.WriteLine("Input: None");
        Console.WriteLine();
        Console.Write("PrintOutput: ");
        for (int i = 10; i > 0; --i)
        {
            Console.Write(i + ", ");
        }
        Console.WriteLine(" Finished!");
        Console.WriteLine();
        Console.WriteLine("Returns: Nothing. The return type is void.");
        Console.WriteLine();
    }
    // Recursive function is a pure function and does not return anything
    // else than what is conditioned by the input.
    public string ReverseString(string word)
    {
        if (word.Length <= 1)
        {
            return word;
        }
        else
        {
            // Prints out the original string in a reversed order.
            // Starts with the last char and prints them out one by one.
            // The first char printed out in "stressed :(" is "(".
            Console.Write(word[word.Length - 1]);
            // First round: Takes the char that corresponds to the one printed out above
            // and cuts it off the original string. The first char cut off is "(".
            // Second round: Adds chars one by one, starting from index [1] and to
            // [word.Length - 1]. The final string == the original string.
            ReverseString(word.Substring(0,(word.Length - 1)));
            return null!;
        }
    }
    public int GCD(int a, int b)
    {
        if (a == 0 && b == 0) return 0;
        int r = a % b;
        if (r == 0) return Math.Abs(b);
        return GCD(b, r);
    }
}
