using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using server.Models.Database;
using server.Models.Validation;

namespace server.Controllers {
    [Authorize]
    public class UserController : Controller {
        private readonly UserManager<UserInfo> userManager;
        private readonly SignInManager<UserInfo> signInManager;
        private readonly ILogger logger;

        public UserController( UserManager<UserInfo> userManager, SignInManager<UserInfo> signInManager, ILogger<UserController> logger) {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null) {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginUserForm model, string returnUrl = null) {

        }

    }

}

// finish

/*
https://docs.microsoft.com/en-us/aspnet/core/security/data-protection/consumer-apis/password-hashing?view=aspnetcore-5.0
https://github.com/dotnet/aspnetcore/blob/main/src/Identity/Extensions.Stores/src/IdentityUser.cs
https://www.google.com/search?channel=fs&client=ubuntu&q=csharp+virtual
https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations?view=net-5.0
https://docs.microsoft.com/en-us/aspnet/core/security/app-secrets?view=aspnetcore-5.0&tabs=linux
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity-enable-qrcodes?view=aspnetcore-5.0
https://docs.microsoft.com/en-us/aspnet/mvc/overview/security/aspnet-mvc-5-app-with-sms-and-email-two-factor-authentication
https://docs.microsoft.com/en-us/aspnet/mvc/overview/security/create-an-aspnet-mvc-5-web-app-with-email-confirmation-and-password-reset
https://docs.microsoft.com/en-us/aspnet/mvc/overview/security/aspnet-mvc-5-app-with-sms-and-email-two-factor-authentication
https://github.com/codebude/QRCoder/wiki/Advanced-usage---Payload-generators#311-one-time-password
https://docs.microsoft.com/en-us/dotnet/api/system.componentmodel.dataannotations.stringlengthattribute?view=net-5.0
https://github.com/kspearrin/Otp.NET
https://github.com/dotnet/AspNetCore.Docs/blob/main/aspnetcore/security/authentication/identity/sample/src/ASPNETCore-IdentityDemoComplete/IdentityDemo/Controllers/AccountController.cs

*/