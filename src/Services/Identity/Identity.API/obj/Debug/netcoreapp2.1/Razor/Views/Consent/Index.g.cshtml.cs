#pragma checksum "C:\Users\User\source\repos\WhiskyArchive\src\Services\Identity\Identity.API\Views\Consent\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e1c29cb3b6b62d50913dfa7ed3718c80191d881a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Consent_Index), @"mvc.1.0.view", @"/Views/Consent/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Consent/Index.cshtml", typeof(AspNetCore.Views_Consent_Index))]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e1c29cb3b6b62d50913dfa7ed3718c80191d881a", @"/Views/Consent/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"23ac09be4bcfaa7f9829a01d1a134874eaae1f3b", @"/Views/_ViewImports.cshtml")]
    public class Views_Consent_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<WhiskyArchive.Services.Identity.API.Models.AccountViewModels.ConsentViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", "hidden", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("consent-scopecheck"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("consent-form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(86, 106, true);
            WriteLiteral("<div class=\"container page-consent\">\r\n    <div class=\"row page-header\">\r\n        <div class=\"col-sm-10\">\r\n");
            EndContext();
#line 5 "C:\Users\User\source\repos\WhiskyArchive\src\Services\Identity\Identity.API\Views\Consent\Index.cshtml"
             if (Model.ClientLogoUrl != null)
            {

#line default
#line hidden
            BeginContext(254, 45, true);
            WriteLiteral("                <div class=\"client-logo\"><img");
            EndContext();
            BeginWriteAttribute("src", " src=\"", 299, "\"", 325, 1);
#line 7 "C:\Users\User\source\repos\WhiskyArchive\src\Services\Identity\Identity.API\Views\Consent\Index.cshtml"
WriteAttributeValue("", 305, Model.ClientLogoUrl, 305, 20, false);

#line default
#line hidden
            EndWriteAttribute();
            BeginContext(326, 9, true);
            WriteLiteral("></div>\r\n");
            EndContext();
#line 8 "C:\Users\User\source\repos\WhiskyArchive\src\Services\Identity\Identity.API\Views\Consent\Index.cshtml"
            }

#line default
#line hidden
            BeginContext(350, 34, true);
            WriteLiteral("            <h1>\r\n                ");
            EndContext();
            BeginContext(385, 16, false);
#line 10 "C:\Users\User\source\repos\WhiskyArchive\src\Services\Identity\Identity.API\Views\Consent\Index.cshtml"
           Write(Model.ClientName);

#line default
#line hidden
            EndContext();
            BeginContext(401, 178, true);
            WriteLiteral("\r\n                <small>is requesting your permission</small>\r\n            </h1>\r\n        </div>\r\n    </div>\r\n    <div class=\"row\">\r\n        <div class=\"col-sm-8\">\r\n            ");
            EndContext();
            BeginContext(580, 34, false);
#line 17 "C:\Users\User\source\repos\WhiskyArchive\src\Services\Identity\Identity.API\Views\Consent\Index.cshtml"
       Write(Html.Partial("_ValidationSummary"));

