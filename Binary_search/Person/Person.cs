using System;

namespace PersonNS {
    public class Person : IComparable 
    {
        public Person(string name, int age) 
        {
            this.name = name;
            this.age = age;
        }

        private int age;
        private string name;

        public int CompareTo(object other) {
            if (other is null) {
                return -1;
            }

            // Small opmtimisation
            if (Object.ReferenceEquals(this, other)) {
                return 0;
            }

            // Cast to person first
            if (other is Person otherPerson) {
                // Reversed because we want oldest first
                var compareAge = otherPerson.age.CompareTo(this.age);
                if (compareAge != 0) {
                    return compareAge;
                } else {
                    // Otherwise sort them by name alphabetically, ignoring case
                    return String.Compare(this.name, otherPerson.name, true);
                }
            } else {
                // If we compare to something that isn't a person, something has gone wrong
                throw new ArgumentException("Object is not a Person");
            }
        }

        public override string ToString() {
            return $"Person({name}, {age})";
        }
    }
}
