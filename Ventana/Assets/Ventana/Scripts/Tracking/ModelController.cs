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

    private ModelController() {
        modelDictionary = initializeModelMapping();
    }

    public static ModelController Instance {
        get {
            if (instance == null ) {
                instance = new ModelController();
            }
            return instance;
        }
    }

    private VentanaModelDictionary initializeModelMapping() {
        VentanaModelDictionary vmDictionary = new VentanaModelDictionary();
        VentanaUser jsonObject = null;

        TextAsset jsonFile = Resources.Load<TextAsset>(Args.VENTANA_MARK_CONFIG_FILE_LOCATION.Replace(".json", ""));
        string json = jsonFile.text;
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
}

public class ModelControllerException : Exception {
    string message;
    public ModelControllerException(string message) {
        this.message = message;
    }
}