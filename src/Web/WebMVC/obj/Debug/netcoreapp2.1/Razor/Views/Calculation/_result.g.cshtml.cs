#pragma checksum "C:\Users\User\source\repos\WhiskyArchive\src\Web\WebMVC\Views\Calculation\_result.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "2548c7354460a770c31b8634c50618fe1b09078c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Calculation__result), @"mvc.1.0.view", @"/Views/Calculation/_result.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Calculation/_result.cshtml", typeof(AspNetCore.Views_Calculation__result))]
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
#line 1 "C:\Users\User\source\repos\WhiskyArchive\src\Web\WebMVC\Views\_ViewImports.cshtml"
using WebMVC;

#line default
#line hidden
#line 2 "C:\Users\User\source\repos\WhiskyArchive\src\Web\WebMVC\Views\_ViewImports.cshtml"
using WebMVC.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2548c7354460a770c31b8634c50618fe1b09078c", @"/Views/Calculation/_result.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d07e873f05b36c9d83cd6a184d4bfbe1720fee4b", @"/Views/_ViewImports.cshtml")]
    public class Views_Calculation__result : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<WebMVC.ViewModels.CalculationResult>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            BeginContext(57, 1028, true);
            WriteLiteral(@"

<div class=""box-shadow table-responsive rounded mb-lg-5 mt-5"">

    <table class=""table"">
        <thead class="""" style=""border: 0"">
            <tr>
                <th>
                    得標價
                </th>
                <th>
                    拍賣會佣金
                </th>
                <th>
                    運費
                </th>
                <th>
                    運送保證費
                </th>
                <th>
                    刷卡附加費
                </th>
                <th>
                    增值稅(VAT)
                </th>
                <th>
                    關稅
                </th>
                <th>
                    菸酒稅
                </th>
                <th>
                    營業稅
                </th>
                <th>
                    代墊費
                </th>
                <th>
                    總成本
                </th>
                <th></th>
            </tr>
        </thead>
        <tbody class=""");
            WriteLiteral("\">\r\n");
            EndContext();
#line 46 "C:\Users\User\source\repos\WhiskyArchive\src\Web\WebMVC\Views\Calculation\_result.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
            BeginContext(1142, 72, true);
            WriteLiteral("                <tr>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1215, 43, false);
#line 50 "C:\Users\User\source\repos\WhiskyArchive\src\Web\WebMVC\Views\Calculation\_result.cshtml"
                   Write(Html.DisplayFor(modelItem => item.WinPrice));

#line default
#line hidden
            EndContext();
            BeginContext(1258, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1338, 45, false);
#line 53 "C:\Users\User\source\repos\WhiskyArchive\src\Web\WebMVC\Views\Calculation\_result.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Commission));

#line default
#line hidden
            EndContext();
            BeginContext(1383, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1463, 46, false);
#line 56 "C:\Users\User\source\repos\WhiskyArchive\src\Web\WebMVC\Views\Calculation\_result.cshtml"
                   Write(Html.DisplayFor(modelItem => item.DeliveryFee));

#line default
#line hidden
            EndContext();
            BeginContext(1509, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1589, 44, false);
#line 59 "C:\Users\User\source\repos\WhiskyArchive\src\Web\WebMVC\Views\Calculation\_result.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Insurance));

#line default
#line hidden
            EndContext();
            BeginContext(1633, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1713, 51, false);
#line 62 "C:\Users\User\source\repos\WhiskyArchive\src\Web\WebMVC\Views\Calculation\_result.cshtml"
                   Write(Html.DisplayFor(modelItem => item.PaymentSurcharge));

#line default
#line hidden
            EndContext();
            BeginContext(1764, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1844, 38, false);
#line 65 "C:\Users\User\source\repos\WhiskyArchive\src\Web\WebMVC\Views\Calculation\_result.cshtml"
                   Write(Html.DisplayFor(modelItem => item.VAT));

#line default
#line hidden
            EndContext();
            BeginContext(1882, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(1962, 42, false);
#line 68 "C:\Users\User\source\repos\WhiskyArchive\src\Web\WebMVC\Views\Calculation\_result.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Customs));

#line default
#line hidden
            EndContext();
            BeginContext(2004, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(2084, 52, false);
#line 71 "C:\Users\User\source\repos\WhiskyArchive\src\Web\WebMVC\Views\Calculation\_result.cshtml"
                   Write(Html.DisplayFor(modelItem => item.TobaccoAlcoholTax));

#line default
#line hidden
            EndContext();
            BeginContext(2136, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(2216, 46, false);
#line 74 "C:\Users\User\source\repos\WhiskyArchive\src\Web\WebMVC\Views\Calculation\_result.cshtml"
                   Write(Html.DisplayFor(modelItem => item.BusinessTax));

#line default
#line hidden
            EndContext();
            BeginContext(2262, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(2342, 49, false);
#line 77 "C:\Users\User\source\repos\WhiskyArchive\src\Web\WebMVC\Views\Calculation\_result.cshtml"
                   Write(Html.DisplayFor(modelItem => item.AdvancePayment));

#line default
#line hidden
            EndContext();
            BeginContext(2391, 79, true);
            WriteLiteral("\r\n                    </td>\r\n                    <td>\r\n                        ");
            EndContext();
            BeginContext(2471, 40, false);
#line 80 "C:\Users\User\source\repos\WhiskyArchive\src\Web\WebMVC\Views\Calculation\_result.cshtml"
                   Write(Html.DisplayFor(modelItem => item.Total));

#line default
#line hidden
            EndContext();
            BeginContext(2511, 52, true);
            WriteLiteral("\r\n                    </td>\r\n                </tr>\r\n");
            EndContext();
#line 83 "C:\Users\User\source\repos\WhiskyArchive\src\Web\WebMVC\Views\Calculation\_result.cshtml"
            }

#line default
#line hidden
            BeginContext(2578, 42, true);
            WriteLiteral("        </tbody>\r\n    </table>\r\n\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<WebMVC.ViewModels.CalculationResult>> Html { get; private set; }
    }
}
#pragma warning restore 1591
