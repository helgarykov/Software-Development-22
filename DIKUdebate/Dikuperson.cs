using System;

namespace DIKUDebate;

public class DikuPerson
{
    private protected readonly Random Random = new Random();
    // Can be read and overwritten by itself and its subclasses.
    private protected string Name { get; set; }
    private protected int MaxIntellect { get; set; }
    private protected int Intellect { get; set; }
    private protected int StrengthOfArgument{ get; set; }
    private protected int Semester { get; set; }
    private protected int CounterArgument { get; set; }
    private protected int CriticalArgument { get; set; }
    // Can be read and overwritten only by itself.
    private Preparation Preparation { get; }

    protected DikuPerson(string name, Preparation preparation)
    {
        Name = name;
        Preparation = preparation;
    }
    //Prints objects as a string.
    public override string ToString()
    {
        return string.Format(Name + " who is " + Preparation + " with intellect " + Intellect);
    }

    public virtual bool HasLost()
    {
        if (Intellect <= 0) return true;
        return false;
    }

    protected virtual bool BeDrained(int amount)
    {
        if (!(CounterArgument > Random.Next(0, 100)))
        {
            Intellect -= amount;
            return true;
        }
        Console.WriteLine($"{Name} succeeded to counter the argument.");
        return false;
    }

    public void Argue(DikuPerson opponent)
    {
        Console.WriteLine($"DikuPerson {Name} ({Preparation}) strikes an argument at DikuPerson {opponent.Name} ({opponent.Preparation}) for {StrengthOfArgument} points of draining.");
        if (CriticalArgument > Random.Next(0, 100))
        {
            StrengthOfArgument += 2;
            opponent.Intellect -= StrengthOfArgument;
            opponent.BeDrained(StrengthOfArgument);
            Console.WriteLine($"DikuPerson {opponent.Name} lost the argument for {StrengthOfArgument} points of draining.");
            Console.WriteLine();
            return;
        }
        opponent.Intellect -= StrengthOfArgument;
        opponent.GetExperience();
        Console.WriteLine($"{opponent.Name} managed to counter the argument.");
        Console.WriteLine();
    }

    public virtual void GetExperience()
    {
        Semester++;
        StrengthOfArgument += 2;
        if (Preparation is Preparation.ReadingAll or Preparation.ReadingSome) MaxIntellect += 10;
        if (Preparation == Preparation.ReadingNone) MaxIntellect += 20;
        if (Preparation == Preparation.ReadingNone) CounterArgument += 3;
        if (Preparation is Preparation.ReadingAll or Preparation.ReadingSome) CounterArgument += 6;
        if (Preparation == Preparation.ReadingAll) CriticalArgument += 6;
        if (Preparation is Preparation.ReadingNone or Preparation.ReadingSome) CriticalArgument += 3;
        Intellect = MaxIntellect;
    }
}