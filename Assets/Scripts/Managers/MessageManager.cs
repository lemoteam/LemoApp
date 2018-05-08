using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    private static MessageManager instance;
    
    private static GameObject globalManagerCanvas;
    
    private static List<GameObject> popupList = new List<GameObject>();
    

    private void Awake() {
        instance = this;
        globalManagerCanvas = GameObject.Find("GlobalManagerCanvas");
    }

    
    public static void ShowMessage(string key) { 
        var list = GlobalManager.instance.messageList; 
        
        foreach (var item in list) {
            if (item.key == key) {
               ActivePopup(item);
            } 
        }
    }

    private static void ActivePopup(Message item)
    {
        var obj = Resources.Load("Prefabs/ui/modalDialoguePanelWrapper") as GameObject;
        var cloneWrapper = Instantiate (obj) as GameObject;
        cloneWrapper.SetActive(false);
        cloneWrapper.transform.parent = globalManagerCanvas.transform;
    
        var cloneText = cloneWrapper.GetComponentInChildren<Text>();
        
        instance.StartCoroutine(MinWaitForGetSize(cloneWrapper, cloneText, item));
    }

    
    private static IEnumerator MinWaitForGetSize(GameObject cloneWrapper, Text cloneText, Message item)
    {
        
        yield return new WaitForSeconds(1.5f);
        
        var height = cloneText.GetComponent<RectTransform>().rect.height;
        var totalHeight = height + 60f + 60f; // Padding

        foreach (var popup in popupList)
        {
            var position = popup.transform.position;
            popup.transform.position = new Vector3(position.x, position.y + totalHeight + 120f, position.z);
        }
        
        instance.StartCoroutine(DisplayPopup(cloneWrapper, cloneText, item));
    }


    
    private static IEnumerator DisplayPopup(GameObject cloneWrapper, Text cloneText,  Message item)
    {

        var popupPanelImage = cloneWrapper.GetComponentsInChildren<Image>()[1];
        var popupPanelText = cloneWrapper.GetComponentInChildren<Text>();

        popupPanelImage.canvasRenderer.SetAlpha(0f);
        popupPanelText.canvasRenderer.SetAlpha(0f);
        
        cloneText.text = item.value;
        Show(cloneWrapper, popupPanelImage, popupPanelText);
        yield return new WaitForSeconds(20);
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