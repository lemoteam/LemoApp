using UnityEngine;

public class CanvasToCamera : MonoBehaviour {
    public Canvas CurrentCanvas;

    public void Awake()
    {
        var tempObject = GameObject.Find("Canvas");
        if (tempObject == null) return;
        //If we found the object, get the Canvas component from it.
        CurrentCanvas = tempObject.GetComponent<Canvas>();
        CurrentCanvas.renderMode = RenderMode.ScreenSpaceCamera;
        CurrentCanvas.worldCamera = GlobalManager.instance.GetComponent<Camera>();
    }
}
