#pragma checksum "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Shared\Registration\_RegistrationStep1.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "53e0b9e8981cbd61fb4d7aa494ce3afaff8dfcaf"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Registration__RegistrationStep1), @"mvc.1.0.view", @"/Views/Shared/Registration/_RegistrationStep1.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"53e0b9e8981cbd61fb4d7aa494ce3afaff8dfcaf", @"/Views/Shared/Registration/_RegistrationStep1.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"656906cfe79154ce9d1e91b643da83ccea2adb12", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Registration__RegistrationStep1 : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<MyCBTS.IDP.Login.Models.RegisterViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("color:#9a0000; font-weight:bold"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("color:#9a0000; font-weight:bold;"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\r\n");
#nullable restore
#line 3 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Shared\Registration\_RegistrationStep1.cshtml"
 using (Html.BeginForm("RegistrationStep1", "Registration", FormMethod.Post))
{

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div>\r\n        <div id=\"pnlSignUp\">\r\n            <div>\r\n");
#nullable restore
#line 8 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Shared\Registration\_RegistrationStep1.cshtml"
                  
                    if (ViewData["ErrorResponseMessage"] != null)
                    {
                        var errorMessage = ViewData["ErrorResponseMessage"];

#line default
#line hidden
#nullable disable
            WriteLiteral("                        <div>\r\n                            <span style=\"font-size:medium;color:red\">");
#nullable restore
#line 13 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Shared\Registration\_RegistrationStep1.cshtml"
                                                                Write(errorMessage);

#line default
#line hidden
#nullable disable
            WriteLiteral("</span>\r\n                        </div>\r\n");
#nullable restore
#line 15 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Shared\Registration\_RegistrationStep1.cshtml"
                    }
                

#line default
#line hidden
#nullable disable
            WriteLiteral("            </div>\r\n            <table class=\"my td-no-border\">\r\n                <tr>\r\n                    <td>\r\n                        <label>First Name:</label>\r\n                        ");
