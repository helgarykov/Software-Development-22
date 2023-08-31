namespace HRI.SoftwareDevelopment2022.Lecture2;

public class Exercise4
{
    /// <summary>
    /// Returns all possible 5-digit numbers the sum of which is larger than 18.
    /// </summary>
    public static void GetAllFiveDigitNumbersSumIsLargerThan18()
    {
        int sum;
        int number;
        int digit;

        for(int i = 10000;i < 100000;i++){ // zadajom cykl(loop)
            sum=0;
            number=i;
            //for loop disassembles number into digits
            //number:12345
            //digit:5
            //j:0
            //number:1234
            //digit:4
            //j:1
            //number:123
            //digit:3
            //j:2
            //...
            //number:0
            //j:5
            for(int j=0;j<5;j++){  // i dette loop lægges der alle 5 cifrer sammen, en efter en og skæres en cifra fra enden ad gangen
                // number: 12345 
                digit=number%10;
                //digit: 5 - ostacha vid dilennya na 10
                //12340/10 == 1234
                sum+=digit;
                number/=10; 
                //number: 1234 ( 12345/10 -> 1234 because both 12345 and 10 are integers)
                //and we need to cut this last number off
            }
            if(sum>18)
            {
                Console.WriteLine(i); // vi printer ud alle 5-cifrede tal hvis sum er større end 18
            }
        } 
    }
    
    
    
    
    /// <summary>
    /// Returns all possible 5-digit numbers the sum of which is larger than 18.
    /// </summary>
    /// <param name="numbers"> A List of ints.</param>
    /// <param name="target">The sum </param>
    /// <param name="partial"> The list of 5 digits</param>
    public static void GetSum(List<int> numbers, int target, List<int> partial)
    {
        SumUpRecursive(numbers, target, partial);
    }

    private static void SumUpRecursive(List<int> numbers, int target, List<int> partial)
    {
        var sum = 0;
        foreach (var number in partial) sum += number; //var sum = partial.Sum();
        
        if (sum > target && partial.Count == 5)
        {
            Console.WriteLine("Sum(" + string.Join(",", partial.ToArray()) + ")>" + target);
        }

        for (var i = 0; i < numbers.Count; i++)
        {
            List<int> remaining = new List<int>();
            var number = numbers[i];
            
            for (var j = i + 1; j < numbers.Count; j++)
            {
                remaining.Add(numbers[j]);
            }

            List<int> partialRec = new List<int>(partial);
            partialRec.Add(number);
            SumUpRecursive(remaining, target, partialRec);
        }
    }
}
     