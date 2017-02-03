using System.Collections.Generic;
using UnityEngine;
using VentanaModelDictionary = System.Collections.Generic.Dictionary<string, string>;
using System.IO;

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

        using ( StreamReader r = new StreamReader(Args.VENTANA_MARK_CONFIG_FILE_LOCATION) ) {
            string json = r.ReadToEnd();
            jsonObject = JsonUtility.FromJson<VentanaUser>(json);
        }

        if ( jsonObject == null ) {
            Debug.Log("<color=yellow>Warning: Missing or malformed Ventana configuration file");
            return null;
        }

        foreach (VentanaMarkObject vmo in jsonObject.VentanaMarks ) {
            vmDictionary.Add(vmo.id, vmo.path);
        }

        return vmDictionary;
    }

    public GameObject GetPrefabWithId(string id) {
        GameObject prefab = null;
        string value = "";
        if ( modelDictionary.TryGetValue(id, out value) ) { //key exists
            //now check if a prefab exists...
            Object possiblePrefab = Resources.Load(value);
            if ( possiblePrefab ) {
                if ( !(prefab = GameObject.Instantiate(possiblePrefab) as GameObject )) {
                    Debug.Log("<color=red>Error: the prefab at " + value + " does not exist</color>");
                }
            }

        } else {
            Debug.Log("<color=red>Error: Key in Ventana Config does not exists in ModelController</color>");
        }
       
        return prefab;
    }
}
