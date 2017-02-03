using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class VentanaUser {
    public string User;
    public List<VentanaMarkObject> VentanaMarks;

    public override string ToString() {
        string result = "";
        result += "User: " + User + "\n";
        result += "--Image Target Locations--\n";
        foreach(VentanaMarkObject vmo in VentanaMarks ) {
            result += vmo.name + ": " + vmo.id + " " + vmo.path + "\n";
        }
        return result;
    }
}
