#pragma checksum "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\ForgotPassword\UsernameDetails.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4f0f72f9a115b80f8c4691af027556d32775d1e3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_ForgotPassword_UsernameDetails), @"mvc.1.0.view", @"/Views/ForgotPassword/UsernameDetails.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4f0f72f9a115b80f8c4691af027556d32775d1e3", @"/Views/ForgotPassword/UsernameDetails.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"656906cfe79154ce9d1e91b643da83ccea2adb12", @"/Views/_ViewImports.cshtml")]
    public class Views_ForgotPassword_UsernameDetails : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<MyCBTS.IDP.Login.Models.ForgotUserNameModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/css/app.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form form_standard mt-5 cb-px-md py-5 mx-auto"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\ForgotPassword\UsernameDetails.cshtml"
  
    Layout = null;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\ForgotPassword\UsernameDetails.cshtml"
  
    List<string> userNameList = Model.UserNameList;

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<!DOCTYPE html>
<!--[if lt IE 7]>      <html class=""no-js lt-ie9 lt-ie8 lt-ie7""> <![endif]-->
<!--[if IE 7]>         <html class=""no-js lt-ie9 lt-ie8""> <![endif]-->
<!--[if IE 8]>         <html class=""no-js lt-ie9""> <![endif]-->
<!--[if gt IE 8]><!-->
<html class=""no-js"">
<!--<![endif]-->
");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4f0f72f9a115b80f8c4691af027556d32775d1e35966", async() => {
                WriteLiteral("\r\n    <meta charset=\"utf-8\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\r\n    <title></title>\r\n    <meta name=\"description\"");
                BeginWriteAttribute("content", " content=\"", 580, "\"", 590, 0);
                EndWriteAttribute();
                WriteLiteral(">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "4f0f72f9a115b80f8c4691af027556d32775d1e36619", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4f0f72f9a115b80f8c4691af027556d32775d1e38501", async() => {
                WriteLiteral(@"
    <!--[if lt IE 7]>
        <p class=""browsehappy"">You are using an <strong>outdated</strong> browser. Please <a href=""#"">upgrade your browser</a> to improve your experience.</p>
    <![endif]-->
    <div class=""container-fluid my-5"">
        <div class=""row"">
            <div class=""col-12"">
                <div class=""spacer py-2 my-3""></div>
");
#nullable restore
#line 31 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\ForgotPassword\UsernameDetails.cshtml"
                  await Html.RenderPartialAsync("_BrandLogo");

#line default
#line hidden
#nullable disable
                WriteLiteral("            </div>\r\n");
#nullable restore
#line 33 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\ForgotPassword\UsernameDetails.cshtml"
             if (userNameList != null && userNameList.Count > 0)
            {

#line default
#line hidden
#nullable disable
                WriteLiteral("            <div class=\"col-12\">\r\n                <div class=\"cb-form\">\r\n                    <div class=\"spacer py-2 my-3\"></div>\r\n                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "4f0f72f9a115b80f8c4691af027556d32775d1e39932", async() => {
                    WriteLiteral("\r\n                        <h2 class=\"py-3 text-center\">Existing Profiles</h2>\r\n                        <p class=\"text-center\">The account number you supplied, ");
#nullable restore
#line 40 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\ForgotPassword\UsernameDetails.cshtml"
                                                                           Write(Model.AccountNumber);

#line default
#line hidden
#nullable disable
                    WriteLiteral(" has the following username(s) associated with it.</p>\r\n\r\n                        <div class=\"py-5\">\r\n");
#nullable restore
#line 43 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\ForgotPassword\UsernameDetails.cshtml"
                             foreach (var user in userNameList)
                            {

#line default
#line hidden
#nullable disable
                    WriteLiteral("                                <p class=\"text-center\">");
#nullable restore
#line 45 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\ForgotPassword\UsernameDetails.cshtml"
                                                  Write(user);

#line default
#line hidden
#nullable disable
                    WriteLiteral("</p>\r\n");
#nullable restore
#line 46 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\ForgotPassword\UsernameDetails.cshtml"
                            }

#line default
#line hidden
#nullable disable
                    WriteLiteral("                        </div>\r\n                        <div class=\"form-group text-center\">\r\n                            <a type=\"submit\" class=\"btn btn-secondary btn-lg cb-btn text-white cb-btn-text-primary\"");
                    BeginWriteAttribute("onclick", " onclick=\"", 2160, "\"", 2217, 3);
                    WriteAttributeValue("", 2170, "location.href=\'", 2170, 15, true);
#nullable restore
#line 49 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\ForgotPassword\UsernameDetails.cshtml"
WriteAttributeValue("", 2185, Url.Action("Login", "Account"), 2185, 31, false);

#line default
#line hidden
#nullable disable
                    WriteAttributeValue("", 2216, "\'", 2216, 1, true);
                    EndWriteAttribute();
                    WriteLiteral(">Continue</a>\r\n                        </div>\r\n                    ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n                </div>\r\n\r\n            </div>\r\n");
#nullable restore
#line 55 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\ForgotPassword\UsernameDetails.cshtml"
            }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                </div>
            </div>
    <script src=""https://code.jquery.com/jquery-3.4.1.slim.min.js"" integrity=""sha384-J6qa4849blE2+poT4WnyKhv5vZF5SrPo0iEjwBvKU7imGFAV0wwj1yYfoRSJoZ+n"" crossorigin=""anonymous""></script>
    <script src=""https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js"" integrity=""sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo"" crossorigin=""anonymous""></script>
    <script src=""https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"" integrity=""sha384-wfSDF2E50Y2D1uUdj0O3uMBJnjuUD4Ih7YwaYd1iqfktj0Uod8GCExl3Og8ifwB6"" crossorigin=""anonymous""></script>
    <script");
                BeginWriteAttribute("src", " src=\"", 3010, "\"", 3016, 0);
                EndWriteAttribute();
                WriteLiteral(" async defer></script>\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</html>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<MyCBTS.IDP.Login.Models.ForgotUserNameModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
