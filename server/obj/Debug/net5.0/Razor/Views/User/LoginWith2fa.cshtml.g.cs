#pragma checksum "D:\Projects\CS6305_FinalProject\server\Views\User\LoginWith2fa.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "51adfa9883687fa05a01a3b5757fca1e63e569a2"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_LoginWith2fa), @"mvc.1.0.view", @"/Views/User/LoginWith2fa.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "D:\Projects\CS6305_FinalProject\server\Views\_ViewImports.cshtml"
using server;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Projects\CS6305_FinalProject\server\Views\_ViewImports.cshtml"
using server.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"51adfa9883687fa05a01a3b5757fca1e63e569a2", @"/Views/User/LoginWith2fa.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"59a25cb1be2527689937de36e98276a658ab234f", @"/Views/_ViewImports.cshtml")]
    public class Views_User_LoginWith2fa : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<server.Models.Validation.LoginWith2faViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\Projects\CS6305_FinalProject\server\Views\User\LoginWith2fa.cshtml"
  
    ViewData["Title"] = "Dual Authentication";
    string returnUrl = (string)ViewData["ReturnUrl"];

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<div class=""centered rounded text-center"">
    <section class=""row"">
        <div class=""col"">
            <h1>Enter your Authentication Code</h1>
        </div>
    </section>
    <section class=""row"">
        <div class=""col-3""></div>
        <div class=""col-6"">
");
#nullable restore
#line 16 "D:\Projects\CS6305_FinalProject\server\Views\User\LoginWith2fa.cshtml"
             using (Html.BeginForm())
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                <div class=\"form-group\">\r\n                    ");
#nullable restore
#line 19 "D:\Projects\CS6305_FinalProject\server\Views\User\LoginWith2fa.cshtml"
               Write(Html.LabelFor(m => m.DualAuthCode));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    ");
#nullable restore
#line 20 "D:\Projects\CS6305_FinalProject\server\Views\User\LoginWith2fa.cshtml"
               Write(Html.TextBoxFor(m => m.DualAuthCode, new { @class = "form-control", @PlaceHolder = "000000", @autofocus = "true"}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    ");
#nullable restore
#line 21 "D:\Projects\CS6305_FinalProject\server\Views\User\LoginWith2fa.cshtml"
               Write(Html.ValidationMessageFor(m => m.DualAuthCode));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </div>\r\n                <input type=\"submit\" class=\"btn btn-primary\">\r\n");
#nullable restore
#line 24 "D:\Projects\CS6305_FinalProject\server\Views\User\LoginWith2fa.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n    </section>\r\n</div>\r\n\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 30 "D:\Projects\CS6305_FinalProject\server\Views\User\LoginWith2fa.cshtml"
       await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
#nullable disable
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<server.Models.Validation.LoginWith2faViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
