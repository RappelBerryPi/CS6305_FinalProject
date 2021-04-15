using System;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Nethereum.Hex.HexConvertors.Extensions;
using server.Models.Database;
using server.Models.Validation;

namespace server.Controllers {
    [Authorize]
    public class UserController : Controller {
        private readonly UserManager<UserInfo> userManager;
        private readonly SignInManager<UserInfo> signInManager;
        private readonly ILogger logger;

        private readonly DefaultContext context;

        public UserController(UserManager<UserInfo> userManager, SignInManager<UserInfo> signInManager, ILogger<UserController> logger, DefaultContext context) {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
            this.context = context;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null) {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserForm model, string returnUrl = null) {
            ViewData["ReturnURL"] = returnUrl;
            if (ModelState.IsValid) {
                var result = await this.signInManager.PasswordSignInAsync(model.UserName, model.Password, true, false);
                if (result.RequiresTwoFactor) {
                    return RedirectToAction(nameof(LoginWith2fa), new { returnUrl });
                } else {
                    ModelState.AddModelError(string.Empty, "Login Invalid.");
                    return View(model);
                }
            }
            return View(model);

        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginWith2fa(string returnUrl = null) {
            var user = await this.signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null) {
                throw new ApplicationException("Unable to load two-factor authentication user.");
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginWith2fa(LoginWith2faViewModel model, string returnUrl = null) {
            if (!ModelState.IsValid) {
                return View(model);
            }

            var user = await this.signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null) {
                throw new ApplicationException("unable to load user");
            }

            var result = await this.userManager.VerifyTwoFactorTokenAsync(user, "GoogleAuthenticator", model.DualAuthCode);
            if (result) {
                await this.signInManager.SignInAsync(user, false);
                if (Url.IsLocalUrl(returnUrl)) {
                    return Redirect(returnUrl);
                } else {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            } else {
                ModelState.AddModelError(string.Empty, "Invalid Authenticator code.");
                return View();
            }

        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult CreateNewUser(string returnUrl = null) {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateNewUser(NewUserForm newUserForm, string returnUrl = null) {
            if (ModelState.IsValid) {
                var user = new UserInfo();

                // Generate blockchain address 
                var ecKey = Nethereum.Signer.EthECKey.GenerateKey();
                var privatekey = ecKey.GetPrivateKeyAsBytes().ToHex();
                var account = new Nethereum.Web3.Accounts.Account(privatekey);

                user.UserName = newUserForm.UserName;
                user.Email = newUserForm.EmailAddress;
                user.FirstName = newUserForm.FirstName;
                user.LastName = newUserForm.LastName;
                user.TwoFactorEnabled = true;
                user.BlockchainAddress = account.Address;
                user.PrivateKey = account.PrivateKey;
                user.EmailConfirmed = true;

                var result = await userManager.CreateAsync(user, newUserForm.Password);
                if (result.Succeeded) {
                    await this.signInManager.SignInAsync(user, false);
                    return RedirectToAction(nameof(GetDualAuthCode), returnUrl); 
                }
            }
            return View(newUserForm);
        }

        [HttpGet]
        public async Task<IActionResult> GetDualAuthCode(string ReturnUrl) {
            UserInfo user = userManager.GetUserAsync(this.User).GetAwaiter().GetResult();
            await userManager.GenerateTwoFactorTokenAsync(user, "GoogleAuthenticator");
            this.context.Update(user);
            this.context.SaveChanges();
            const string AuthenticatorUriFormat = "otpauth://totp/{0} ({1})?secret={2}&issuer={0}&digits=6";
            ViewData["dualAuthKey"] = System.String.Format(AuthenticatorUriFormat, "BlockMart", user.UserName, user.DualAuthenticationSecretKey);
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout(UserInfo user) {
            await this.signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }

}