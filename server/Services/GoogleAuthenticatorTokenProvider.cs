using server.Models.Database;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using OtpNet;

namespace server.Services {
    public class GoogleAuthenticatorTokenProvider : IUserTwoFactorTokenProvider<UserInfo> {
        public Task<bool> CanGenerateTwoFactorTokenAsync(UserManager<UserInfo> manager, UserInfo user) {
            if (user.DualAuthenticationSecretKey != null) {
                return Task.FromResult(true);
            }
            return Task.FromResult(false);
        }

        public Task<string> GenerateAsync(string purpose, UserManager<UserInfo> manager, UserInfo user) {
            var key = KeyGeneration.GenerateRandomKey(10);
            var base32String = Base32Encoding.ToString(key);
            user.DualAuthenticationSecretKey = base32String;
            return Task.FromResult(base32String);
        }

        public Task<bool> ValidateAsync(string purpose, string token, UserManager<UserInfo> manager, UserInfo user) {
            var otp = new Totp(Base32Encoding.ToBytes(user.DualAuthenticationSecretKey));
            long timeStepMatched = 0;
            var isValid = otp.VerifyTotp(token, out timeStepMatched, new VerificationWindow(1, 1));
            return Task.FromResult(isValid);
        }
    }
}