using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MessageManager : MonoBehaviour
{
    
    [System.Serializable]
    public class Popup
    {
        public string messageSlug;
        public int illuID; // Order in wrapper illu
        public float time = 5f;
        public bool onScan; // 0 => On scene load / 1 => On scan
    }

    public List<Popup> popups;
    private static MessageManager instance;
    private static GameObject globalManagerCanvas;
    private static List<GameObject> popupList = new List<GameObject>();
    private static List<GameObject> imageTargetList = new List<GameObject>();
    private static Animator animator;
    private static Transform illuSprite;
    private static Transform illuWrapper;
        
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
            StartCoroutine(WaitToShow(message.messageSlug, message.time, message.illuID));
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
                Hide(message);
            }
        }
        
        
        // If a message exist show it
        if (filteredPopup.Count > 0) {
            var message = filteredPopup[0];
            ShowMessage(message.messageSlug, message.time, message.illuID);
        }
    }

    private static IEnumerator WaitToShow(string key, float time, int illuID)
    {
        yield return new WaitForSeconds(2f);
        ShowMessage(key, time, illuID);
    }
    
    public static void ShowMessage(string key, float time, int illuID) { 
        var list = GlobalManager.instance.messageList; 
        
        foreach (var item in list) {
            if (item.id == key) {
               ActivePopup(item, time, illuID);
            }
        }
    }

    private static void ActivePopup(Message item, float time, int illuID) 
    {
        var obj = Resources.Load("Prefabs/ui/modalDialoguePanelWrapper") as GameObject;
        GameObject cloneWrapper = (GameObject)Instantiate(obj);
        
        cloneWrapper.SetActive(false);
        cloneWrapper.transform.parent = globalManagerCanvas.transform;
        cloneWrapper.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        
        var cloneTitle = cloneWrapper.GetComponentsInChildren<Text>()[0];
        var cloneText = cloneWrapper.GetComponentsInChildren<Text>()[1];
        illuWrapper = cloneWrapper.transform.GetChild(0).transform.GetChild(1);
        Transform toto;
        if (illuID != null)
        {
            illuSprite = illuWrapper.transform.GetChild(illuID);
            toto = illuWrapper.transform.GetChild(illuID);
        }
        
        instance.StartCoroutine(DisplayPopup(cloneWrapper, cloneTitle, cloneText, item, time));
    }


    private static IEnumerator DisplayPopup(GameObject cloneWrapper, Text cloneTitle, Text cloneText, Message item, float time)
    {
        
        cloneText.text = item.content ;
        cloneTitle.text = !string.IsNullOrEmpty(item.title) ? item.title : null;
        
        Show(cloneWrapper);
        yield return new WaitForSeconds(time);
        Hide(cloneWrapper);
    }

    
    private static void Show(GameObject cloneWrapper)
    {    
        var cloneWrapperRect = cloneWrapper.GetComponent<RectTransform>();
        animator = cloneWrapper.GetComponent<Animator>();
        popupList.Add(cloneWrapper);
        cloneWrapper.transform.localPosition = Vector3.zero;
        cloneWrapper.transform.localScale = Vector3.one;
        cloneWrapperRect.offsetMin = new Vector2(0, 0); 
        cloneWrapperRect.offsetMax = new Vector2(0, 0);
        cloneWrapperRect.anchorMin = new Vector2(0, 0);
        cloneWrapperRect.anchorMax = new Vector2(1, 1);
        cloneWrapperRect.pivot = new Vector2(0.5f, 0.5f);

        if (illuSprite)
        {
            illuWrapper.gameObject.SetActive(true);
            illuSprite.gameObject.SetActive(true);
        }
        else
        {
            illuWrapper.gameObject.SetActive(false);
        }
        
        cloneWrapper.SetActive(true);
        cloneWrapper.GetComponent<CanvasGroup>().alpha = 0;
        animator.Play("show");
            
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

    
    private static void Hide(GameObject cloneWrapper)
    {     
        
        animator.Play("hide");

        popupList.Remove(cloneWrapper);
        
        instance.StartCoroutine(Show3D(cloneWrapper));
    }
    
    private static IEnumerator Show3D(GameObject cloneWrapper)
    {
        yield return new WaitForSeconds(1f);
        cloneWrapper.SetActive(false);
        foreach (var item in imageTargetList) {
            if (item != null) {
                item.SetActive(true);
            }
        }
    }

}