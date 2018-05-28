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
        public float time = 5f;
        public bool onScan; // 0 => On scene load / 1 => On scan
    }

    public List<Popup> popups;
    
    private static MessageManager instance;
    private static GameObject globalManagerCanvas;
    private static List<GameObject> popupList = new List<GameObject>();
    private static List<GameObject> imageTargetList = new List<GameObject>();
        

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
    
    
    public void OnScan()
    {
        var filteredPopup = new List<Popup>();
        
        // Filter message on scene load
        foreach (var popup in popups)
        {
            if (popup.onScan) {
                filteredPopup.Add(popup);
            }
        }

        
        // Hide prev messages
        var globalMessagesWrapper = GameObject.FindGameObjectsWithTag("message");
        if (globalMessagesWrapper != null)
        {
            foreach (var message in globalMessagesWrapper)
            {
                var popupPanelImage = message.GetComponentsInChildren<Image>()[1];
                var popupPanelText = message.GetComponentInChildren<Text>();
                Hide(message, popupPanelImage, popupPanelText);
            }
        }
        
        
        // If a message exist show it
        if (filteredPopup.Count > 0) {
            var message = filteredPopup[0];
            ShowMessage(message.messageSlug, message.time);
        }
    }
    
    
    public static void ShowMessage(string key, float time) { 
        var list = GlobalManager.instance.messageList; 
        
        foreach (var item in list) {
            if (item.id == key) {
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
        
        cloneText.text = item.content;
        Show(cloneWrapper, popupPanelImage, popupPanelText);
        yield return new WaitForSeconds(time);
        Hide(cloneWrapper, popupPanelImage, popupPanelText);
    }

    
    private static void Show(GameObject cloneWrapper, Image popupPanelImage, Text popupPanelText)
    {    
        popupList.Add(cloneWrapper);
        cloneWrapper.transform.localPosition = Vector3.zero;
        cloneWrapper.transform.localScale = Vector3.one;
        cloneWrapper.SetActive(true);
        popupPanelImage.CrossFadeAlpha(1.0f, 1.0f, false);
        popupPanelText.CrossFadeAlpha(1.0f, 1.0f, false);
        
        // Image targets to hide
        var targetChoice = GameObject.FindGameObjectsWithTag("targetChoice");
        var targetImage = GameObject.FindGameObjectsWithTag("targetImage");
		
        foreach (var item in targetChoice) {
            item.SetActive(false);
            imageTargetList.Add(item);
        }
		
        foreach (var item in targetImage) {
            item.SetActive(false);
            imageTargetList.Add(item);
        }

    }

    
    private static void Hide(GameObject cloneWrapper, Image popupPanelImage, Text popupPanelText)
    {    
        popupPanelImage.CrossFadeAlpha(0.0f, 1.0f, false);
        popupPanelText.CrossFadeAlpha(0.0f, 1.0f, false);
                		
        cloneWrapper.SetActive(false);

        popupList.Remove(cloneWrapper);
        
        instance.StartCoroutine(Show3D());
    }
    
    private static IEnumerator Show3D()
    {
        yield return new WaitForSeconds(1f);
        foreach (var item in imageTargetList) {
            if (item != null) {
                item.SetActive(true);
            }
        }
    }

}