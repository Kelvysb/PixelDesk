using Microsoft.AspNetCore.Components;

namespace PixelDesk.Client.Shared
{
    public class BottomBoxBase : ComponentBase
    {
        [Parameter]
        public string MessageLine1 { get; set; } = "";

        [Parameter]
        public string MessageLine2 { get; set; } = "";
    }
}