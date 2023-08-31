namespace HRI.SoftwareDevelopment2022.Lecture1;

public class Exercise1
{
    public static void SwitchStatement()
    {
        Season season = Season.Autumn;
        switch (season)
        {
            case Season.Autumn:
            case Season.Summer:
                Console.WriteLine(
                    "It's time to get a promotion."); // if season is Autumn OR Summer, this peace of code will be executed
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
    }
}