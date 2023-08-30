using System.Collections.Generic;

namespace DIKUDebate
{
    namespace System.Collections.Generic
    {
        public static class Program
        {
            public static void Main(string[] args)
            {
                DIKUStudent uffe = new DIKUStudent("Uffe", Preparation.ReadingSome);
                DIKUStudent marianne = new DIKUStudent("Marianne", Preparation.ReadingNone);
                DIKUStudent sonja = new DIKUStudent("Sonja", Preparation.ReadingAll);
                DIKUStudent liva = new DIKUStudent("Liva", Preparation.ReadingAll);
                DIKUStudent martin = new DIKUStudent("Martin", Preparation.ReadingNone);
                DIKUStudent elise = new DIKUStudent("Elise", Preparation.ReadingSome);
                DIKUProfessor boris = new DIKUProfessor("Professor Boris", Preparation.ReadingAll);
                Classroom classroom = new Classroom();

                classroom.Discuss(uffe, marianne);
                classroom.Discuss(sonja, boris);
                classroom.Discuss(marianne, sonja);
                var debateParticipants = new List<DikuPerson>
                {
                    uffe,
                    marianne,
                    sonja,
                    liva,
                    martin,
                    elise,
                    boris,
                };
                // Important that the call to RunDebate() takes place after a new list object been initialized.
                classroom.RunDebate(debateParticipants);
            }
        }
    }
}

