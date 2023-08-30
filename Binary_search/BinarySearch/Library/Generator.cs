using System;

namespace Library {
    public class Generator {
        // Type of "function" used in `NextArray` that converts an int to an `IComparable`
        public delegate IComparable Initializer(int value);

        private Random rand;

        public Generator() {
            rand = new Random();
        }

        public int NextInt(int maxValue) {
            return rand.Next(maxValue);
        }

        public IComparable[] NextArray(int size, int maxValue) {
            return NextArray(size, maxValue, (i) => i);
        }

        public IComparable[] NextArray(int size, int maxValue, Initializer initializer) {
            var array = new IComparable[size];

            for (var i = 0; i < size; i++) {
                array[i] = initializer(rand.Next(maxValue));
            }

            Array.Sort(array);

            return array;
        }
    }
}
