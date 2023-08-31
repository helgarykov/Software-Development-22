namespace HRI.SoftwareDevelopment2022.Lecture2;

public static class Exercise6
{
    /// <summary>
    /// Returns a vector of all natural numbers within the range of
    /// 1st natural number and 2nd natural number (though not larger than 100)
    /// if a given decimal number is within this range.
    /// </summary>
    /// <param name="naturalNumberOne"> 1st natural number </param>
    /// <param name="naturalNumberTwo">2nd natural number </param>
    /// <param name="decimalNumber">a decimal digit </param>
    public static void GetVector(int naturalNumberOne, int naturalNumberTwo, int digit)
    {
        if (naturalNumberTwo <= 100)
        {
            if (decimalNumber > naturalNumberOne && decimalNumber < naturalNumberTwo)
            {
                for (var i = naturalNumberOne; i <= naturalNumberTwo; i++)
                {
                    Console.WriteLine(i);
                }
            }
            else
            { 
                Console.WriteLine("Enter a decimal number that is within the bounds of the two natural numbers.");
            }
        }
        else
        {
            for (var i = naturalNumberOne; i <= 100; i++)
            {
                if (decimalNumber > naturalNumberOne && decimalNumber < naturalNumberTwo)
                {
                    Console.WriteLine(i);
                }
            }
        } 
    } 
}