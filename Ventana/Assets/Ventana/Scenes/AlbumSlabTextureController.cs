using UnityEngine;
using System.Collections;

public class AlbumSlabTextureController : MonoBehaviour {
    public string newURL;
    public Texture oldTexture;
    void Start() {
        newURL = "http://is5.mzstatic.com/image/thumb/Music3/v4/47/97/af/4797af7e-24c9-7428-ac64-5b5f35eba51e/source/100000x100000-999.jpg";
        WWW www = new WWW(newURL);
        StartCoroutine(WaitForRequest(www));

        InvokeRepeating("requestAlbum", 1.0f, 0.25f);
    }

    // Update is called once per frame
    void Update() {


    }

    void requestAlbum() {
        gameObject.SendMessageUpwards("makeAPIRequest", "albumArt");
        Debug.Log("Requesting the Album Art...");

    }

    IEnumerator WaitForRequest(WWW www) {
        yield return www;

        if ( www.error == null ) {
            Debug.Log("WWW OK!");
            GetComponent<Renderer>().material.mainTexture = null;
            //www.LoadImageIntoTexture(renderer.material.mainTexture);
            Renderer renderer = GetComponent<Renderer>();
            renderer.material.mainTexture = www.texture;
            Debug.Log(www.texture);


        } else {
            Debug.Log("Not changing texture");
        }
    }

    void OnURLSent(string url) {
        Debug.Log(url);
        if ( newURL != url ) {
            Debug.Log("Im getting changed");
            WWW www = new WWW(url);
            newURL = url;
            StartCoroutine(WaitForRequest(www));

        }
    }
}
