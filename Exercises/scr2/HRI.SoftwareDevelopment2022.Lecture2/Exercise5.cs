namespace HRI.SoftwareDevelopment2022.Lecture2;

public static class Exercise5
{
    /// <summary>
    /// Calculate the arithmetic mean of all the numbers is an int-array.
    /// Set all numbers that are larger than the mean to 0.
    /// Set the largest number in the array to the value of the arithmetic mean.
    /// </summary>
    /// <param name="myArray"> an array of integers </param>
    public static void ManipulateArray(int[] myArray)
    {
        Console.WriteLine("\n The original array is " + string.Join(",", myArray));
        
        var average = myArray.Average();
        var maxValue = myArray.Max();
        for (var i = 0; i < myArray.Length; i++)
        {
            var number = myArray[i];
            if (number > average && number != maxValue)
            {
                Array.Clear(myArray, i, 1);
            }
            else if (number == maxValue)
            {
                myArray[i] = (int) average;
            }
        }
        Console.WriteLine("The mean is " + average + 
                          "\n All numbers larger than the arithmetic mean are \n" +
                          "set to zero and the max number is set to the arithmetic mean \n" 
                          + string.Join(",", myArray));
    }
}