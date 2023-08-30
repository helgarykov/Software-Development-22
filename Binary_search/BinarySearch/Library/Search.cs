using System;

namespace Library {
    public static class Search {
        public static int Binary(IComparable[] array, IComparable target) {
            var low = 0;
            var high = array.Length - 1;

            while (low <= high) {
                var mid = (high + low) / 2;
                var midVal = array[mid];
                var relation = midVal.CompareTo(target);

                if (relation < 0) {
                    low = mid + 1;
                } else if (relation > 0) {
                    high = mid - 1;
                } else {
                    return mid;
                }
            }

            return -1;
        }
    }
}