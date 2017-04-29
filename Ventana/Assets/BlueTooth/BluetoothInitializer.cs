#if UNITY_EDITOR
using UnityEngine;
using BluetoothSerialClass;

public class BluetoothInitializer : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

#elif WINDOWS_UWP
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BluetoothSerialClass;
using System.Threading.Tasks;

public class BluetoothInitializer : MonoBehaviour {
    BluetoothComponent btComp = new BluetoothComponent();
	// Use this for initialization
	async void Start () {
        await btComp.ConnectDevice("poop");                
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
#endif
