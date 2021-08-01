﻿using Microsoft.AspNetCore.Components;

namespace PixelDesk.Client.Shared
{
    public class TopRowBase : ComponentBase
    {
        [Parameter]
        public string Clock { get; set; }

        [Parameter]
        public string WeatherState { get; set; }

        [Parameter]
        public string IntercomState { get; set; }
    }
}