using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    
    [System.Serializable]
    public class Popup
    {
        public string messageSlug;
        public float time = 2f;
        public bool onScan; // 0 => On scene load / 1 => On scan
    }

    public List<Popup> popups;
    
    private static MessageManager instance;
    private static GameObject globalManagerCanvas;
    private static List<GameObject> popupList = new List<GameObject>();
        

    private void Awake() {
        instance = this;
        globalManagerCanvas = GameObject.Find("GlobalManagerCanvas");
    }

    public void OnLoadScene()
    {
        var filteredPopup = new List<Popup>();
        
        // Filter message on scene load
        foreach (var popup in popups)
        {
            if (!popup.onScan)
            {
                filteredPopup.Add(popup);
            }
        }

        // If a message exist show it
        if (filteredPopup.Count > 0)
        {
            var message = filteredPopup[0];
            ShowMessage(message.messageSlug, message.time);
        }
    }
    
    
    public static void ShowMessage(string key, float time) { 
        var list = GlobalManager.instance.messageList; 
        
        foreach (var item in list) {
            if (item.key == key) {
               ActivePopup(item, time);
            } 
        }
    }

    private static void ActivePopup(Message item, float time)
    {
        var obj = Resources.Load("Prefabs/ui/modalDialoguePanelWrapper") as GameObject;
        var cloneWrapper = Instantiate (obj) as GameObject;
        cloneWrapper.SetActive(false);
        cloneWrapper.transform.parent = globalManagerCanvas.transform;
    
        var cloneText = cloneWrapper.GetComponentInChildren<Text>();
        
        instance.StartCoroutine(DisplayPopup(cloneWrapper, cloneText, item, time));
    }


    private static IEnumerator DisplayPopup(GameObject cloneWrapper, Text cloneText,  Message item, float time)
    {

        var popupPanelImage = cloneWrapper.GetComponentsInChildren<Image>()[1];
        var popupPanelText = cloneWrapper.GetComponentInChildren<Text>();

        popupPanelImage.canvasRenderer.SetAlpha(0f);
        popupPanelText.canvasRenderer.SetAlpha(0f);
        
        cloneText.text = item.value;
        Show(cloneWrapper, popupPanelImage, popupPanelText);
        yield return new WaitForSeconds(time);
        Hide(cloneWrapper, popupPanelImage, popupPanelText);
    }

    
    private static void Show(GameObject cloneWrapper, Image popupPanelImage, Text popupPanelText)
    {    
        popupList.Add(cloneWrapper);
        cloneWrapper.SetActive(true);
        popupPanelImage.CrossFadeAlpha(1.0f, 1.0f, false);
        popupPanelText.CrossFadeAlpha(1.0f, 1.0f, false);
    }

    
    private static void Hide(GameObject cloneWrapper, Image popupPanelImage, Text popupPanelText)
    {    
        popupPanelImage.CrossFadeAlpha(0.0f, 1.0f, false);
        popupPanelText.CrossFadeAlpha(0.0f, 1.0f, false);
        popupList.Remove(cloneWrapper);
    }

}