#nullable restore
#line 22 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Shared\Registration\_RegistrationStep1.cshtml"
                   Write(Html.TextBoxFor(m => m.FirstName, new { placeholder = "First Name" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "53e0b9e8981cbd61fb4d7aa494ce3afaff8dfcaf6451", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#nullable restore
#line 23 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Shared\Registration\_RegistrationStep1.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.FirstName);

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
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n                <tr>\r\n                    <td>\r\n                        <label>Last name:</label>\r\n                        ");
#nullable restore
#line 29 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Shared\Registration\_RegistrationStep1.cshtml"
                   Write(Html.TextBoxFor(m => m.LastName, new { placeholder = "Last Name" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "53e0b9e8981cbd61fb4d7aa494ce3afaff8dfcaf8641", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#nullable restore
#line 30 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Shared\Registration\_RegistrationStep1.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.LastName);

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
            WriteLiteral(@"

                        <hr class=""gray margtb"" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Account Number: <a href=""https://my.cincinnatibell.com/SelfCare/sign-up/help/bill-example.aspx"" target=""_blank"">Where do I find it?</a></label>
                        ");
#nullable restore
#line 38 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Shared\Registration\_RegistrationStep1.cshtml"
                   Write(Html.TextBoxFor(m => m.AccountNumber, new { placeholder = "Account Number" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "53e0b9e8981cbd61fb4d7aa494ce3afaff8dfcaf11014", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#nullable restore
#line 39 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Shared\Registration\_RegistrationStep1.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.AccountNumber);

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
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n                <tr>\r\n                    <td>\r\n                        <label>Account Nickname:</label>\r\n                        ");
#nullable restore
#line 45 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Shared\Registration\_RegistrationStep1.cshtml"
                   Write(Html.TextBoxFor(m => m.AccountNickName, new { @class = "bg-white sign-up-input", placeholder = "Nickname" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        <br /><span class=\"dark-gray italic marg-left\">(e.g.; My Internet Account)</span>\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "53e0b9e8981cbd61fb4d7aa494ce3afaff8dfcaf13368", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#nullable restore
#line 47 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Shared\Registration\_RegistrationStep1.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.AccountNickName);

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
            WriteLiteral("\r\n                        <hr class=\"gray margtb\" />\r\n                    </td>\r\n                </tr>\r\n                <tr>\r\n                    <td>\r\n                        <label>Email Address:</label>\r\n                        ");
#nullable restore
#line 54 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Shared\Registration\_RegistrationStep1.cshtml"
                   Write(Html.TextBoxFor(m => m.EmailAddress, new { placeholder = "Email Address" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "53e0b9e8981cbd61fb4d7aa494ce3afaff8dfcaf15633", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#nullable restore
#line 55 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Shared\Registration\_RegistrationStep1.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.EmailAddress);

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
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n                <tr>\r\n                    <td>\r\n                        <label>Re-enter Email Address:</label>\r\n                        ");
#nullable restore
#line 61 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Shared\Registration\_RegistrationStep1.cshtml"
                   Write(Html.TextBoxFor(m => m.ReEmailAddress, new { placeholder = "Email Address" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "53e0b9e8981cbd61fb4d7aa494ce3afaff8dfcaf17850", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#nullable restore
#line 62 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Shared\Registration\_RegistrationStep1.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.ReEmailAddress);

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
            WriteLiteral(@"
                        <hr class=""gray margtb"" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <label>Choose a Password:</label>
                        <p>Your password must be 7 to 20 characters in length and contain alphanumeric characters (0-9, A-Z or a-z) or non-alphabetic characters (like %, #, $).</p>
                    </td>
                </tr>
                <tr>
                    <td>
                        ");
#nullable restore
#line 74 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Shared\Registration\_RegistrationStep1.cshtml"
                   Write(Html.PasswordFor(m => m.Password, new { id = "txtPassword", placeholder = "Password" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "53e0b9e8981cbd61fb4d7aa494ce3afaff8dfcaf20397", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#nullable restore
#line 75 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Shared\Registration\_RegistrationStep1.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Password);

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
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n                <tr>\r\n                    <td>\r\n                        <label>Re-enter Password:</label>\r\n                        ");
#nullable restore
#line 81 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Shared\Registration\_RegistrationStep1.cshtml"
                   Write(Html.PasswordFor(m => m.ReEnterPassword, new { id = "txtRePassword", placeholder = "Password" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "53e0b9e8981cbd61fb4d7aa494ce3afaff8dfcaf22624", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#nullable restore
#line 82 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Shared\Registration\_RegistrationStep1.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.ReEnterPassword);

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
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n\r\n                <tr>\r\n                    <td>\r\n                        <label>Mobile Number:(optional)</label>\r\n                        ");
#nullable restore
#line 89 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Shared\Registration\_RegistrationStep1.cshtml"
                   Write(Html.TextBoxFor(m => m.strPhoneNumber, new { Maxlength = "12", placeholder = "XXX-XXX-XXXX" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "53e0b9e8981cbd61fb4d7aa494ce3afaff8dfcaf24866", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#nullable restore
#line 90 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Shared\Registration\_RegistrationStep1.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.strPhoneNumber);

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
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n                <tr>\r\n                    <td>\r\n                        <label>Title: </label>\r\n                        ");
#nullable restore
#line 96 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Shared\Registration\_RegistrationStep1.cshtml"
                   Write(Html.TextBoxFor(m => m.Title, new { Maxlength = "10", placeholder = "Title" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "53e0b9e8981cbd61fb4d7aa494ce3afaff8dfcaf27070", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#nullable restore
#line 97 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Shared\Registration\_RegistrationStep1.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.Title);

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
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n                <tr>\r\n                    <td>\r\n                        <label>Brand Name: </label>\r\n                        ");
#nullable restore
#line 103 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Shared\Registration\_RegistrationStep1.cshtml"
                   Write(Html.DropDownListFor(m => m.BrandName, new List<SelectListItem> {
                                                            new SelectListItem() { Text="MyCBTS", Value = "MyCBTS", Selected=true},
                                                            new SelectListItem() { Text="OnX", Value = "OnX"}
                                                            }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("span", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "53e0b9e8981cbd61fb4d7aa494ce3afaff8dfcaf29566", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ValidationMessageTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper);
#nullable restore
#line 107 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Shared\Registration\_RegistrationStep1.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ValidationMessageTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.BrandName);

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
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n            </table>\r\n        </div>\r\n        <div>\r\n            <input type=\"submit\" name=\"ibnContinue\" value=\"Continue\" id=\"ibnContinue\" />\r\n        </div>\r\n    </div>\r\n");
#nullable restore
#line 116 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Shared\Registration\_RegistrationStep1.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<MyCBTS.IDP.Login.Models.RegisterViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591