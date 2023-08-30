using System;
using Library;

namespace TUI {
    internal class MainClass {
        private static void PrintArray(object[] array) {
            Console.WriteLine("Test Array: {0}", Show.Array(array));
        }

        private static void TestArray(IComparable[] array) {
            MainClass.PrintArray(array);

            for (var i = 0; i <= 11; i++) {
                var result = Search.Binary(array, i);
                if (result == -1) {
                    Console.WriteLine("{0} was not found.", i);
                } else {
                    Console.WriteLine("{0} was found in index {1}.", i, result);
                }
            }
        }

        public static void Main(string[] args) {
            var gen = new Generator();

            MainClass.TestArray(gen.NextArray(10, 10));
            MainClass.TestArray(gen.NextArray(10, 10));
            MainClass.TestArray(gen.NextArray(10, 10));
        }
    }
}