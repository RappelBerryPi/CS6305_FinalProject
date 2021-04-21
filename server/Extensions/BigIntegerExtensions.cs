using System;
using System.Numerics;

namespace server.Extensions {
    public static class BigIntegerExtensions {
        public static DateTimeOffset ToDateTimeOffset(this BigInteger bigInteger) {
            if (bigInteger.IsZero) {
                return DateTimeOffset.UnixEpoch;
            }
            ulong TimeStampAsULong = (ulong) bigInteger;
            return DateTimeOffset.FromUnixTimeSeconds(TimeStampAsULong.ToLong());

        }
    }
}