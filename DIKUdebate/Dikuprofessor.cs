using System;

namespace DIKUDebate;

public class DIKUProfessor : DikuPerson
{
    public DIKUProfessor(string name, Preparation preparation) : base(name, preparation)
    {
        MaxIntellect = 10000;
        Intellect = 10000;
        StrengthOfArgument = 10000;
        CounterArgument = 10000;
        CriticalArgument = 10000;
    }

    public override bool HasLost()
    {
        Console.WriteLine($"I am {Name} and can not lose! Muhaha!");
            return false;
    }

    protected override bool BeDrained(int amount)
    {
        if (CounterArgument < Random.Next(0, 100))
        {
            Intellect /= amount;
            Console.WriteLine($"{Name} couldn't counter the argument and have been drastically drained of intellect.");
            return true;
        }
        Console.WriteLine("I am the professor and will always win an argument.");
        return false;
    }

    public override void GetExperience()
    {
        Console.WriteLine($"{Name} has finished his degree, he cannot be greater.");
    }
}
