#pragma checksum "D:\CRQs\2021\CSG\MyCBTS.IDP.Login\src\Views\Shared\Success.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0176a9b7400ea37fa1c09a5a28b5e3f856b189f1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared_Success), @"mvc.1.0.view", @"/Views/Shared/Success.cshtml")]
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
#line 1 "D:\CRQs\2021\CSG\MyCBTS.IDP.Login\src\Views\_ViewImports.cshtml"
using MyCBTS.IDP.Login;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\CRQs\2021\CSG\MyCBTS.IDP.Login\src\Views\_ViewImports.cshtml"
using MyCBTS.IDP.Login.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\CRQs\2021\CSG\MyCBTS.IDP.Login\src\Views\_ViewImports.cshtml"
using Microsoft.AspNetCore.Http;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0176a9b7400ea37fa1c09a5a28b5e3f856b189f1", @"/Views/Shared/Success.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"656906cfe79154ce9d1e91b643da83ccea2adb12", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared_Success : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<CustomResponse>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/assets/css/app.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form form_standard form_standard-sm mt-5 px-4 py-5 mx-auto text-center"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
#line 3 "D:\CRQs\2021\CSG\MyCBTS.IDP.Login\src\Views\Shared\Success.cshtml"
  
    Layout = "~/Views/Shared/_Layout.cshtml";
    var successMessage = Model.SuccessMessage;
    var errorList = Model.Errors;
    var brandName = commonUtility.GetBrandName();
    ViewData["Title"] = "Success - My " + brandName;
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\CRQs\2021\CSG\MyCBTS.IDP.Login\src\Views\Shared\Success.cshtml"
     if (!string.IsNullOrEmpty(successMessage))
    {
        ViewData["Title"] = "Success - My " + brandName;
    }

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "D:\CRQs\2021\CSG\MyCBTS.IDP.Login\src\Views\Shared\Success.cshtml"
     if (errorList.Count > 0)
    {
        ViewData["Title"] = "Failure - My " + brandName;
    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<!DOCTYPE html>
<!--[if lt IE 7]>      <html class=""no-js lt-ie9 lt-ie8 lt-ie7""> <![endif]-->
<!--[if IE 7]>         <html class=""no-js lt-ie9 lt-ie8""> <![endif]-->
<!--[if IE 8]>         <html class=""no-js lt-ie9""> <![endif]-->
<!--[if gt IE 8]><!-->
<html class=""no-js"">
<!--<![endif]-->
");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0176a9b7400ea37fa1c09a5a28b5e3f856b189f16147", async() => {
                WriteLiteral("\r\n    <meta charset=\"utf-8\">\r\n    <meta http-equiv=\"X-UA-Compatible\" content=\"IE=edge\">\r\n    <title>");
#nullable restore
#line 29 "D:\CRQs\2021\CSG\MyCBTS.IDP.Login\src\Views\Shared\Success.cshtml"
      Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("</title>\r\n    <meta name=\"description\"");
                BeginWriteAttribute("content", " content=\"", 1013, "\"", 1023, 0);
                EndWriteAttribute();
                WriteLiteral(">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1\">\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "0176a9b7400ea37fa1c09a5a28b5e3f856b189f17017", async() => {
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0176a9b7400ea37fa1c09a5a28b5e3f856b189f18900", async() => {
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
#line 42 "D:\CRQs\2021\CSG\MyCBTS.IDP.Login\src\Views\Shared\Success.cshtml"
                  await Html.RenderPartialAsync("_BrandLogo");

#line default
#line hidden
#nullable disable
                WriteLiteral("            </div>\r\n\r\n            <div class=\"col-12\">\r\n                <div class=\"cb-form\">\r\n                    <div class=\"spacer py-2 my-3\"></div>\r\n                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0176a9b7400ea37fa1c09a5a28b5e3f856b189f19960", async() => {
                    WriteLiteral("\r\n");
#nullable restore
#line 49 "D:\CRQs\2021\CSG\MyCBTS.IDP.Login\src\Views\Shared\Success.cshtml"
                         if (!string.IsNullOrEmpty(successMessage))
                        {

#line default
#line hidden
#nullable disable
                    WriteLiteral("                            <h2 class=\"py-3 mt-2\">Success!</h2>\r\n                            <p class=\"pb-4\">");
#nullable restore
#line 52 "D:\CRQs\2021\CSG\MyCBTS.IDP.Login\src\Views\Shared\Success.cshtml"
                                       Write(successMessage);

#line default
#line hidden
#nullable disable
                    WriteLiteral("</p>\r\n");
#nullable restore
#line 53 "D:\CRQs\2021\CSG\MyCBTS.IDP.Login\src\Views\Shared\Success.cshtml"
                        }

#line default
#line hidden
#nullable disable
#nullable restore
#line 54 "D:\CRQs\2021\CSG\MyCBTS.IDP.Login\src\Views\Shared\Success.cshtml"
                         if (errorList.Count > 0)
                        {

#line default
#line hidden
#nullable disable
                    WriteLiteral("                            <h2 class=\"py-3 mt-2\">Failure!</h2>\r\n");
#nullable restore
#line 57 "D:\CRQs\2021\CSG\MyCBTS.IDP.Login\src\Views\Shared\Success.cshtml"
                            foreach (var errorMessage in errorList)
                            {


#line default
#line hidden
#nullable disable
                    WriteLiteral("                                <p class=\"pb-4\">");
#nullable restore
#line 60 "D:\CRQs\2021\CSG\MyCBTS.IDP.Login\src\Views\Shared\Success.cshtml"
                                           Write(errorMessage);

#line default
#line hidden
#nullable disable
                    WriteLiteral("</p>\r\n");
#nullable restore
#line 61 "D:\CRQs\2021\CSG\MyCBTS.IDP.Login\src\Views\Shared\Success.cshtml"
                            }
                        }

#line default
#line hidden
#nullable disable
                    WriteLiteral("                        <div class=\"form-group text-center\">\r\n                            <a type=\"submit\" class=\"btn btn-secondary btn-lg cb-btn text-white cb-btn-text-primary\"");
                    BeginWriteAttribute("onclick", " onclick=\"", 2656, "\"", 2713, 3);
                    WriteAttributeValue("", 2666, "location.href=\'", 2666, 15, true);
#nullable restore
#line 64 "D:\CRQs\2021\CSG\MyCBTS.IDP.Login\src\Views\Shared\Success.cshtml"
WriteAttributeValue("", 2681, Url.Action("Login", "Account"), 2681, 31, false);

#line default
#line hidden
#nullable disable
                    WriteAttributeValue("", 2712, "\'", 2712, 1, true);
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
                WriteLiteral(@"
                </div>
            </div>
        </div>
    </div>
    <script src=""https://code.jquery.com/jquery-3.4.1.slim.min.js"" integrity=""sha384-J6qa4849blE2+poT4WnyKhv5vZF5SrPo0iEjwBvKU7imGFAV0wwj1yYfoRSJoZ+n"" crossorigin=""anonymous""></script>
    <script src=""https://cdn.jsdelivr.net/npm/popper.js@1.16.0/dist/umd/popper.min.js"" integrity=""sha384-Q6E9RHvbIyZFJoft+2mJbHaEWldlvI9IOYy5n3zV9zzTtmI3UksdQRVvoxMfooAo"" crossorigin=""anonymous""></script>
    <script src=""https://stackpath.bootstrapcdn.com/bootstrap/4.4.1/js/bootstrap.min.js"" integrity=""sha384-wfSDF2E50Y2D1uUdj0O3uMBJnjuUD4Ih7YwaYd1iqfktj0Uod8GCExl3Og8ifwB6"" crossorigin=""anonymous""></script>
    <script src=""https://kit.fontawesome.com/245c020873.js"" crossorigin=""anonymous""></script>
    <script");
                BeginWriteAttribute("src", " src=\"", 3568, "\"", 3574, 0);
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
            WriteLiteral("\r\n</html>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public MyCBTS.IDP.Login.Utility.ICommonUtility commonUtility { get; private set; }
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<CustomResponse> Html { get; private set; }
    }
}
#pragma warning restore 1591
