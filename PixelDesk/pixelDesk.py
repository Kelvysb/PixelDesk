import os
import logging
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
    INTERCOM_STATUS = StringProperty('')
    WEATHER_STATUS = StringProperty('')

    def __init__(self, **kwargs):
        super(PixelDesk, self).__init__(**kwargs)

        self.pixelDeskConfig = get_config()
        self.update_weather()
        self.update_intercom()

        t_clock = Thread(target=self.clock_timer, name='main_clock').start()
    
    def clock_timer(self):
        timeout_weather = 30
        timeout_intercom = 5
        count_weather = 0
        count_intercom = 0

        while self.running:                                
            try:
                self.CLOCK = datetime.now().strftime('%H:%M')
                self.DATE = datetime.now().strftime('%d/%m/%Y')
            except Exception:
                pass

            if count_weather >= timeout_weather:
                count_weather = 0
                self.update_weather()

            if count_intercom >= timeout_intercom:
                count_intercom = 0
                self.update_intercom()

            count_weather = count_weather + 1
            count_intercom = count_intercom + 1

            sleep(1)

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
            if self.weather_data != None:
                self.TEMPERATURE = int(self.weather_data['main']['temp'])
                self.FEELS_LIKE = int(self.weather_data['main']['feels_like'])
                self.HUMIDITY = int(self.weather_data['main']['humidity'])
                self.WIND = float(self.weather_data['wind']['speed'])
                self.WEATHER = self.weather_data['weather'][0]['main']
                self.WEATHER_DESC = self.weather_data['weather'][0]['description']            
                self.WEATHER_ICON = f'http://openweathermap.org/img/wn/{ self.weather_data["weather"][0]["icon"] }@2x.png'
                self.WEATHER_STATUS = 'Weather: online'
            else:
                self.WEATHER_STATUS = 'Weather: offline'
        except Exception:
            self.WEATHER_STATUS = 'Weather: error'

    def update_intercom(self):
        try:
            self.intercom = get_intercom(self.pixelDeskConfig['intercomUrl'])
            if self.intercom != None:
                self.INTERCOM_STATE = 0 if self.intercom['external'] == 'OFF' else 1
                if self.INTERCOM_STATE == 1 and not self.alarm_on:
                    self.alarm_on = True
                    logging.info(f'Intercom call at: {datetime.now().strftime("%d/%m/%Y")} {datetime.now().strftime("%H:%M")}')
                    t_alarm = Thread(target=self.alarm_timer).start()
                self.INTERCOM_STATUS = 'Intercom: online'
            else:
                self.INTERCOM_STATUS = 'Intercom: offline'    
        except Exception:
            self.INTERCOM_STATUS = 'Intercom: error'
    
    
    def on_start(self):
        self.running = True
        logging.basicConfig(filename=self.pixelDeskConfig['logFile'], level=logging.INFO, force=True)
        logging.info('Started')

    def on_stop(self):
        self.running = False
        logging.info('Finished')

if __name__ == '__main__':
    abspath = os.path.abspath(__file__)
    dname = os.path.dirname(abspath)
    os.chdir(dname)   
    Window.fullscreen = True
    Window.show_cursor = False
    PixelDesk().run()
    logging.info('Finished')