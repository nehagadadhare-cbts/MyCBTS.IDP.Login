#pragma checksum "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Registration\Register.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0c28f8f47a32a86105df981a5699d2d2fda10fcf"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Registration_Register), @"mvc.1.0.view", @"/Views/Registration/Register.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0c28f8f47a32a86105df981a5699d2d2fda10fcf", @"/Views/Registration/Register.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"656906cfe79154ce9d1e91b643da83ccea2adb12", @"/Views/_ViewImports.cshtml")]
    public class Views_Registration_Register : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<MyCBTS.IDP.Login.Models.RegisterViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Registration\Register.cshtml"
  
    Layout = null;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 6 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Registration\Register.cshtml"
  
    var RegistrationStep1 = MyCBTS.IDP.Login.Models.RegisterViewModel.RegisterFlow.RegistrationStep1;
    var RegistrationContinue = MyCBTS.IDP.Login.Models.RegisterViewModel.RegisterFlow.RegistrationContinue;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Registration\Register.cshtml"
 if (Model.RegisterStep == RegistrationStep1)
{
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Registration\Register.cshtml"
Write(await Html.PartialAsync("Registration/_RegistrationStep1", Model));

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Registration\Register.cshtml"
                                                                      ;
}
else if (Model.RegisterStep == RegistrationContinue)
{
    

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Registration\Register.cshtml"
Write(await Html.PartialAsync("Registration/_RegistrationContinue", Model));

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Registration\Register.cshtml"
                                                                         ;
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
#nullable restore
#line 19 "D:\CBT\OnX-CBTS\WorkingCode\MyCBTS.IDP.Login_WorkingCode\MyCBTS.IDP.Login\src\Views\Registration\Register.cshtml"
  await Html.RenderPartialAsync("_ValidationScriptsPartial");

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<script type=""text/javascript"">
    $(document).ready(function () {
        $('#phone').keydown(function (e) {
            var key = e.charCode || e.keyCode || 0;
            $text = $(this);
            if (key !== 8 && key !== 9) {
                if ($text.val().length === 3) {
                    $text.val($text.val() + '-');
                }
                if ($text.val().length === 7) {
                    $text.val($text.val() + '-');
                }

            }
            return (key == 8 || key == 9 || key == 46 || (key >= 48 && key <= 57) || (key >= 96 && key <= 105));
        });
    });

</script>
}
");
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
