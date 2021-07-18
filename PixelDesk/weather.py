import requests
import json

def get_weather(city, api_key):
    try:
        url = f"http://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid={api_key}"

        payload={}
        headers = {}

        response = requests.request("GET", url, headers=headers, data=payload)

        return json.loads(response.text)
    except Exception:
        return None