using HoloToolkit.Unity;
using HoloToolkit.Unity.InputModule;
using UnityEngine;
using HoloToolkit.Unity.SpatialMapping;

public class AnchorLoader : MonoBehaviour
{
    public Transform prefab1;
    public Transform prefab2;
    bool loaded;

    private void Start()
    {
        InputManager.Instance.PushFallbackInputHandler(
          this.gameObject);
    }
    private void Update()
    {
        /* referenced from: https://mtaulty.com/2016/12/20/hitchhiking-the-holotoolkity-unity-leg-5-baby-steps-with-world-anchors-and-persisting-holograms/  */
        if (!this.loaded && (WorldAnchorManager.Instance.AnchorStore != null))
        {
            var ids = WorldAnchorManager.Instance.AnchorStore.GetAllIds();
            
            foreach (var id in ids)
            {
                
                char[] delimiterChars = { ' ', '(' };
                string[] attributes = id.ToString().Split(delimiterChars);
                Debug.Log("<color=yellow>Anchor Name:" + attributes[0]);
                
                if (attributes[0].Equals("MusicController")) {
                    Debug.Log("IN MC");
                    var instance = Instantiate(this.prefab1);
                    WorldAnchorManager.Instance.AttachAnchor(instance.gameObject, id);
                    var go = instance.gameObject;
                    go.AddComponent<TapToPlace>();
                }
                if (attributes[0].Equals("MusicControllerV2")) {
                    Debug.Log("IN MC 2");
                    var instance = Instantiate(this.prefab2);
                    WorldAnchorManager.Instance.AttachAnchor(instance.gameObject, id);
                }
                /*
                var instance = Instantiate(this.prefab1);
                WorldAnchorManager.Instance.AttachAnchor(instance.gameObject, id);
                var go = instance.gameObject;
                go.AddComponent<TapToPlace>();
                */
            }
            this.loaded = true;
        }
    }
}
