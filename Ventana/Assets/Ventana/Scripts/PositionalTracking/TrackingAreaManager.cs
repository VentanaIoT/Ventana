using SocketIO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingAreaManager : MonoBehaviour {
    public int ID = 1;
    public GameObject thimbleObject; //for now we will call the thing being tracked a thimble
    public GameObject originObject;
    public SocketIOComponent socket;
    private List<string> workingFileLines;

    // Use this for initialization
    void Start () {
        //socket.On("position", HandlePositionUpdate);
        workingFileLines = new List<string>();
        string[] temp = OpenTextFileToUse("sample_positional_data.txt").Split('\n');
        
        foreach (string line in temp ) {
            workingFileLines.Add(line);
        }
        DigestPositionData();
	}

   public void HandlePositionUpdate(SocketIOEvent ev) {
        string[] positionArray = ev.data.GetField(ID.ToString()).ToString().Split(' ');
        char[] trimChars = { ' ', '"' };
        float x = float.Parse(positionArray[0].TrimStart( trimChars).TrimEnd( trimChars));
        float y = float.Parse(positionArray[1].TrimStart( trimChars).TrimEnd(trimChars));
        float z = float.Parse(positionArray[2].TrimStart( trimChars).TrimEnd(trimChars));
        Vector3 received = new Vector3(x, y, z);
        Vector3 newPosition = received + originObject.transform.localPosition;
        thimbleObject.transform.localPosition = newPosition;

    }
    
    public void DigestPositionData() {
        if ( workingFileLines.Count > 0 ) {
            JSONObject toSend = new JSONObject();
            toSend.AddField(ID.ToString(), workingFileLines[0]);
            SocketIOEvent newEv = new SocketIOEvent("position", toSend);
            workingFileLines.RemoveAt(0);
            HandlePositionUpdate(newEv);
        }
    }

    private void OnDestroy() {
        //socket.Off("position", HandlePositionUpdate);
    }

    public string OpenTextFileToUse(string assetName) {
        string fileData = "";
#if UNITY_EDITOR
        string fileName = GetFilePath("Ventana/"+assetName ); //for unity
#elif WINDOWS_UWP
        string fileName = ApplicationData.Current.LocalFolder.Path + "/assetName";//for hololens
#endif
        Debug.Log(fileName);
        byte[] bytes = UnityEngine.Windows.File.ReadAllBytes(fileName);
        fileData = System.Text.Encoding.ASCII.GetString(bytes);
        //This works on both platforms
        return fileData;
    }

    // Update is called once per frame
    void Update () {
        DigestPositionData();
	}

    string GetFilePath(string fileName) {
        return Application.streamingAssetsPath + "/" + fileName;
    }
}
