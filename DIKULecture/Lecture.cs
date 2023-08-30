namespace DIKULecture;

public class Lecture : ChatRoom
{
    private int numOfStudentsOnline = 0;
    private bool information;

    public bool Information
    {
        get { return information; }
        set { information = value; }
    }

    public int NumOfStudents
    {
        get { return numOfStudentsOnline; }
        set { numOfStudentsOnline = value; }
    }

    public Lecture(string? name)
    {
        Name = name;
    }

    public override string ToString()
    {
        return string.Format("\n" + Name + " is currently attended by " + NumOfStudents + " students");
    }
}