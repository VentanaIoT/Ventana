using UnityEngine;
using System.Collections;
using SocketIO;
using UnityEngine.UI;

public class AlbumSlabTextureController : MonoBehaviour {
    public string newURL;
    public Texture oldTexture;
    public SocketIOComponent socket;
    public Text songText;
    public Text albumText;
    private VentanaMusicController vmc;
    

    void Start() {
        socket = VentanaRequestFactory.Instance.socket;
        newURL = "http://is5.mzstatic.com/image/thumb/Music3/v4/47/97/af/4797af7e-24c9-7428-ac64-5b5f35eba51e/source/100000x100000-999.jpg";
        WWW www = new WWW(newURL);
        StartCoroutine(ChangeAlbumTexture(www));
        socket.On("push", HandlePush);
        vmc = GetComponentInParent<VentanaMusicController>();
    }
    public void HandlePush(SocketIOEvent e) {
        Debug.Log("[SocketIO]" + e.data);
        VentanaInteractable myVentana = SonosInfo.CreateFromJSON(e.data.GetField(vmc.VentanaID.ToString()).ToString());
        if ( myVentana as SonosInfo != null ) {
            SonosInfo ventana = (SonosInfo)myVentana;
            if ( ventana.isPaused ) {
                SendMessageUpwards("SetPauseState");
            } else {
                SendMessageUpwards("SetPlayState");
            }
        }
        OnURLSent(myVentana);
    }

    IEnumerator ChangeAlbumTexture(WWW www) {
        yield return www;
        if ( www.error == null ) {
            var materials = gameObject.GetComponent<Renderer>().materials;
            materials[1].mainTexture = www.texture;
            gameObject.GetComponent<Renderer>().materials = materials;
        } else {
            //Debug.Log("Not changing texture");
        }
    }

    void OnURLSent(VentanaInteractable venta) {
        //album art contains the URL
        SonosInfo info = venta as SonosInfo;
        //Debug.Log(info.album_art);
        if ( newURL != info.album_art ) {
            Debug.Log("Im getting changed");
            WWW www = new WWW(info.album_art);
            newURL = info.album_art;
            songText.text = info.title;
            albumText.text = info.artist;
            StartCoroutine(ChangeAlbumTexture(www));

        }
    }
}
