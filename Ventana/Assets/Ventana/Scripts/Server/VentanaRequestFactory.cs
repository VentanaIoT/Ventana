using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity;
using System.Text;
using System;
using UnityEngine.Networking;
using SocketIO;

public class VentanaRequestFactory : Singleton<VentanaRequestFactory> {
    

    [Tooltip("In the form of http://yourholohubip:port")]
    public string HoloHubURI = Args.HOLOHUB_ADDRESS;
    private string MusicEndpoint = "/sonos/";
    private string LightEndpoint = "/wink/";
    private string PowerEndpoint = "/wink/";
    public SocketIOComponent socket;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    

    //this will do web requests...
    /* MUSIC */


    public IEnumerator PostToMusicAPIEndpoint(string action, int id, string data) {
        StringBuilder url = new StringBuilder(HoloHubURI);
        url.Append(MusicEndpoint);
        url.Append(action + "/");
        //url.Append(id.ToString());
        //post data is not needed for this endpoint
        //Debug.Log("ACTION: " + action + " URL: " + url.ToString());
        url.Append(id.ToString());
        
        url.Append("/");

        Debug.Log("ACTION: " + action + " URL: " + url.ToString());
        Dictionary<string, string> request = new Dictionary<string, string>();
        request.Add("value", data);
        UnityWebRequest holoHubRequest = UnityWebRequest.Post(url.ToString(), request);
        yield return holoHubRequest.Send();
        if ( !holoHubRequest.isError ) {
            //Debug.Log("WWW Ok!: " + responseString);
        } else {
            Debug.Log("WWW Error: " + holoHubRequest.error);
        }


    }

    public IEnumerator GetFromMusicAPIEndpoint(string action, int id, Action<VentanaInteractable> callback) {
        StringBuilder url = new StringBuilder(HoloHubURI);
        url.Append(MusicEndpoint);
        url.Append(action + "/");
        //url.Append(id.ToString());
        //realistically its only for status...
        
        url.Append(id.ToString());

        url.Append("/");
        Debug.Log("ACTION: " + action + " URL: " + url.ToString());
        UnityWebRequest holoHubRequest = UnityWebRequest.Get(url.ToString());
        yield return holoHubRequest.Send();

        if ( !holoHubRequest.isError ) {
            //Debug.Log("WWW Ok!: " + responseString);
            if ( action == "status" && callback != null ) {
                try {
                    VentanaInteractable myVentana = SonosInfo.CreateFromJSON(holoHubRequest.downloadHandler.text);
                    if ( myVentana != null ) {
                        callback(myVentana);
                    }
                } catch (ArgumentException e ) {
                    Debug.LogWarning(e.Message + " \nHoloHub Response Val: " + holoHubRequest.downloadHandler.text);
                }
            }

        } else {
            Debug.Log("WWW Error: " + holoHubRequest.error);
        }
    }


    /* LIGHT */

    public IEnumerator PostToLightAPIEndpoint(string action, int id, string data) {
        StringBuilder url = new StringBuilder(HoloHubURI);
        url.Append(LightEndpoint);
        url.Append(action + "/");
        url.Append(id.ToString() + "/");
        // url.Append(data);

        Debug.Log("ACTION: " + action + " URL: " + url.ToString());
        Dictionary<string, string> request = new Dictionary<string, string>();
        request.Add("value", data);

        UnityWebRequest holoHubRequest = UnityWebRequest.Post(url.ToString(), request);
        yield return holoHubRequest.Send();

        if ( !holoHubRequest.isError ) {
            //Debug.Log("WWW Ok!: " + responseString);
        } else {
            Debug.Log("WWW Error: " + holoHubRequest.error);
        }

    }

    public IEnumerator GetFromLightAPIEndpoint(string action, int id, Action<string, VentanaInteractable> callback) {
        StringBuilder url = new StringBuilder(HoloHubURI);
        url.Append(LightEndpoint);
        url.Append(action + "/");
        url.Append(id.ToString() + "/");

        Debug.Log("ACTION: " + action + " URL: " + url.ToString());
        UnityWebRequest holoHubRequest = UnityWebRequest.Get(url.ToString());
        yield return holoHubRequest.Send();

        if ( !holoHubRequest.isError ) {
            //Debug.Log("WWW Ok!: " + responseString);
            if ( action == "status" ) {
                //VentanaInteractable myVentana = SonosInfo.CreateFromJSON(holoHubRequest.downloadHandler.text);
                //callback("OnURLSent", myVentana);
            }

        } else {
            Debug.Log("WWW Error: " + holoHubRequest.error);
        }
    }

    /* POWER */

    public void PostToPowerAPIEndpoint(string action, int id, string data) {
        StringBuilder url = new StringBuilder(HoloHubURI);
        url.Append(PowerEndpoint);
        throw new NotImplementedException();


    }

    public void GetFromPowerAPIEndpoint(string action, int id) {
        StringBuilder url = new StringBuilder(HoloHubURI);
        url.Append(PowerEndpoint);
        throw new NotImplementedException();

    }

}
