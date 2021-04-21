namespace server.Extensions {
    public static class LongExtensions {
        public static ulong ToULong(this long inputLong) {
            return unchecked((ulong)(inputLong - long.MinValue));
        }

        public static long ToLong(this ulong inputULong) {
            return (long) inputULong;
            //return unchecked((long) inputULong + long.MinValue);
        }
    }
}