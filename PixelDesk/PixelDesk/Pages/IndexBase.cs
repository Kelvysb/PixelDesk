using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;
using PixelDesk.Domain.Abstractions.Services;
using PixelDesk.Domain.Extesnsions;
using PixelDesk.Domain.Models;

namespace PixelDesk.Pages
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
        private static int CLOCK_INTERVAL = 1000;
        private static int WEATHER_INTERVAL = 30000;
        private Timer weatherTimer;
        private Timer clockTimer;
        private DateTime lastAlarm = DateTime.UtcNow.AddMinutes(-1);
        private int alarmRetain = 5;
        private int intercomOnlineTime = 20;
        private DateTime lastIntercomSignal = DateTime.UtcNow.AddMinutes(-1);

        [Inject]
        public IWeatherService WeatherService { get; set; }

        [Inject]
        public IIntercomService IntercomService { get; set; }

        public IntercomData IntercomData { get; set; }

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

            clockTimer.Dispose();
            clockTimer = null;
        }

        protected override async Task OnInitializedAsync()
        {
            clockTimer = new Timer(
                async (object stateinfo) => await ClockExecute(),
                new AutoResetEvent(false),
                0,
                CLOCK_INTERVAL);

            weatherTimer = new Timer(
                async (object stateinfo) => await WeatherExecute(),
                new AutoResetEvent(false),
                0,
                WEATHER_INTERVAL);

            await IntercomService.Subscribe(async (IntercomData intercomData) => await IntercomUpdate(intercomData));

            await base.OnInitializedAsync();
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await JSRuntime.InvokeVoidAsync("SetFocusToElement", mainDiv);
            }
        }

        private async Task IntercomUpdate(IntercomData intercomData)
        {
            try
            {
                if (intercomData == null) return;
                lastIntercomSignal = DateTime.UtcNow;
                if (intercomData.Intercom)
                {
                    alert = true;
                    bottomBoxMessageLine1 = "INTERCOM !!";
                    lastAlarm = DateTime.UtcNow;
                }
                else if (DateTime.UtcNow.Subtract(lastAlarm).TotalSeconds >= alarmRetain)
                {
                    alert = false;
                    bottomBoxMessageLine1 = "";
                }
                intercomState = "Online";
            }
            catch (Exception ex)
            {
                alert = false;
                bottomBoxMessageLine1 = "";
                intercomState = "Offline";
                Console.Error.WriteLine(ex.Message);
            }
            finally
            {
                await InvokeAsync(() => StateHasChanged());
            }
        }

        private async Task ClockExecute()
        {
            try
            {
                clock = DateTime.UtcNow.Local().ToString("HH:mm - dd/MM/yyyy");
                if (DateTime.UtcNow.Subtract(lastIntercomSignal).TotalSeconds > intercomOnlineTime)
                {
                    intercomState = "Offline";
                    alert = false;
                    bottomBoxMessageLine1 = "";
                }
                await InvokeAsync(() => StateHasChanged());
            }
            catch
            {
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
                await InvokeAsync(() => StateHasChanged());
            }
        }
    }
}