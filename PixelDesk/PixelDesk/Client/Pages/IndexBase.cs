using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using PixelDesk.Shared.Abstractions.Services;
using PixelDesk.Shared.Models;

namespace PixelDesk.Client.Pages
{
    public class IndexBase : ComponentBase, IDisposable
    {
        protected ElementReference mainDiv;
        protected bool alert = false;
        protected string clock = DateTime.Now.ToString("HH:mm - dd/MM/yyyy");
        protected string bottomBoxMessageLine1 = "";
        protected string bottomBoxMessageLine2 = "";
        protected string intercomState = "Offline";
        protected string weatherState = "Offline";
        private static int INTERCOM_INTERVAL = 2000;
        private static int WEATHER_INTERVAL = 30000;
        private Timer weatherTimer;
        private Timer intercomTimer;

        [Inject]
        public IWeatherService WeatherService { get; set; }

        [Inject]
        public IIntercomService IntercomService { get; set; }

        public IntercomModule IntercomData { get; set; }

        public OpenWeather WeatherData { get; set; }

        [Inject]
        private IJSRuntime JSRuntime { get; set; }

        public void KeyDown(KeyboardEventArgs e)
        {
        }

        public void Dispose()
        {
            weatherTimer.Dispose();
            weatherTimer = null;

            intercomTimer.Dispose();
            intercomTimer = null;
        }

        protected override Task OnInitializedAsync()
        {
            intercomTimer = new Timer(
                async (object stateinfo) => await IntercomExecute(),
                new AutoResetEvent(false),
                0,
                INTERCOM_INTERVAL);

            weatherTimer = new Timer(
                async (object stateinfo) => await WeatherExecute(),
                new AutoResetEvent(false),
                0,
                WEATHER_INTERVAL);

            return base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("SetFocusToElement", mainDiv);
            }
        }

        private async Task IntercomExecute()
        {
            try
            {
                IntercomData = await IntercomService.getIntercomData();
                if (IntercomData.External.Equals("ON", StringComparison.InvariantCultureIgnoreCase))
                {
                    alert = true;
                    bottomBoxMessageLine1 = "INTERCOM !!";
                }
                else
                {
                    alert = false;
                    bottomBoxMessageLine1 = "";
                }
                intercomState = "Online";
            }
            catch (Exception ex)
            {
                intercomState = "Offline";
                Console.Error.WriteLine(ex.Message);
            }
            finally
            {
                clock = DateTime.Now.ToString("HH:mm - dd/MM/yyyy");
                StateHasChanged();
            }
        }

        private async Task WeatherExecute()
        {
            try
            {
                WeatherData = await WeatherService.getWeather();
                weatherState = "Online";
            }
            catch (Exception ex)
            {
                weatherState = "Offline";
                Console.Error.WriteLine(ex.Message);
            }
            finally
            {
                StateHasChanged();
            }
        }
    }
}