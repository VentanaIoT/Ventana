using System.Collections.Generic;
using UnityEngine;
using VentanaModelDictionary = System.Collections.Generic.Dictionary<int, string>;
using System.IO;
using System;

public class ModelController  {
    
    //Make Singleton that feeds the initializer the models it needs. 
    //This is gonna control what model goes with what Image Target

    private static ModelController instance;
    private VentanaModelDictionary modelDictionary;
    private VentanaUser user;
    private bool shouldWrite = true;

    private ModelController() {
        user = initializeUser();
    }

    public static ModelController Instance {
        get {
            if (instance == null ) {
                instance = new ModelController();
            }
            return instance;
        }
    }

    private VentanaUser initializeUser() {
        VentanaUser user = null;
        VentanaModelDictionary vmDictionary = new VentanaModelDictionary();

        string json = ReadFromConfig();
        user = JsonUtility.FromJson<VentanaUser>(json);

        if ( user == null ) {
            Debug.Log("<color=yellow>Warning: Missing or malformed Ventana configuration file");
            return null;
        }

        foreach ( VentanaMarkObject vmo in user.VentanaMarks ) {
            vmDictionary.Add(Convert.ToInt32(vmo.id, 16), vmo.path);
        }

        modelDictionary = vmDictionary;
        
        return user;
    }

    private VentanaModelDictionary initializeModelMapping() {
        VentanaModelDictionary vmDictionary = new VentanaModelDictionary();
        VentanaUser jsonObject = null;

        
        string json = ReadFromConfig();
        jsonObject = JsonUtility.FromJson<VentanaUser>(json);
        
        if ( jsonObject == null ) {
            Debug.Log("<color=yellow>Warning: Missing or malformed Ventana configuration file");
            return null;
        }

        foreach (VentanaMarkObject vmo in jsonObject.VentanaMarks ) {
            vmDictionary.Add(Convert.ToInt32(vmo.id, 16), vmo.path);
        }

        return vmDictionary;
    }

    public GameObject GetPrefabWithId(int id) {
        GameObject prefab = null;
        string value = "";
        if ( modelDictionary.TryGetValue(id, out value) ) { //key exists
            //now check if a prefab exists...
            UnityEngine.Object possiblePrefab = Resources.Load(value);
            if ( possiblePrefab ) {
                if ( !(prefab = GameObject.Instantiate(possiblePrefab) as GameObject )) {
                    Debug.Log("<color=red>Error: the prefab at " + value + " does not exist</color>");
                    throw new ModelControllerException("prefab not found");
                }
            }

        } else {
            Debug.Log("<color=red>Error: Key in Ventana Config does not exists in ModelController</color>");
            throw new ModelControllerException("Key not found");
        }
       
        return prefab;
    }
    public string ReadFromConfig() {
        TextAsset jsonFile = Resources.Load<TextAsset>(Args.VENTANA_MARK_CONFIG_FILE_LOCATION.Replace(".json", ""));
        return jsonFile.text;
    }

    public bool WriteToConfig(bool write) {
        return true;
    }
}



public class ModelControllerException : Exception {
    string message;
    public ModelControllerException(string message) {
        this.message = message;
    }
}