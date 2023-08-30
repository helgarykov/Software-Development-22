namespace DIKUDebate;

public class DIKUStudent : DikuPerson
{
    public DIKUStudent(string name, Preparation preparation) : base(name, preparation)
    {
        Intellect = 30;
        MaxIntellect = 30;
        StrengthOfArgument = 3;
        Semester = 1;
        CounterArgument = 10;
        CriticalArgument = 10;
    }
}

