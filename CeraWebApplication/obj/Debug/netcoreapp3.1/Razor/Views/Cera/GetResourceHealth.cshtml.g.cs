#pragma checksum "C:\Users\v-vetam\source\repos\Cloud Enablement And Risk Assessment (CERA)2\CeraWebApplication\Views\Cera\GetResourceHealth.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ec1538d2097675c4b3f56ff175fb708531329fd0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Cera_GetResourceHealth), @"mvc.1.0.view", @"/Views/Cera/GetResourceHealth.cshtml")]
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
#line 1 "C:\Users\v-vetam\source\repos\Cloud Enablement And Risk Assessment (CERA)2\CeraWebApplication\Views\_ViewImports.cshtml"
using CeraWebApplication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\v-vetam\source\repos\Cloud Enablement And Risk Assessment (CERA)2\CeraWebApplication\Views\_ViewImports.cshtml"
using CeraWebApplication.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ec1538d2097675c4b3f56ff175fb708531329fd0", @"/Views/Cera/GetResourceHealth.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5c31bb6c3a36d36b880a7a5547376c5fbff6bbb8", @"/Views/_ViewImports.cshtml")]
    public class Views_Cera_GetResourceHealth : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<CeraWebApplication.Models.CeraResourceHealth>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<h1>Resource Health</h1>\r\n");
#nullable restore
#line 3 "C:\Users\v-vetam\source\repos\Cloud Enablement And Risk Assessment (CERA)2\CeraWebApplication\Views\Cera\GetResourceHealth.cshtml"
 if (Model != null)
{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"    <table class=""table table-striped table-hover"" style=""margin-top:1%"">
        <thead>
            <tr>

                <th>
                    Resource Name
                </th>

                <th>
                    Resource Group
                </th>

                <th>
                    Resource Type
                </th>

                <th>
                    Location
                </th>
                <th>
                    Availability State
                </th>
            </tr>
        </thead>
        <tbody>
");
#nullable restore
#line 30 "C:\Users\v-vetam\source\repos\Cloud Enablement And Risk Assessment (CERA)2\CeraWebApplication\Views\Cera\GetResourceHealth.cshtml"
             foreach (var item in Model)
            {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n                <td>\r\n                    ");
#nullable restore
#line 34 "C:\Users\v-vetam\source\repos\Cloud Enablement And Risk Assessment (CERA)2\CeraWebApplication\Views\Cera\GetResourceHealth.cshtml"
               Write(Html.DisplayFor(modelItem => item.ResourceName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 37 "C:\Users\v-vetam\source\repos\Cloud Enablement And Risk Assessment (CERA)2\CeraWebApplication\Views\Cera\GetResourceHealth.cshtml"
               Write(Html.DisplayFor(modelItem => item.ResourceGroupName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 40 "C:\Users\v-vetam\source\repos\Cloud Enablement And Risk Assessment (CERA)2\CeraWebApplication\Views\Cera\GetResourceHealth.cshtml"
               Write(Html.DisplayFor(modelItem => item.ResourceType));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 43 "C:\Users\v-vetam\source\repos\Cloud Enablement And Risk Assessment (CERA)2\CeraWebApplication\Views\Cera\GetResourceHealth.cshtml"
               Write(Html.DisplayFor(modelItem => item.Location));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n");
#nullable restore
#line 46 "C:\Users\v-vetam\source\repos\Cloud Enablement And Risk Assessment (CERA)2\CeraWebApplication\Views\Cera\GetResourceHealth.cshtml"
                       if (item.AvailabilityState == "Available")
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <i style=\"color:green\">");
#nullable restore
#line 48 "C:\Users\v-vetam\source\repos\Cloud Enablement And Risk Assessment (CERA)2\CeraWebApplication\Views\Cera\GetResourceHealth.cshtml"
                                              Write(Html.DisplayFor(modelItem => item.AvailabilityState));

#line default
#line hidden
#nullable disable
            WriteLiteral("</i>\r\n");
#nullable restore
#line 49 "C:\Users\v-vetam\source\repos\Cloud Enablement And Risk Assessment (CERA)2\CeraWebApplication\Views\Cera\GetResourceHealth.cshtml"
                        }
                        else if (item.AvailabilityState == "Unavailable")
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <i style=\"color:red\">");
#nullable restore
#line 52 "C:\Users\v-vetam\source\repos\Cloud Enablement And Risk Assessment (CERA)2\CeraWebApplication\Views\Cera\GetResourceHealth.cshtml"
                                            Write(Html.DisplayFor(modelItem => item.AvailabilityState));

#line default
#line hidden
#nullable disable
            WriteLiteral("</i>\r\n");
#nullable restore
#line 53 "C:\Users\v-vetam\source\repos\Cloud Enablement And Risk Assessment (CERA)2\CeraWebApplication\Views\Cera\GetResourceHealth.cshtml"
                        }
                        else
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            <i style=\"color:dimgray\">");
#nullable restore
#line 56 "C:\Users\v-vetam\source\repos\Cloud Enablement And Risk Assessment (CERA)2\CeraWebApplication\Views\Cera\GetResourceHealth.cshtml"
                                                Write(Html.DisplayFor(modelItem => item.AvailabilityState));

#line default
#line hidden
#nullable disable
            WriteLiteral("</i>\r\n");
#nullable restore
#line 57 "C:\Users\v-vetam\source\repos\Cloud Enablement And Risk Assessment (CERA)2\CeraWebApplication\Views\Cera\GetResourceHealth.cshtml"
                        }
                    

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 62 "C:\Users\v-vetam\source\repos\Cloud Enablement And Risk Assessment (CERA)2\CeraWebApplication\Views\Cera\GetResourceHealth.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral("        </tbody>\r\n    </table>");
#nullable restore
#line 64 "C:\Users\v-vetam\source\repos\Cloud Enablement And Risk Assessment (CERA)2\CeraWebApplication\Views\Cera\GetResourceHealth.cshtml"
            }

#line default
#line hidden
#nullable disable
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<CeraWebApplication.Models.CeraResourceHealth>> Html { get; private set; }
    }
}
#pragma warning restore 1591
