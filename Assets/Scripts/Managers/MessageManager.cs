using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    private static GameObject popupPanel;
    private static Text popupText;

    private void Awake() {
        popupPanel = GameObject.Find("PopupPanel").GetComponent<GameObject>();
        popupText = GameObject.Find("PopupText").GetComponent<Text>();
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
        popupText.text = item.value;
    }
}