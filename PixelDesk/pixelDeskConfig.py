import json

def get_config():
    with open("config.json") as jsonFile:
        jsonConfig = json.load(jsonFile)
        jsonFile.close()
        
    return jsonConfig
