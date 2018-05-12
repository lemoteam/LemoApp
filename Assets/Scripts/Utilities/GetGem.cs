using System.Collections;
using System.Reflection.Emit;
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
        getGem(currentParameter.ToLower(), currentSettingName.ToLower());
    }
		
    
    // Set Names
    void setCurrentSettingName(string currentSetting)
    {
        switch (currentSetting)
        {
            case "1":
                currentSettingName = "mysterieux";
                break;
            case "2":
                currentSettingName = "loufoque";
                break;
            case "3":
                currentSettingName = "paisible";
                break;
        }
    }
    
    
    // Get gem -> currentParameter (mood,intensity) / currentSettingName(mysterieux,...)

    void getGem(string currentParameter, string currentSettingName) {
        
        // Variables
        var loadUrl = "Prefabs/" + currentSettingName + "/" + currentParameter;
        var targetChoice = GameObject.FindGameObjectsWithTag("targetChoice");
        var index = 1;
        
        
        foreach (GameObject imgTarget in targetChoice) {
            
            // Load
            var obj = Resources.Load(loadUrl + index) as GameObject;
            
            // Instance
            var cloneObj = Instantiate (obj);
            
            ButtonChoice btnScript = cloneObj.GetComponent<ButtonChoice>();
            btnScript.parameter = index;
            btnScript.readerManager = readerManager;
            
            var selectBbtn = imgTarget.transform.GetChild(0);
            btnScript.virtualButton = selectBbtn.gameObject;
            
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
