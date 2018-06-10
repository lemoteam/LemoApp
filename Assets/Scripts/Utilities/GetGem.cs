using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGem : MonoBehaviour
{
    private string currentSettingInt;
    private string currentParameter;
    private string currentSettingName;
    public string stringPreviousSetting; // Previous settings mood or intensity 'cause dynamic is the last setting
    public ReaderManager readerManager;
    private List<GameObject> targetChoice = new List<GameObject>();

    public void Start()
    {
        var previousSetting = stringPreviousSetting.ToLower();
        
        switch (previousSetting)
        {
            case "start":
                currentSettingInt = "0";
                currentParameter = "mood";
                break;
            case "mood":
                currentSettingInt = readerManager.GetReaderSetting("mood");
                currentParameter = "intensity";
                break;
            case "intensity":
                currentSettingInt = readerManager.GetReaderSetting("mood");
                currentParameter = "dynamic";
                break;
        }

        setCurrentSettingName(currentSettingInt);
        
        var cloneObjList = GameObject.FindGameObjectsWithTag("cloneGem");

        if (cloneObjList.Length > 0)
        {
            foreach (var cloneObj in cloneObjList)
            {
                cloneObj.SetActive(false);
                DestroyImmediate(cloneObj, true);
            }  
        }

        StartCoroutine(currentParameter.ToLower() == "dynamic"
            ? getCurrentGems("Prefabs/" + currentSettingName.ToLower() + "/gem/" + currentParameter.ToLower(), true)
            : getCurrentGems("Prefabs/" + currentSettingName.ToLower() + "/gem/" + currentParameter.ToLower(), false));
    }
    
    // Set Names
    void setCurrentSettingName(string currentSetting)
    {
        switch (currentSetting)
        {
            case "0":
                currentSettingName = "common";
                break;
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
    
    
    // Get gem -> currentParameter (mood, intensity) / currentSettingName(mysterieux,...)

    private IEnumerator getCurrentGems(string loadUrl, bool isDynamic) {
        
        // Variables
        for (var i = 0; i < transform.parent.childCount; i++)
        {
            var gameObj = transform.parent.GetChild(i).gameObject;
            
            if (gameObj.CompareTag("targetChoice"))
            {
                targetChoice.Add(gameObj);
            }
        }
        
        var index = 1;

        foreach (var imgTarget in targetChoice) {
            
            // Load
            var operation = isDynamic 
                ? Resources.LoadAsync(loadUrl, typeof(GameObject)) 
                : Resources.LoadAsync(loadUrl + index, typeof(GameObject));
                         
            GlobalManager.instance.sceneLoader.progressText.text = "";
            
            while (!operation.isDone) {
                Debug.Log (operation.progress);
                var progress = Mathf.Clamp01 (operation.progress / .9f);
                GlobalManager.instance.sceneLoader.progressText.text = progress * 100f + "%";;
                yield return operation;
            }
		
            // Get the reference to the loaded object
            GameObject obj = operation.asset as GameObject;

            // Instance
            if (obj) {
                var cloneObj = Instantiate (obj);
                cloneObj.gameObject.tag = "cloneGem"; 
                cloneObj.SetActive(false);
                            
                // Gem Manager
                var gemManagerObj = imgTarget.transform.GetChild(1);
                var gemManager = gemManagerObj.GetComponent<GemManager>();
                gemManager.Gem = cloneObj;
                
                if(gemManager.Gem.GetComponent<Animator>()){
                    gemManager.animator = gemManager.Gem.GetComponent<Animator>();
                    gemManager.animatorSlug = gemManager.animator.runtimeAnimatorController.name;
                }
                
                // Btn Script
                var selectBbtn = imgTarget.transform.GetChild(0);
                if (selectBbtn.gameObject.name == "selectBtn") {
                    var btnScript = cloneObj.GetComponent<ButtonChoice>();
                    btnScript.parameter = index;
                    btnScript.readerManager = readerManager;
                    btnScript.virtualButton = selectBbtn.gameObject;
                    
                    // Attach gem manager to cloneObj
                    btnScript.gemManager = gemManager;
                }
                
                
                // Gembase  
                for (var i = 0; i < cloneObj.transform.childCount; i++)
                {
                    var gameObj = cloneObj.transform.GetChild(i).gameObject;
            
                    if (gameObj.CompareTag("gemBase"))
                    {
                        var gembase = gameObj;
                        var gembaseParticles = gembase.transform.GetChild(0).GetComponent<ParticleSystem>(); // GembaseParticles first
                        var gembaseBase = gembase.transform.GetChild(1).GetComponent<ParticleSystem>(); // Then GembaseBase
                        gemManager.baseParticle = gembaseBase;
                        gemManager.particles = gembaseParticles;
                    }
                }
                
                
                // Position cloneObj
                cloneObj.transform.parent = imgTarget.transform;
                cloneObj.transform.localScale = new Vector3(4.1f,4.1f,4.1f);
                
                cloneObj.transform.localPosition = new Vector3(0f, 0.52f, 0.18f);
                cloneObj.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
                cloneObj.SetActive(true);
            }
            
            // Increment
            index++;
        }
    }
}

