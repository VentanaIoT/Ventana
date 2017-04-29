using System.Collections.Generic;
using UnityEngine;
using VentanaModelDictionary = System.Collections.Generic.Dictionary<int, string>;
using System.IO;
using System;
using System.Text;
#if !UNITY_EDITOR
using System.Threading.Tasks;
using Windows.Storage;
#endif

public class ModelController {
    
    //Make Singleton that feeds the initializer the models it needs. 
    //This is gonna control what model goes with what Image Target

    private static ModelController instance;
    private VentanaModelDictionary modelDictionary;
    private VentanaUser user;
    private bool shouldWrite = true;
    private bool deployed = false;


    private ModelController() {
#if WINDOWS_UWP
        InitializeConfig();
#elif UNITY_EDITOR

#endif
        initializeUser();
    }
public static ModelController Instance {
        get {
            if (instance == null ) {
                instance = new ModelController();
            }
            return instance;
        }
    }

    public void initializeUser() {
        user = null;
        modelDictionary = null;
        VentanaModelDictionary vmDictionary = new VentanaModelDictionary();

        string json = ReadFromConfig();
        user = JsonUtility.FromJson<VentanaUser>(json);

        if ( user == null ) {
            Debug.Log("<color=yellow>Warning: Missing or malformed Ventana configuration file");
        }

        foreach ( VentanaMarkObject vmo in user.VentanaMarks ) {
            vmDictionary.Add(Convert.ToInt32(vmo.id, 16), vmo.path);
        }

        modelDictionary = vmDictionary;
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
        string fileData = "";
#if UNITY_EDITOR
        string fileName = GetFilePath("Ventana/VentanaConfig.json"); //for unity
#elif WINDOWS_UWP
        string fileName = ApplicationData.Current.LocalFolder.Path + "/VentanaConfig.json";//for hololens
#endif
        Debug.Log(fileName);
        byte[] bytes = UnityEngine.Windows.File.ReadAllBytes(fileName);
        fileData = System.Text.Encoding.ASCII.GetString(bytes);
        //This works on both platforms
        return fileData;
    }

#if UNITY_EDITOR
    public void WriteToConfig(VentanaUser newUser) {
        //string fileData = "";
        string fileName = GetFilePath("Ventana/VentanaConfig.json");
        UnityEngine.Windows.File.WriteAllBytes(fileName, Encoding.ASCII.GetBytes(JsonUtility.ToJson(newUser, true)));
        //This works in UNITY but not HOLOLENS
#elif WINDOWS_UWP
    public void InitializeConfig() {
        //do some logic to only load from the streaming assets folder if the file doesn't exist in local
        Task.Run(
            async () => {
                try {
                    //Get local folder
                    StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                    //Create file
                    StorageFile textFileForWrite = await storageFolder.CreateFileAsync("VentanaConfig.json", CreationCollisionOption.FailIfExists);//skip work

                    //Else is fresh install and thus we should copy the template config to the HoloLens Local Folder
                    StorageFolder dataFolder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Data");
                    StorageFolder streamingAssetsFolder = await dataFolder.GetFolderAsync("StreamingAssets");
                    StorageFolder ventana = await streamingAssetsFolder.GetFolderAsync("Ventana");
                    StorageFile file = await ventana.GetFileAsync("VentanaConfig.json"); //Template Config so that the user can have some feedback
                    string contents = await FileIO.ReadTextAsync(file);
                    //Write to file
                    await FileIO.WriteTextAsync(textFileForWrite, contents);
                    /*
                    //Get file
                    StorageFile textFileForRead = await storageFolder.GetFileAsync("VentanaConfig.json");
                    //Read file
                    string plainText = "";
                    plainText = await FileIO.ReadTextAsync(textFileForRead);
                    Debug.Log(plainText);
                    */
                } catch (Exception ex ) {
                    Debug.Log(ex.Message);
                }
            }).Wait();
    }

    public void WriteToConfig(VentanaUser newUser) {
        Task.Run(
            async () => {
            string newFile = JsonUtility.ToJson(newUser, true);
            try {
                    //Get local folder
                    StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                    //Create file
                    StorageFile textFileForWrite = await storageFolder.CreateFileAsync("VentanaConfig.json", CreationCollisionOption.ReplaceExisting);
                    //Write to file
                    await FileIO.WriteTextAsync(textFileForWrite, newFile);
                    //Get file
                    StorageFile textFileForRead = await storageFolder.GetFileAsync("VentanaConfig.json");
                    //Read file
                    string plainText = "";
                    plainText = await FileIO.ReadTextAsync(textFileForRead);
                } catch ( Exception ex ) {
                    Debug.Log(ex.Message);
            }
        }).Wait();
#endif
    }
    string GetFilePath(string fileName) {
        return Application.streamingAssetsPath + "/" + fileName ;
    }

}



public class ModelControllerException : Exception {
    string message;
    public ModelControllerException(string message) {
        this.message = message;
    }
}