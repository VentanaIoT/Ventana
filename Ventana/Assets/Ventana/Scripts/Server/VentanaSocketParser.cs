using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class VentanaSocketData  {
    public string socketCode;
    public string channel;
    public string jsonPayload;

    public static VentanaSocketData ParseFromString(string str) {
        VentanaSocketData socc = new VentanaSocketData();

        int firstBracketIndex = str.IndexOf('[');
        int firstCommaIndex = str.IndexOf(',');
        int firstSquiggle = str.IndexOf('{');
        try {
            string code = (firstBracketIndex == -1) ? "" : str.Substring(0, firstBracketIndex);
            socc.socketCode = code;
            //Debug.Log("CODE: " + code);
            if (code == "42"  &&
                ((firstBracketIndex < firstSquiggle) && ( firstCommaIndex < firstSquiggle )) ){ // the message you want to listen to
                string channel = str.Substring(firstBracketIndex + 2, (firstCommaIndex - firstBracketIndex) - 3);
                string JSON = str.Substring(firstCommaIndex + 2, (str.Length - firstCommaIndex) - 3);
                socc.channel = channel;
                socc.jsonPayload = "{" + JSON + "}";
                //Debug.Log("CHANNEL: " + channel);
                //Debug.Log("JSON: " + JSON);
            } else {
                socc = null;
                Debug.Log("Socket Message Not Listned To: " + str);
            }

        } catch ( Exception ex ) {
            Debug.Log(ex.Message);
            socc = null; //if null not a request we can handle
        }

        return socc;
    }

    public override string ToString() {
        return "Channel: " + channel + " :: PayLoad: " + jsonPayload;
    }
}
