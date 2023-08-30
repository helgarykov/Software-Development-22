using System.Text;

namespace Library {
    public static class Show {
        public static string Array(object[] a) {
            var builder = new StringBuilder("{ ");

            for (var i = 0; i < a.Length; i++) {
                builder.Append(a[i]);
                if (i == a.Length - 1) {
                    break;
                }

                builder.Append(',');
            }

            builder.Append(" }");

            return builder.ToString();
        }
    }
}