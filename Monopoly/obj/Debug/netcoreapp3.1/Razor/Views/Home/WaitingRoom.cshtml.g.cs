#pragma checksum "D:\LICENTA\Monopoly\Monopoly\Views\Home\WaitingRoom.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ef78d80838822d762792b71d8bbfe81e8f0417f1"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_WaitingRoom), @"mvc.1.0.view", @"/Views/Home/WaitingRoom.cshtml")]
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
#line 1 "D:\LICENTA\Monopoly\Monopoly\Views\_ViewImports.cshtml"
using Monopoly;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\LICENTA\Monopoly\Monopoly\Views\_ViewImports.cshtml"
using Monopoly.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\LICENTA\Monopoly\Monopoly\Views\Home\WaitingRoom.cshtml"
using Microsoft.AspNetCore.Identity;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\LICENTA\Monopoly\Monopoly\Views\Home\WaitingRoom.cshtml"
using Monopoly.Areas.Identity.Data;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ef78d80838822d762792b71d8bbfe81e8f0417f1", @"/Views/Home/WaitingRoom.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d86fd3ed6c31c356d20b466714e6a83b2a2b8a87", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_WaitingRoom : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/waiting-room.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/signalr/dist/browser/signalr.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/js/chat-waiting-room.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 1 "D:\LICENTA\Monopoly\Monopoly\Views\Home\WaitingRoom.cshtml"
  
    ViewData["Title"] = "Waiting";

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagOnly, "ef78d80838822d762792b71d8bbfe81e8f0417f15122", async() => {
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
            WriteLiteral("\r\n");
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ef78d80838822d762792b71d8bbfe81e8f0417f16307", async() => {
                WriteLiteral("\r\n    <div class=\"centered-div\">\r\n        <h2 class=\"text-center waiting-room-title\">Waiting room</h2>\r\n        <hr style=\"border-color:#F5CD6C;background-color:#F5CD6C\">\r\n        <button type=\"button\" class=\"btn see-players-button\"");
                BeginWriteAttribute("onclick", " onclick=\"", 513, "\"", 642, 9);
                WriteAttributeValue("", 523, "seePlayers(`", 523, 12, true);
#nullable restore
#line 15 "D:\LICENTA\Monopoly\Monopoly\Views\Home\WaitingRoom.cshtml"
WriteAttributeValue("", 535, UserManager.GetUserAsync(User).Result.FirstName, 535, 48, false);

#line default
#line hidden
#nullable disable
                WriteAttributeValue("", 583, "`+", 583, 2, true);
                WriteAttributeValue(" ", 585, "`", 586, 2, true);
                WriteAttributeValue(" ", 587, "`", 588, 2, true);
                WriteAttributeValue(" ", 589, "+", 590, 2, true);
                WriteAttributeValue(" ", 591, "`", 592, 2, true);
#nullable restore
#line 15 "D:\LICENTA\Monopoly\Monopoly\Views\Home\WaitingRoom.cshtml"
WriteAttributeValue("", 593, UserManager.GetUserAsync(User).Result.LastName, 593, 47, false);

#line default
#line hidden
#nullable disable
                WriteAttributeValue("", 640, "`)", 640, 2, true);
                EndWriteAttribute();
                WriteLiteral(@">See current players from this room</button>
        <button onclick=""startGame()"" class=""start-button"" hidden=""true"" id=""startGame"">Start<i class='fas fa-angle-double-right'></i></button>
        <div class=""chat"">            
            <div class=""send-message"">
                <div class=""send-message-title"">Message:</div>
                <input type=""text"" id=""txtmessage"" class=""input-message"" onkeyup=""checkRequiredFields()""/>
                <button type=""button"" id=""sendToGroupBtn"" class=""btn send-message-button"" disabled=""true"">Send</button>
            </div>
            
            <div class=""message-list"" id=""messagesList"">
                <ul id=""ulGroupMessages"" style=""color:#23442F;font-weight:bold;font-size:medium""></ul>
            </div>
        </div>
    </div>
");
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
            WriteLiteral("\r\n\r\n");
            WriteLiteral(@"<div class=""modal fade"" tabindex=""-1"" role=""dialog"" aria-hidden=""true"" id=""modalCurrentPlayers"">
    <div class=""modal-dialog modal-md"">
        <div class=""modal-content"" style=""background-color: #52755F;"">
            <div class=""modal-header text-center"" style=""justify-content: center"">
                <h2 class=""modal-title"" style=""color: #F5CD6C;"">Current players in this room</h2>
            </div>

            <div style=""margin:10px"" class=""text-center players-list"" id=""currentPlayersList"">
            </div>

            <div class=""modal-footer"" style=""justify-content:center"">
                <button type=""button"" class=""btn close-button"" onclick=""closeCurrentPlayersPopup()"">Close</button>
            </div>
        </div>
    </div>
</div>

<script>
    var intervalId = window.setInterval(function(){
        $.ajax({
            url: 'GetPlayersCount',
            type: 'GET',
            contentType: 'application/json',
            data: {
                player: `");
#nullable restore
#line 56 "D:\LICENTA\Monopoly\Monopoly\Views\Home\WaitingRoom.cshtml"
                    Write(UserManager.GetUserAsync(User).Result.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral("`+ ` ` + `");
#nullable restore
#line 56 "D:\LICENTA\Monopoly\Monopoly\Views\Home\WaitingRoom.cshtml"
                                                                              Write(UserManager.GetUserAsync(User).Result.LastName);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"`
            },
            success: function(data) {
                console.log(data)
                if(data > 1){
                    $('#startGame').prop('hidden', false);
                }else{
                    $('#startGame').prop('hidden', true);
                }
                
            }
        }
        );
    }, 1000);
    function startGame()
    {
        window.location.href = 'Game';
    }
    
    function seePlayers(player)
    {
        $('#modalCurrentPlayers').modal('show'); 
        $.ajax({
            url: 'GetPlayersFromRoom',
            type: 'GET',
            contentType: 'application/json',
            data: {
                player: player
            },
            success: function(data) {
                $(""#currentPlayersList"").empty();
                $.each(data, function(){
                    var text = '<p>'+this+'</p>';
                    $(text).appendTo('#currentPlayersList');
                });
                
       ");
            WriteLiteral(@"     }
        }
        );
    }

    function closeCurrentPlayersPopup()
    {
        $('#modalCurrentPlayers').modal('hide'); 
    }

    function checkRequiredFields()
    {
        if($('#txtmessage').val() != """"){
            $('#sendToGroupBtn').prop('disabled', false);
        }else {
            $('#sendToGroupBtn').prop('disabled', true);
        }
    }
</script>
");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ef78d80838822d762792b71d8bbfe81e8f0417f112585", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "ef78d80838822d762792b71d8bbfe81e8f0417f113625", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public UserManager<MonopolyUser> UserManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public SignInManager<MonopolyUser> SignInManager { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
