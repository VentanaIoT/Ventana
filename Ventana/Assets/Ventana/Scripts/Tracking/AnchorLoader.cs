using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using UnityEngine.VR.WSA.Persistence;
using HoloToolkit.Unity.SpatialMapping;
using System.Collections.Generic;
using System;

public class AnchorLoader : MonoBehaviour
{
    bool loaded;

    private void Start()
    {
        WorldAnchorStore.GetAsync(OnWorldAnchorStoreLoaded);
    }

    private void OnWorldAnchorStoreLoaded(WorldAnchorStore store) {

        var persistentGameObject = GameObject.Find("persistentGameObject");
        persistentGameObjectScript persistentScript = persistentGameObject.GetComponent<persistentGameObjectScript>();

        if (persistentScript.loadWorldAnchors)
        {
            Debug.Log("LOADING WORLD ANCHORS");
            var ids = store.GetAllIds();

            foreach (var id in ids)
            {

                char[] delimiterChars = { ':' };
                string[] anchorInfo = id.ToString().Split(delimiterChars);
                Debug.Log("<color=yellow>Anchor ID:" + anchorInfo[0] + " Creation Time: " + anchorInfo[1]);

                ModelController mc = ModelController.Instance;
                int integerID = Convert.ToInt32(anchorInfo[0]);
                try
                {
                    GameObject go = mc.GetPrefabWithId(integerID);
                    BaseVentanaController bvc = go.GetComponent<BaseVentanaController>();
                    if (bvc)
                    {
                        bvc.OnVumarkFound();
                        bvc.VentanaID = Convert.ToInt32(anchorInfo[0]);

                    }
                    TapToPlace ttp = go.AddComponent<TapToPlace>();
                    ttp.SavedAnchorFriendlyName = id;
                    go.transform.localScale = new Vector3(0.1175234f, 0.1175234f, 0.1175234f);
                    ttp.layerMask = SpatialMappingManager.Instance.LayerMask;
                    store.Load(id, go);
                }
                catch (ModelControllerException ex)
                {
                    Debug.Log(ex.Message);
                }
            }
        }
        else
        {
                store.Clear();
                Debug.Log("World Anchor Store CLEARED");
        }
    }
    private void Update()
    {
    }
}
