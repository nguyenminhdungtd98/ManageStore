#pragma checksum "D:\StudyDoc\IT\Học kì 2\Kĩ thuật lập trình\21880024\21880024\Pages\BillOut.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "be838f3849421fc11b7531dc97e0dc96668a7b09"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(_21880024.Pages.Pages_BillOut), @"mvc.1.0.razor-page", @"/Pages/BillOut.cshtml")]
namespace _21880024.Pages
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
#line 1 "D:\StudyDoc\IT\Học kì 2\Kĩ thuật lập trình\21880024\21880024\Pages\_ViewImports.cshtml"
using _21880024;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"be838f3849421fc11b7531dc97e0dc96668a7b09", @"/Pages/BillOut.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fcf4f4270925b55898ba933958babb8ceb66afe4", @"/Pages/_ViewImports.cshtml")]
    #nullable restore
    public class Pages_BillOut : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("was-validated"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<div class=\"row\">\r\n    <div class=\"col col-md-3\">\r\n\r\n        ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "be838f3849421fc11b7531dc97e0dc96668a7b093927", async() => {
                WriteLiteral("\r\n            <div class=\"form-group\">\r\n                <label for=\"productNumber\">Mã hóa đơn nhập hàng:</label>\r\n                <input type=\"number\" class=\"form-control\" id=\"numberBill\" placeholder=\"Mã sản phẩm(Chỉ được nhập số)\" name=\"numberBill\"");
                BeginWriteAttribute("value", " value=\"", 405, "\"", 433, 1);
#nullable restore
#line 12 "D:\StudyDoc\IT\Học kì 2\Kĩ thuật lập trình\21880024\21880024\Pages\BillOut.cshtml"
WriteAttributeValue("", 413, Model.numberBillOut, 413, 20, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" readonly>\r\n            </div>\r\n\r\n            <div class=\"form-group\">\r\n                <label for=\"productName\">Tên sản phẩm:</label>\r\n                <select name=\"productName\" id=\"productName\" required>\r\n");
#nullable restore
#line 18 "D:\StudyDoc\IT\Học kì 2\Kĩ thuật lập trình\21880024\21880024\Pages\BillOut.cshtml"
                      
                        if (Model.productsInStock.Count > 0)
                        {
                            for (int i = 0; i < Model.productsInStock.Count; i++)
                            {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("option", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "be838f3849421fc11b7531dc97e0dc96668a7b095517", async() => {
#nullable restore
#line 23 "D:\StudyDoc\IT\Học kì 2\Kĩ thuật lập trình\21880024\21880024\Pages\BillOut.cshtml"
                                                                                 Write(Model.productsInStock[i].productName);

#line default
#line hidden
#nullable disable
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.OptionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper);
                BeginWriteTagHelperAttribute();
#nullable restore
#line 23 "D:\StudyDoc\IT\Học kì 2\Kĩ thuật lập trình\21880024\21880024\Pages\BillOut.cshtml"
                                   WriteLiteral(Model.productsInStock[i].productName);

#line default
#line hidden
#nullable disable
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value = __tagHelperStringValueBuffer;
                __tagHelperExecutionContext.AddTagHelperAttribute("value", __Microsoft_AspNetCore_Mvc_TagHelpers_OptionTagHelper.Value, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
#nullable restore
#line 24 "D:\StudyDoc\IT\Học kì 2\Kĩ thuật lập trình\21880024\21880024\Pages\BillOut.cshtml"
                            }
                        }
                    

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                </select>
            </div>
            <div class=""form-group"">
                <label for=""number"">Số lượng:</label>
                <input type=""number"" class=""form-control"" id=""number"" placeholder=""Số lượng(Chỉ được nhập số)"" name=""number""");
                BeginWriteAttribute("value", " value=\"", 1349, "\"", 1370, 1);
#nullable restore
#line 32 "D:\StudyDoc\IT\Học kì 2\Kĩ thuật lập trình\21880024\21880024\Pages\BillOut.cshtml"
WriteAttributeValue("", 1357, Model.number, 1357, 13, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\r\n            </div>\r\n\r\n            <div class=\"form-group\">\r\n                <label for=\"createDate\">Ngày tạo hơn đơn:</label>\r\n                <input type=\"datetime\" class=\"form-control\" id=\"createDate\" name=\"createDate\"");
                BeginWriteAttribute("value", " value=\"", 1594, "\"", 1619, 1);
#nullable restore
#line 37 "D:\StudyDoc\IT\Học kì 2\Kĩ thuật lập trình\21880024\21880024\Pages\BillOut.cshtml"
WriteAttributeValue("", 1602, Model.createDate, 1602, 17, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" readonly>\r\n            </div>\r\n            <button type=\"submit\" class=\"btn btn-primary\">Thêm hoặc chỉnh sửa sản phẩm vào hóa đơn</button>\r\n\r\n        ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n    </div>\r\n    <div class=\"col col-md-9\">\r\n        <div style=\"height:800px; overflow-y: scroll ;\">\r\n            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "be838f3849421fc11b7531dc97e0dc96668a7b0910596", async() => {
                WriteLiteral(@"
                <table id=""dtVerticalScrollExample"" class=""table table-striped table-bordered table-sm"" cellspacing=""0"" width=""100%"">
                    <thead class=""thead-dark"">
                        <tr>
                            <th class=""th-sm"">Mã hóa đơn</th>
                            <th class=""th-sm"">Ngày tạo hóa đơn</th>
                        </tr>

                    </thead>
                    <tr>
                        <td>");
#nullable restore
#line 55 "D:\StudyDoc\IT\Học kì 2\Kĩ thuật lập trình\21880024\21880024\Pages\BillOut.cshtml"
                       Write(Model.numberBillOut);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 56 "D:\StudyDoc\IT\Học kì 2\Kĩ thuật lập trình\21880024\21880024\Pages\BillOut.cshtml"
                       Write(Model.createDate);

#line default
#line hidden
#nullable disable
                WriteLiteral(@"</td>
                    </tr>


                </table>
                <table id=""dtVerticalScrollExample"" class=""table table-striped table-bordered table-sm"" cellspacing=""0"" width=""100%"">
                    <thead class=""thead-dark"">
                        <tr>

                            <th class=""th-sm"">Mã Sản Phẩm</th>
                            <th class=""th-sm"">Tên Sản Phẩm</th>
                            <th class=""th-sm"">Số lượng</th>
                            <th class=""th-sm"">Đơn giá</th>
                            <th class=""th-sm"">Tổng cộng</th>
                            <th class=""th-sm""></th>
                        </tr>
                    </thead>
");
#nullable restore
#line 73 "D:\StudyDoc\IT\Học kì 2\Kĩ thuật lập trình\21880024\21880024\Pages\BillOut.cshtml"
                       if (Model.productInBills != null)
                        {
                            for (int i = 0; i < Model.productInBills.Count; i++)
                            {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                <tr>\r\n                                    <td>");
#nullable restore
#line 78 "D:\StudyDoc\IT\Học kì 2\Kĩ thuật lập trình\21880024\21880024\Pages\BillOut.cshtml"
                                   Write(Model.productInBills[i].productNumber);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                                    <td>");
#nullable restore
#line 79 "D:\StudyDoc\IT\Học kì 2\Kĩ thuật lập trình\21880024\21880024\Pages\BillOut.cshtml"
                                   Write(Model.productInBills[i].productName);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                                    <td>");
#nullable restore
#line 80 "D:\StudyDoc\IT\Học kì 2\Kĩ thuật lập trình\21880024\21880024\Pages\BillOut.cshtml"
                                   Write(Model.productInBills[i].number);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                                    <td>");
#nullable restore
#line 81 "D:\StudyDoc\IT\Học kì 2\Kĩ thuật lập trình\21880024\21880024\Pages\BillOut.cshtml"
                                   Write(Model.productInBills[i].price);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                                    <td>");
#nullable restore
#line 82 "D:\StudyDoc\IT\Học kì 2\Kĩ thuật lập trình\21880024\21880024\Pages\BillOut.cshtml"
                                   Write(Model.productInBills[i].total);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                                    <td>\r\n                                        <a");
                BeginWriteAttribute("href", " href=\"", 3897, "\"", 3961, 2);
                WriteAttributeValue("", 3904, "/BillOut?idDeleteP=", 3904, 19, true);
#nullable restore
#line 84 "D:\StudyDoc\IT\Học kì 2\Kĩ thuật lập trình\21880024\21880024\Pages\BillOut.cshtml"
WriteAttributeValue("", 3923, Model.productInBills[i].productNumber, 3923, 38, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">Xóa</a>\r\n                                    </td>\r\n                                </tr>\r\n");
#nullable restore
#line 87 "D:\StudyDoc\IT\Học kì 2\Kĩ thuật lập trình\21880024\21880024\Pages\BillOut.cshtml"
                            }
                        }

                    

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                </table>
                <label>Đồng ý hoàn tất hóa đơn</label>
                <input type=""checkbox"" value=""save"" id=""save"" name=""save"" required />
                <button type=""submit"" class=""btn btn-primary"">Hoàn tất hóa đơn</button>
            ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<_21880024.Pages.BillOutModel> Html { get; private set; } = default!;
        #nullable disable
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<_21880024.Pages.BillOutModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<_21880024.Pages.BillOutModel>)PageContext?.ViewData;
        public _21880024.Pages.BillOutModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
