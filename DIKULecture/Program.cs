using System.Runtime.CompilerServices;
using Microsoft.VisualBasic;
using System;

namespace DIKULecture
{
    class Program
    {
        static void Main(string[] args)
        {
            Lecture lecture = new("DMA");
            Lecture lectureTwo = new("PoP");
            Console.WriteLine(lecture);

            Student a = new("Ove", "student", 18);
            Student c = new("Tove", "student", 22);
            Student d = new("Marie", "student", 30);
            Student e = new("Carla", "student", 18);
            Student f = new("Emil", "student", 26);
            Student g = new("Waldemar", "student", 18);
            
            Speaker b = new("Boris", "teacher", 45);
            
            a.Join(lecture, "Ove");
            c.Join(lecture, "Tove");
            d.Join(lecture, "Marie");
            e.Join(lecture, "Carla");
            f.Join(lecture, "Emil");
            g.Join(lecture, "Waldemar");
            
            b.Broadcast(lecture);
            b.Speak(true);
            b.Rename("PoP");

            a.Listen();
            c.Listen();
            d.Listen();
            e.Listen();
            f.Listen();
            g.Listen();
        }
    }
}






