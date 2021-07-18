import cProfile
from datetime import datetime
from threading import Thread
from time import sleep
from pixelDeskConfig import get_config
from weather import get_weather
from intercom import get_intercom
from kivy.app import App
from kivy.uix.boxlayout import BoxLayout 
from kivy.uix.image import Image, AsyncImage
from kivy.uix.label import Label
from kivy.uix.screenmanager import ScreenManager, Screen
from kivy.properties import NumericProperty, StringProperty, ColorProperty
from kivy.config import Config
from kivy.core.window import Window

BACKGROUND_COLOR = [0, 0, 0, 1]
ALARM_COLOR = [1, 0, 0, 1]
FONT_COLOR = [1, 1, 1, 1]
PIXEL_SIZE = 3
Config.set('kivy','window_icon','images/icon.png')

class Manager(ScreenManager):
    pass

class PixelImage(Image):
    def __init__(self, **kwargs):
        super().__init__(**kwargs)        
        self.keep_ratio = True
        self.allow_stretch = True        
        self.bind(texture=self._update_texture_filters)

    def _update_texture_filters(self, image, texture):
        texture.mag_filter = 'nearest'
        self.size = self.texture_size[0] * PIXEL_SIZE, self.texture_size[1] * PIXEL_SIZE

class PixelAsyncImage(AsyncImage):
    def __init__(self, **kwargs):
        super().__init__(**kwargs)        
        self.keep_ratio = True
        self.allow_stretch = True        
        self.bind(texture=self._update_texture_filters)

    def _update_texture_filters(self, image, texture):
        texture.mag_filter = 'nearest'
        self.size = self.texture_size[0] * PIXEL_SIZE, self.texture_size[1] * PIXEL_SIZE

class PixelLabel(Label):
    def __init__(self, **kwargs):
        super().__init__(**kwargs)        
        self.font_name = 'fonts/Minecraft.ttf'

class Main(Screen):
    pass

class Config(Screen):
    pass

class PixelDesk(App):
    running = True
    alarm_on = False
    PIXEL_SIZE = NumericProperty(PIXEL_SIZE)
    TEMPERATURE = NumericProperty(1.0)
    FEELS_LIKE = NumericProperty(1.0)
    WIND = NumericProperty(1.0)
    HUMIDITY = NumericProperty(1.0)
    INTERCOM_STATE = NumericProperty(0)
    WEATHER = StringProperty('')
    WEATHER_DESC = StringProperty('')
    WEATHER_ICON = StringProperty('')
    CLOCK = StringProperty('')
    DATE = StringProperty('')
    BACK_COLOR = ColorProperty(BACKGROUND_COLOR)
    FONT_COLOR = ColorProperty(FONT_COLOR)
    MESSAGE = StringProperty('')

    def __init__(self, **kwargs):
        super(PixelDesk, self).__init__(**kwargs)

        self.pixelDeskConfig = get_config()
        self.update_weather()
        self.update_intercom()

        t_weather = Thread(target=self.weather_timer).start()
        t_intercom = Thread(target=self.intercom_timer).start()
        t_clock = Thread(target=self.clock_timer).start()
    
    def clock_timer(self):
        while self.running:
            sleep(1)
            try:
                self.CLOCK = datetime.now().strftime("%H:%M:%S")
                self.DATE = datetime.now().strftime("%d/%m/%Y")
            except Exception:
                pass

    def weather_timer(self):
        timeout = 30
        count = 0
        while self.running:
            sleep(1)
            count = count + 1
            if count >= timeout:
                count = 0
                self.update_weather()

    def intercom_timer(self):
        timeout = 5
        count = 0
        while self.running:
            sleep(1)
            count = count + 1
            if count >= timeout:
                count = 0
                self.update_intercom()

    def alarm_timer(self):
        toggle = False
        self.MESSAGE = 'Intercom!!!'        
        while self.INTERCOM_STATE == 1 and self.running:            
            sleep(0.5)
            if toggle:
                self.BACK_COLOR = ALARM_COLOR
            else:
                self.BACK_COLOR = BACKGROUND_COLOR              
            toggle = not toggle
        try:
            self.BACK_COLOR = BACKGROUND_COLOR 
            self.alarm_on = False
            self.MESSAGE = ''
        except Exception:
            pass

    def update_weather(self):
        try:
            self.weather_data = get_weather(self.pixelDeskConfig['city'], self.pixelDeskConfig['weatherApiKey'])
            self.TEMPERATURE = int(self.weather_data['main']['temp'])
            self.FEELS_LIKE = int(self.weather_data['main']['feels_like'])
            self.HUMIDITY = int(self.weather_data['main']['humidity'])
            self.WIND = float(self.weather_data['wind']['speed'])
            self.WEATHER = self.weather_data['weather'][0]['main']
            self.WEATHER_DESC = self.weather_data['weather'][0]['description']            
            self.WEATHER_ICON = f'http://openweathermap.org/img/wn/{ self.weather_data["weather"][0]["icon"] }@2x.png'
        except Exception:
            pass

    def update_intercom(self):
        try:
            self.intercom = get_intercom(self.pixelDeskConfig['intercomUrl'])
            self.INTERCOM_STATE = 0 if self.intercom['external'] == 'OFF' else 1
            if self.INTERCOM_STATE == 1 and not self.alarm_on:
                self.alarm_on = True
                t_alarm = Thread(target=self.alarm_timer).start()
        except Exception:
            pass
    
    
    def on_start(self):
        self.running = True
        self.profile = cProfile.Profile()
        self.profile.enable()

    def on_stop(self):
        self.running = False
        self.profile.disable()
        self.profile.dump_stats('pixelDesk.profile')

if __name__ == '__main__':
    Window.fullscreen = True
    Window.show_cursor = False
    PixelDesk().run()