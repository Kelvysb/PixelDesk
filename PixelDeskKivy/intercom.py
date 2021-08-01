import logging
import requests
import json

def get_intercom(intercomUri):
    try:
        payload={}
        headers = {}

        response = requests.request("GET", intercomUri, headers=headers, data=payload)

        return json.loads(response.text)
    except Exception as e:
        logging.error(str(e))
        return None