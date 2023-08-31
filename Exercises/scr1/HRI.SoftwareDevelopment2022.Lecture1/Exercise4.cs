using static HRI.SoftwareDevelopment2022.Lecture1.Exercise4;

namespace HRI.SoftwareDevelopment2022.Lecture1;

public class Exercise4
{
    private static int carSpeedLimit { get; set; }

    private static int carCurrentSpeed { get; set; }


    private static int numberOfDivisions { get; set; }
    private static int penaltyPoints { get; set; }

    private static int CalculateDifference()
    {
        var difference = carCurrentSpeed - carSpeedLimit;
        return difference;
    }

    private static int CalculateNumberOfDivisions()
    {
        var calculateDifference = CalculateDifference();
        if (calculateDifference >= 5)
        {
            numberOfDivisions = calculateDifference / 5;
        }
        // TODO : need to round down odd numbers!
        return numberOfDivisions;
    }
    /// <summary>
    /// Write a program that asks the user to enter the speed limit.
    /// Once set, the program asks for the speed of a car.
    /// If the user enters a value less than the speed limit, program should display Ok on the console.
    /// If the value is above the speed limit, the program should calculate the number of demerit points.
    /// For every 5km/hr above the speed limit, 1 demerit points should be incurred and displayed on the console.
    /// If the number of demerit points is above 12, the program should display License Suspended.
    /// </summary>
    public static void ControlSpeed()
    {
        Console.WriteLine("Enter a number for your car speed limit");
        var carSpeedLimit = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enter the current speed of the car.");
        var carCurrentSpeed = Convert.ToInt32(Console.ReadLine());
        
        if (carCurrentSpeed <= carSpeedLimit)
        {
            Console.WriteLine("OK");
        } 
        else
        {
            // var calculateDifference = CalculateDifference();
            var calculateDifference = carCurrentSpeed - carSpeedLimit;
            if (calculateDifference >= 5)
            {
                penaltyPoints = CalculateNumberOfDivisions();
                Console.WriteLine($"You have exceeded the speed limit with 5km/hr and have received {penaltyPoints} penalty points.");
            }
            else if (penaltyPoints > 12)
            {
                Console.WriteLine("License Suspended");
            }
        }
    }
}