using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextManager : MonoBehaviour
{
    private static TextManager instance;
    public string pageId;
    public string code;
    private string mood;
    private string intensity;
    private string textScene;
    private GameObject textSceneMarker;
    public ReaderManager readerManager;

    private void Awake()
    {
        textSceneMarker = GameObject.FindWithTag("textSceneMarker");
        var mood = readerManager.GetReaderSetting("mood");
        var intensity = readerManager.GetReaderSetting("intensity");
        code = mood + intensity;
    }

    public void OnLoadScene()
    {
        if (mood != null && intensity != null)
        {
            GetText();
        }
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
                textSceneMarker.GetComponent<TextMeshPro>().SetText(textScene);
                //Debug.Log(version.text);
            }
        }
    }
}