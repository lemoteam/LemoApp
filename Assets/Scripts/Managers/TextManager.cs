using System;
using System.Collections.Generic;
using UnityEngine;

public class TextManager : MonoBehaviour
{
    private static TextManager instance;
    public string pageId;
    public string code;
    private string textScene;
    private GameObject textSceneMarker;

    private void Start()
    {
        textSceneMarker = GameObject.FindWithTag("textSceneMarker");
    }

    public void OnLoadScene()
    {
        GetText();
    }
    
    private void GetText() { 
        var list = GlobalManager.instance.pageList;  
        foreach (var item in list) {
           // Debug.Log(item);
            if (item.pageID == pageId)
            {
                var versions = item.versions;
                GetCurrentSceneText(versions);
            }
        }
    }

    void GetCurrentSceneText(List<Version> versions)
    {
        foreach (var version in versions) {
            // Debug.Log(version);
            
            if (version.code == code)
            {
                textScene = version.text;
                //Debug.Log(version.text);
            }
        }
    }
}