import requests
import json

def get_intercom(intercomUri):

    payload={}
    headers = {}

    response = requests.request("GET", intercomUri, headers=headers, data=payload)

    return json.loads(response.text)