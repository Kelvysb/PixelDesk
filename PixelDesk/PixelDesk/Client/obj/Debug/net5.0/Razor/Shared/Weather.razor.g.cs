#pragma checksum "D:\Repositories\PixelDesk\PixelDesk\PixelDesk\Client\Shared\Weather.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8d04b5e34896715e1dd998645942608f70ed514b"
// <auto-generated/>
#pragma warning disable 1591
namespace PixelDesk.Client.Shared
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "D:\Repositories\PixelDesk\PixelDesk\PixelDesk\Client\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\Repositories\PixelDesk\PixelDesk\PixelDesk\Client\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "D:\Repositories\PixelDesk\PixelDesk\PixelDesk\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "D:\Repositories\PixelDesk\PixelDesk\PixelDesk\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "D:\Repositories\PixelDesk\PixelDesk\PixelDesk\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "D:\Repositories\PixelDesk\PixelDesk\PixelDesk\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "D:\Repositories\PixelDesk\PixelDesk\PixelDesk\Client\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "D:\Repositories\PixelDesk\PixelDesk\PixelDesk\Client\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "D:\Repositories\PixelDesk\PixelDesk\PixelDesk\Client\_Imports.razor"
using PixelDesk.Client;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "D:\Repositories\PixelDesk\PixelDesk\PixelDesk\Client\_Imports.razor"
using PixelDesk.Client.Shared;

#line default
#line hidden
#nullable disable
    public partial class Weather : WeatherBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "frame-box weather-box");
            __builder.AddAttribute(2, "b-nnpecotffn");
            __builder.AddMarkupContent(3, "<div class=\"top-bottom-frame\" b-nnpecotffn><img src=\"/images/Box_TL.png\" class=\"corner\" b-nnpecotffn>\r\n        <img src=\"/images/Box_T.png\" class=\"stretch-line\" b-nnpecotffn>\r\n        <img src=\"/images/Box_TR.png\" class=\"corner\" b-nnpecotffn></div>\r\n    ");
            __builder.OpenElement(4, "div");
            __builder.AddAttribute(5, "class", "middle-frame");
            __builder.AddAttribute(6, "b-nnpecotffn");
            __builder.AddMarkupContent(7, "<img src=\"/images/Box_S.png\" class=\"stretch-column\" b-nnpecotffn>\r\n        ");
            __builder.OpenElement(8, "div");
            __builder.AddAttribute(9, "class", "frame-content box-content");
            __builder.AddAttribute(10, "b-nnpecotffn");
#nullable restore
#line 12 "D:\Repositories\PixelDesk\PixelDesk\PixelDesk\Client\Shared\Weather.razor"
             if (OpenWeather != null)
            {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(11, "div");
            __builder.AddAttribute(12, "class", "infos");
            __builder.AddAttribute(13, "b-nnpecotffn");
            __builder.OpenElement(14, "label");
            __builder.AddAttribute(15, "b-nnpecotffn");
            __builder.AddContent(16, "Temperature: ");
            __builder.AddContent(17, 
#nullable restore
#line 15 "D:\Repositories\PixelDesk\PixelDesk\PixelDesk\Client\Shared\Weather.razor"
                                     OpenWeather.Main.Temp

#line default
#line hidden
#nullable disable
            );
            __builder.AddContent(18, "°");
            __builder.CloseElement();
            __builder.AddMarkupContent(19, "\r\n                ");
            __builder.OpenElement(20, "label");
            __builder.AddAttribute(21, "b-nnpecotffn");
            __builder.AddContent(22, "Feels Like: ");
            __builder.AddContent(23, 
#nullable restore
#line 16 "D:\Repositories\PixelDesk\PixelDesk\PixelDesk\Client\Shared\Weather.razor"
                                    OpenWeather.Main.FeelsLike

#line default
#line hidden
#nullable disable
            );
            __builder.AddContent(24, "°");
            __builder.CloseElement();
            __builder.AddMarkupContent(25, "\r\n                ");
            __builder.OpenElement(26, "label");
            __builder.AddAttribute(27, "b-nnpecotffn");
            __builder.AddContent(28, "Humidity: ");
            __builder.AddContent(29, 
#nullable restore
#line 17 "D:\Repositories\PixelDesk\PixelDesk\PixelDesk\Client\Shared\Weather.razor"
                                  OpenWeather.Main.Humidity

#line default
#line hidden
#nullable disable
            );
            __builder.AddContent(30, "%");
            __builder.CloseElement();
            __builder.AddMarkupContent(31, "\r\n                ");
            __builder.OpenElement(32, "label");
            __builder.AddAttribute(33, "b-nnpecotffn");
            __builder.AddContent(34, "Weather: ");
            __builder.AddContent(35, 
#nullable restore
#line 18 "D:\Repositories\PixelDesk\PixelDesk\PixelDesk\Client\Shared\Weather.razor"
                                 OpenWeather.Weather.First().Description

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(36, "\r\n                ");
            __builder.OpenElement(37, "label");
            __builder.AddAttribute(38, "b-nnpecotffn");
            __builder.AddContent(39, "Wind: ");
            __builder.AddContent(40, 
#nullable restore
#line 19 "D:\Repositories\PixelDesk\PixelDesk\PixelDesk\Client\Shared\Weather.razor"
                              OpenWeather.Wind.Speed

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.AddMarkupContent(41, "\r\n                ");
            __builder.OpenElement(42, "div");
            __builder.AddAttribute(43, "class", "weather-icon");
            __builder.AddAttribute(44, "b-nnpecotffn");
            __builder.OpenElement(45, "img");
            __builder.AddAttribute(46, "src", 
#nullable restore
#line 22 "D:\Repositories\PixelDesk\PixelDesk\PixelDesk\Client\Shared\Weather.razor"
                               Icon

#line default
#line hidden
#nullable disable
            );
            __builder.AddAttribute(47, "class", "icon");
            __builder.AddAttribute(48, "b-nnpecotffn");
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 24 "D:\Repositories\PixelDesk\PixelDesk\PixelDesk\Client\Shared\Weather.razor"
            }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.AddMarkupContent(49, "\r\n        <img src=\"/images/Box_S.png\" class=\"stretch-column\" b-nnpecotffn>");
            __builder.CloseElement();
            __builder.AddMarkupContent(50, "\r\n    ");
            __builder.AddMarkupContent(51, "<div class=\"top-bottom-frame\" b-nnpecotffn><img src=\"/images/Box_BL.png\" class=\"corner\" b-nnpecotffn>\r\n        <img src=\"/images/Box_B.png\" class=\"stretch-line\" b-nnpecotffn>\r\n        <img src=\"/images/Box_BR.png\" class=\"corner\" b-nnpecotffn></div>");
            __builder.CloseElement();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
