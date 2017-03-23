using UnityEngine;
using System.Collections;

[System.Serializable]
public class SonosInfo : VentanaInteractable {
    //PLAYING --> playing
    //PAUSED_PLAYBACK --> paused
    public bool isPaused {
        get {
            switch ( current_transport_state ) {
                case "PLAYING":
                return false;
                case "PAUSED_PLAYBACK":
                return true;
                default:
                return true;
            }
        }
    }
    public string album;
    public string artist;
    public string title;
    public string uri;
    public int playlist_position;
    public string duration;
    public string position;
    public string album_art;
    public string current_transport_state = "";
    public string metadata;

    public static SonosInfo CreateFromJSON(string jsonString) {
        return JsonUtility.FromJson<SonosInfo>(jsonString);
    }

    public override string ToString() {
        return "Album: " + album + " Artist: " + artist + " Title: " + title +
                " URI: " + uri + " Playlist Position: " + playlist_position + " Duration " +
                duration + " Position: " + position + " Album Art URL: " + album_art + " Metadata: " + metadata + " Paused: " + isPaused;
    }

    // Given JSON input:
    // {"name":"Dr Charles","lives":3,"health":0.8}
    // this example will return a PlayerInfo object with
    // name == "Dr Charles", lives == 3, and health == 0.8f.

}



public class VentanaInteractable {

}


