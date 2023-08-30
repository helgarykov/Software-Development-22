public static int CollatzRec(int n, int max_len, int max_size) {
    if(n < 1 || n > max_size || max_len <= 0) {
        return 0;
    } else if (n == 1) {
        return 1;
    } else {
        return 1 + (n % 2 == 0
            ? CollatzRec(n/2,max_len-1,max_size)
            : CollatzRec(3*n+1,max_len-1,max_size));
    }
}