#line default
#line hidden
            EndContext();
            BeginContext(614, 14, true);
            WriteLiteral("\r\n            ");
            EndContext();
            BeginContext(628, 2605, false);
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "937ca50179c94866bbd3c3d4c8c57e93", async() => {
                BeginContext(674, 18, true);
                WriteLiteral("\r\n                ");
                EndContext();
                BeginContext(692, 43, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "7b84d75fa750429793ce5462d959c09c", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.InputTypeName = (string)__tagHelperAttribute_0.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
#line 19 "C:\Users\User\source\repos\WhiskyArchive\src\Services\Identity\Identity.API\Views\Consent\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.ReturnUrl);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(735, 82, true);
                WriteLiteral("\r\n                <div>Uncheck the permissions you do not wish to grant.</div>\r\n\r\n");
                EndContext();
#line 22 "C:\Users\User\source\repos\WhiskyArchive\src\Services\Identity\Identity.API\Views\Consent\Index.cshtml"
                 if (Model.IdentityScopes.Any())
                {

#line default
#line hidden
                BeginContext(886, 331, true);
                WriteLiteral(@"                    <div class=""panel panel-default consent-buttons"">
                        <div class=""panel-heading"">
                            <span class=""glyphicon glyphicon-user""></span>
                            Personal Information
                        </div>
                        <ul class=""list-group"">
");
                EndContext();
#line 30 "C:\Users\User\source\repos\WhiskyArchive\src\Services\Identity\Identity.API\Views\Consent\Index.cshtml"
                             foreach (var scope in Model.IdentityScopes)
                            {
                                

#line default
#line hidden
                BeginContext(1355, 37, false);
#line 32 "C:\Users\User\source\repos\WhiskyArchive\src\Services\Identity\Identity.API\Views\Consent\Index.cshtml"
                           Write(Html.Partial("_ScopeListItem", scope));

#line default
#line hidden
                EndContext();
#line 32 "C:\Users\User\source\repos\WhiskyArchive\src\Services\Identity\Identity.API\Views\Consent\Index.cshtml"
                                                                      
                            }

#line default
#line hidden
                BeginContext(1425, 59, true);
                WriteLiteral("                        </ul>\r\n                    </div>\r\n");
                EndContext();
#line 36 "C:\Users\User\source\repos\WhiskyArchive\src\Services\Identity\Identity.API\Views\Consent\Index.cshtml"
                }

#line default
#line hidden
                BeginContext(1503, 16, true);
                WriteLiteral("                ");
                EndContext();
#line 37 "C:\Users\User\source\repos\WhiskyArchive\src\Services\Identity\Identity.API\Views\Consent\Index.cshtml"
                 if (Model.ResourceScopes.Any())
                {

#line default
#line hidden
                BeginContext(1572, 314, true);
                WriteLiteral(@"                    <div class=""panel panel-default"">
                        <div class=""panel-heading"">
                            <span class=""glyphicon glyphicon-tasks""></span>
                            Application Access
                        </div>
                        <ul class=""list-group"">
");
                EndContext();
#line 45 "C:\Users\User\source\repos\WhiskyArchive\src\Services\Identity\Identity.API\Views\Consent\Index.cshtml"
                             foreach (var scope in Model.ResourceScopes)
                            {
                                

#line default
#line hidden
                BeginContext(2024, 37, false);
#line 47 "C:\Users\User\source\repos\WhiskyArchive\src\Services\Identity\Identity.API\Views\Consent\Index.cshtml"
                           Write(Html.Partial("_ScopeListItem", scope));

#line default
#line hidden
                EndContext();
#line 47 "C:\Users\User\source\repos\WhiskyArchive\src\Services\Identity\Identity.API\Views\Consent\Index.cshtml"
                                                                      
                            }

#line default
#line hidden
                BeginContext(2094, 59, true);
                WriteLiteral("                        </ul>\r\n                    </div>\r\n");
                EndContext();
#line 51 "C:\Users\User\source\repos\WhiskyArchive\src\Services\Identity\Identity.API\Views\Consent\Index.cshtml"
                }

#line default
#line hidden
                BeginContext(2172, 16, true);
                WriteLiteral("                ");
                EndContext();
#line 52 "C:\Users\User\source\repos\WhiskyArchive\src\Services\Identity\Identity.API\Views\Consent\Index.cshtml"
                 if (Model.AllowRememberConsent)
                {

#line default
#line hidden
                BeginContext(2241, 113, true);
                WriteLiteral("                    <div class=\"consent-remember\">\r\n                        <label>\r\n                            ");
                EndContext();
                BeginContext(2354, 62, false);
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("input", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "ab3ac756ecaf49f8af2c9b6340f3e1c7", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.InputTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
#line 56 "C:\Users\User\source\repos\WhiskyArchive\src\Services\Identity\Identity.API\Views\Consent\Index.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For = ModelExpressionProvider.CreateModelExpression(ViewData, __model => __model.RememberConsent);

#line default
#line hidden
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-for", __Microsoft_AspNetCore_Mvc_TagHelpers_InputTagHelper.For, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                EndContext();
                BeginContext(2416, 131, true);
                WriteLiteral("\r\n                            <strong>Remember My Decision</strong>\r\n                        </label>\r\n                    </div>\r\n");
                EndContext();
#line 60 "C:\Users\User\source\repos\WhiskyArchive\src\Services\Identity\Identity.API\Views\Consent\Index.cshtml"
                }

#line default
#line hidden
                BeginContext(2566, 248, true);
                WriteLiteral("                <div class=\"consent-buttons\">\r\n                    <button name=\"button\" value=\"yes\" class=\"btn btn-primary\" autofocus>Yes, Allow</button>\r\n                    <button name=\"button\" value=\"no\" class=\"btn\">No, Do Not Allow</button>\r\n");
                EndContext();
#line 64 "C:\Users\User\source\repos\WhiskyArchive\src\Services\Identity\Identity.API\Views\Consent\Index.cshtml"
                     if (Model.ClientUrl != null)
                    {

#line default
#line hidden
                BeginContext(2888, 77, true);
                WriteLiteral("                        <a class=\"pull-right btn btn-default\" target=\"_blank\"");
                EndContext();
                BeginWriteAttribute("href", " href=\"", 2965, "\"", 2988, 1);
#line 66 "C:\Users\User\source\repos\WhiskyArchive\src\Services\Identity\Identity.API\Views\Consent\Index.cshtml"
WriteAttributeValue("", 2972, Model.ClientUrl, 2972, 16, false);

#line default
#line hidden
                EndWriteAttribute();
                BeginContext(2989, 120, true);
                WriteLiteral(">\r\n                            <span class=\"glyphicon glyphicon-info-sign\"></span>\r\n                            <strong>");
                EndContext();
                BeginContext(3110, 16, false);
#line 68 "C:\Users\User\source\repos\WhiskyArchive\src\Services\Identity\Identity.API\Views\Consent\Index.cshtml"
                               Write(Model.ClientName);

#line default
#line hidden
                EndContext();
                BeginContext(3126, 41, true);
                WriteLiteral("</strong>\r\n                        </a>\r\n");
                EndContext();
#line 70 "C:\Users\User\source\repos\WhiskyArchive\src\Services\Identity\Identity.API\Views\Consent\Index.cshtml"
                    }

#line default
#line hidden
                BeginContext(3190, 36, true);
                WriteLiteral("                </div>\r\n            ");
                EndContext();
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            EndContext();
            BeginContext(3233, 40, true);
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<WhiskyArchive.Services.Identity.API.Models.AccountViewModels.ConsentViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
