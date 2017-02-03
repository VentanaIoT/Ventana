using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class RequestScript : MonoBehaviour {

    public string url = "http://localhost:50000/";

    void Start() {
        InvokeRepeating("requestAlbum", 1.0f, 1.0f);
    }

    // Use this for initialization
    void makeAPIRequest(string child) {

        switch (child)
        {
            case "play":
                Debug.Log("Bubbled play");
                StartCoroutine(callToAPI("play"));
                break;
            case "pause":
                Debug.Log("Bubbled pause");
                StartCoroutine(callToAPI("pause"));
                break;
            case "next":
                Debug.Log("Bubbled next");
                StartCoroutine(callToAPI("next"));
                break;
            case "previous":
                Debug.Log("Bubbled previous");
                StartCoroutine(callToAPI("previous"));
                break;
            case "status":
                //Debug.Log("Bubbled albumArt");
                StartCoroutine(callToAPI("status"));
                break;
            default:
                Debug.Log("No good bubble called");
                break;
        }

    }
    void requestAlbum() {
        makeAPIRequest("status");
    }

    IEnumerator callToAPI(string request, string parameters = null)
    {
        string newUrl = url;
        if (parameters != null)
        {
            newUrl += "?" + parameters;
        }

        newUrl += request;
        //Debug.Log(newUrl);
        UnityWebRequest www = UnityWebRequest.Get(newUrl);
        yield return www.Send();
        


        // check for errors
        if (!www.isError)
        {
            //Debug.Log("WWW Ok!: " + responseString);
            
            if (newUrl == url + "status" ) {
                VentanaInteractable myVentana = SonosInfo.CreateFromJSON(www.downloadHandler.text);
                gameObject.BroadcastMessage("OnURLSent", myVentana);
            }
            
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }

    }
}
