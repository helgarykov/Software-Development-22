namespace HRI.SoftwareDevelopment2022.Lecture1
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            
            // Exe 0: On If / Else statement
            bool isGoldCustomer = true;
            //float price = 19.95f;

            /* if (isGoldCustomer)
            {
                price = 19.95f;
            }
            else
            {
                price = 29.95f;
            } */

            /* var price = (isGoldCustomer) ? 19.95f : 29.95f; // the same as IF/ELSE statement above
            Console.WriteLine(price);
            */
            

            /*
            // Call to SwitchStatement() in Exercise1
            var season = new Exercise1();
            Exercise1.SwitchStatement();
            Console.WriteLine(season);
            
            
            // Call to MaxInteger() in Exercise2
            var maxValue = new Exercise2();
            Exercise2.MaxInteger();
            Console.WriteLine(maxValue);
            
            // Call to GetPortraitOrLandscape() in Exercise3
            var portraitOrLandscape = new Exercise3();
            Exercise3.GetPortraitOrLandscape();
            Console.WriteLine(portraitOrLandscape);*/
            
            // Call to ControlSpeed() in Exercise4
            var carSpeed = new Exercise4();
            Exercise4.ControlSpeed();
            Console.WriteLine(carSpeed);
        }
        
    }

}

