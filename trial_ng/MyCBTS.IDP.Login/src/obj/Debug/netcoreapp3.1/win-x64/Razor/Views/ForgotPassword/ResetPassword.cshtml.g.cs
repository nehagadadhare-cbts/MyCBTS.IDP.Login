#pragma checksum "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\ForgotPassword\ResetPassword.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "061f293f3645a7f36c7127da69232aea160c091e"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ForgotPassword_ResetPassword), @"mvc.1.0.view", @"/Views/ForgotPassword/ResetPassword.cshtml")]
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
#line 1 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\_ViewImports.cshtml"
using MyCBTS.IDP.Login;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\_ViewImports.cshtml"
using MyCBTS.IDP.Login.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"061f293f3645a7f36c7127da69232aea160c091e", @"/Views/ForgotPassword/ResetPassword.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"656906cfe79154ce9d1e91b643da83ccea2adb12", @"/Views/_ViewImports.cshtml")]
    public class Views_ForgotPassword_ResetPassword : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<MyCBTS.IDP.Login.Models.ForgotPasswordModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("color:#9a0000; font-weight:bold;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("color:#9a0000; font-weight:bold"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\ForgotPassword\ResetPassword.cshtml"
  
    ViewData["Title"] = "ForgotPassword";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>ForgotPassword</h1>\r\n\r\n<h4>Reset your password </h4>\r\n");
#nullable restore
#line 10 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\ForgotPassword\ResetPassword.cshtml"
 if (Model.PasswordReset)
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <h3>Password Reset Successfully.</h3>\r\n");
#nullable restore
#line 13 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\ForgotPassword\ResetPassword.cshtml"
}
else
{
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\ForgotPassword\ResetPassword.cshtml"
     using (Html.BeginForm("ResetPassword", "ForgotPassword", FormMethod.Post))
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("<div>\r\n    ");
#nullable restore
#line 19 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\ForgotPassword\ResetPassword.cshtml"
Write(Html.HiddenFor(user => user.UserId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    ");
#nullable restore
#line 20 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\ForgotPassword\ResetPassword.cshtml"
Write(Html.HiddenFor(user => user.EmailAddress));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    <div>\r\n        <div>\r\n");
#nullable restore
#line 23 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\ForgotPassword\ResetPassword.cshtml"
              
                if (ViewData["ErrorResponseMessage"] != null)
                {
                    var errorMessage = ViewData["ErrorResponseMessage"];

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <div>\r\n                        <span style=\"font-size:medium;color:red\">");
#nullable restore
#line 28 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\ForgotPassword\ResetPassword.cshtml"
                                                            Write(errorMessage);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                    </div>\r\n");
#nullable restore
#line 30 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\ForgotPassword\ResetPassword.cshtml"
                }
            

#line default
#line hidden
#nullable disable
            WriteLiteral("        </div>\r\n        <table>\r\n            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 36 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\ForgotPassword\ResetPassword.cshtml"
               Write(Html.PasswordFor(m => m.Password, new { id = "txtPassword", placeholder = "Password" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "061f293f3645a7f36c7127da69232aea160c091e7662", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#nullable restore
#line 37 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\ForgotPassword\ResetPassword.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Password);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n            <tr>\r\n                <td>\r\n                    <label>Re-enter Password:</label>\r\n                    ");
#nullable restore
#line 43 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\ForgotPassword\ResetPassword.cshtml"
               Write(Html.PasswordFor(m => m.ReEnterPassword, new { id = "txtRePassword", placeholder = "Password" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "061f293f3645a7f36c7127da69232aea160c091e9836", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#nullable restore
#line 44 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\ForgotPassword\ResetPassword.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.ReEnterPassword);

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-validation-for", __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n        </table>\r\n    </div>\r\n    <div>\r\n        <input type=\"submit\" name=\"submit\" value=\"Submit\" />\r\n    </div>\r\n</div>\r\n");
#nullable restore
#line 53 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\ForgotPassword\ResetPassword.cshtml"
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 53 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\ForgotPassword\ResetPassword.cshtml"
     
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\r\n");
#nullable restore
#line 57 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\ForgotPassword\ResetPassword.cshtml"
      await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
#nullable disable
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public IHttpContextAccessor httpContextAccessor { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<MyCBTS.IDP.Login.Models.ForgotPasswordModel> Html { get; private set; }
    }
}
#pragma warning restore 1591