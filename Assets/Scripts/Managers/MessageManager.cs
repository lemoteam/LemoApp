using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    private static MessageManager instance;
    
    private static GameObject popupPanel;
    private static Text popupText;
    private static Animator popupAni;
    private static bool isAnimated;

    private void Awake() {
        instance = this;
        popupPanel = GameObject.Find("PopupPanel").GetComponent<GameObject>();
        popupText = GameObject.Find("PopupText").GetComponent<Text>();
        popupAni = GameObject.Find("PopupPanel").GetComponent<Animator>();
    }

    public static void ShowMessage(string key) { 
        var list = GlobalManager.instance.messageList; 
        
        foreach (var item in list) {
            if (item.key == key) {
               ActivePopup(item);
            } 
        }
    }

    private static void ActivePopup(Message item) {
        if (isAnimated) return;
        isAnimated = true;
        instance.StartCoroutine(DisplayPopup(item));
    }

    private static IEnumerator DisplayPopup(Message item) {
        
        popupText.text = item.value;
        popupAni.Play("popupIn");
        yield return new WaitForSeconds(4);
        popupText.text = "back";
        popupAni.Play("popupOut");
        isAnimated = false;
    }
}