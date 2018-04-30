using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    private static MessageManager instance;
    
    private static GameObject popupPanel;
    private static Image popupPanelImage;
    private static Text popupPanelText;
    
    private static Text popupText;
    private static Animator popupAni;
    private static bool isAnimated;

    private void Awake() {
        instance = this;
        popupPanel = GameObject.Find("PopupPanel");
        popupText = GameObject.Find("PopupText").GetComponent<Text>();
        popupAni = GameObject.Find("PopupPanel").GetComponent<Animator>();

        popupPanelImage = popupPanel.GetComponent<Image>();
        popupPanelText = popupPanel.GetComponentInChildren<Text>();

        popupPanelImage.canvasRenderer.SetAlpha(0f);
        popupPanelText.canvasRenderer.SetAlpha(0f);
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
        instance.StartCoroutine(!isAnimated ? DisplayPopup(item) : HidePopup(item));
    }


    private static IEnumerator DisplayPopup(Message item) {
        popupText.text = item.value;
        Show();
        yield return new WaitForSeconds(3);
        Hide();
    }
    
    private static IEnumerator HidePopup(Message item) {
        Hide();
        yield return new WaitForSeconds(1);
        instance.StartCoroutine(DisplayPopup(item));
    }
    
    private static void Show()
    {
        isAnimated = true;
        popupAni.Play("popupIn");
        popupPanelImage.CrossFadeAlpha(1.0f, 1.0f, false);
        popupPanelText.CrossFadeAlpha(1.0f, 1.0f, false);
    }

    
    private static void Hide()
    {
        popupAni.Play("popupOut");
        popupPanelImage.CrossFadeAlpha(0.0f, 1.0f, false);
        popupPanelText.CrossFadeAlpha(0.0f, 1.0f, false);
        isAnimated = false;
    }
}