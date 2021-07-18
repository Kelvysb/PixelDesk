import os
import json

def get_config():
    abspath = os.path.abspath(__file__)
    dname = os.path.dirname(abspath)
    os.chdir(dname)
    with open("config.json") as jsonFile:
        jsonConfig = json.load(jsonFile)
        jsonFile.close()
        
    return jsonConfig
