using System.Collections;
using UnityEngine;

public class GetGem : MonoBehaviour
{

    private string currentSettingInt;
    private string currentParameter;
    private string currentSettingName;
    public string stringPreviousSetting; // Previous settings mood or intensity 'cause dynamic is the last setting
    public ReaderManager readerManager;
	

    private void Awake()
    {
        var previousSetting = stringPreviousSetting.ToLower();
        
        
        switch (previousSetting)
        {
            case "mood":
                currentSettingInt = readerManager.GetReaderSetting("mood");
                currentParameter = "intensity";
                break;
            case "intensity":
                currentSettingInt = readerManager.GetReaderSetting("intensity");
                currentParameter = "dynamic";
                break;
        }

        setCurrentSettingName(currentSettingInt);
        getCurrentGems(currentParameter.ToLower(), currentSettingName.ToLower());
    }
		
    
    // Set Names
    void setCurrentSettingName(string currentSetting)
    {
        switch (currentSetting)
        {
            case "1":
                currentSettingName = "paisible";
                break;
            case "2":
                currentSettingName = "extraordinaire";
                break;
            case "3":
                currentSettingName = "mysterieux";
                break;
        }
    }
    
    
    // Get gem -> currentParameter (mood,    intensity) / currentSettingName(mysterieux,...)

    void getCurrentGems(string currentParameter, string currentSettingName) {
        
        // Variables
        var loadUrl = "Prefabs/" + currentSettingName + "/gem/" + currentParameter;
        var targetChoice = GameObject.FindGameObjectsWithTag("targetChoice");
        var index = 1;
        
        Debug.Log(loadUrl);
        
        
        foreach (GameObject imgTarget in targetChoice) {
            
            // Load
            var obj = Resources.Load(loadUrl + index) as GameObject;
            
            // Instance
            var cloneObj = Instantiate (obj);
            
            // Btn Script
            var selectBbtn = imgTarget.transform.GetChild(0);
            var btnScript = cloneObj.GetComponent<ButtonChoice>();
            btnScript.parameter = index;
            btnScript.readerManager = readerManager;
            btnScript.virtualButton = selectBbtn.gameObject;
            
            // Gem Manager
            var gemManagerObj = imgTarget.transform.GetChild(1);
            var gemManager = gemManagerObj.GetComponent<GemManager>();
            gemManager.Gem = cloneObj;
            
            // Attach gem manager to cloneObj
            btnScript.gemManager = gemManager;
            
            // Position cloneObj
            cloneObj.transform.parent = imgTarget.transform;
            cloneObj.transform.localScale = new Vector3(4.7f,4.7f,4.7f);
			
            StartCoroutine(SetPosition(cloneObj));
            
            // Increment
            index++;
        }

    }
	
    private static IEnumerator SetPosition(GameObject cloneObj)
    {
        yield return new WaitForSeconds(1.5f);
        cloneObj.transform.localPosition = new Vector3(0f, 0.52f, 0.18f);
    }
}
