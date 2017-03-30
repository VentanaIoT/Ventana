using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class ModelControllerInitializer : MonoBehaviour {

	// Use this for initialization
	IEnumerator Start () {
        StringBuilder url = new StringBuilder(Args.HOLOHUB_ADDRESS);
        url.Append("/holoconfig/");
        UnityWebRequest holoHubRequest = UnityWebRequest.Get(url.ToString());
        yield return holoHubRequest.Send();

        if ( !holoHubRequest.isError ) {
            try {
                ModelController mc = ModelController.Instance;
                Debug.Log(holoHubRequest.downloadHandler.text);
                VentanaUser user = JsonUtility.FromJson<VentanaUser>(holoHubRequest.downloadHandler.text);
                mc.WriteToConfig(user);
                mc.initializeUser();
                    
            }
            catch ( Exception e ) {
                Debug.LogWarning(e.Message + " \nHoloHub Response Val: " + holoHubRequest.downloadHandler.text);
            }

        } else {
            Debug.Log("WWW Error: " + holoHubRequest.error);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
