using UnityEngine;
using System.Collections;

public class AlbumSlabTextureController : MonoBehaviour {
    public string newURL;
    public Texture oldTexture;
    void Start() {
        newURL = "http://is5.mzstatic.com/image/thumb/Music3/v4/47/97/af/4797af7e-24c9-7428-ac64-5b5f35eba51e/source/100000x100000-999.jpg";
        WWW www = new WWW(newURL);
        StartCoroutine(ChangeAlbumTexture(www));
    }

    // Update is called once per frame
    void Update() {


    }

    IEnumerator ChangeAlbumTexture(WWW www) {
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

    void OnURLSent(VentanaInteractable venta) {
        //album art contains the URL
        SonosInfo info = venta as SonosInfo;
        Debug.Log(info.album_art);
        if ( newURL != info.album_art ) {
            Debug.Log("Im getting changed");
            WWW www = new WWW(info.album_art);
            newURL = info.album_art;
            StartCoroutine(ChangeAlbumTexture(www));

        }
    }
}
