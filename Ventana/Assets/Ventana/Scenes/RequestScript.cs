using UnityEngine;
using System.Collections;

public class RequestScript : MonoBehaviour {

    public string url = "http://localhost:5000/";

    // Use this for initialization
    void makeAPIRequest(string child) {

        switch (child)
        {
            case "play":
                Debug.Log("Bubbled play");
                StartCoroutine(callToAPI("play"));
                break;
            case "next":
                Debug.Log("Bubbled next");
                StartCoroutine(callToAPI("next"));
                break;
            case "previous":
                Debug.Log("Bubbled previous");
                StartCoroutine(callToAPI("previous"));
                break;
            case "albumArt":
                Debug.Log("Bubbled albumArt");
                StartCoroutine(callToAPI("picture"));
                break;
            default:
                Debug.Log("No good bubble called");
                break;
        }

    }

    IEnumerator callToAPI(string request, string parameters = null)
    {
        url += request;

        if (parameters != null)
        {
            url += "?" + parameters;
        }
        WWW www = new WWW(url);

        yield return www;
        string responseString = www.text;


        // check for errors
        if (www.error == null)
        {
            Debug.Log("WWW Ok!: " + responseString);
            gameObject.BroadcastMessage("OnURLSent", responseString);
        }
        else
        {
            Debug.Log("WWW Error: " + www.error);
        }

    }
}
