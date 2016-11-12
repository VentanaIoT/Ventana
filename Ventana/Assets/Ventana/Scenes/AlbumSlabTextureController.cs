using UnityEngine;
using System.Collections;

public class AlbumSlabTextureController : MonoBehaviour {
    private string newURL = "http://is5.mzstatic.com/image/thumb/Music71/v4/c0/39/b4/c039b43b-11cf-2a33-6a77-2323fb4c2b9f/source/100000x100000-999.jpg";



    void Start() {
        WWW www = new WWW(newURL);
        StartCoroutine(WaitForRequest(www));
    }

    // Update is called once per frame
    void Update () {
	
	}

    IEnumerator WaitForRequest(WWW www) {
        yield return www;

        if ( www.error == null ) {
            Debug.Log("WWW OK!");
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.mainTexture = www.texture;

        } else {

        }
    }

    void OnURLSent(string url) {
        Debug.Log(url);
        WWW www = new WWW(newURL);
        StartCoroutine(WaitForRequest(www));
    }
}
