using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetScene : MonoBehaviour {
	private string mood; 
	private string moodName;
	private int intensity;
	private GameObject obj;
	private GameObject cloneObj;
	private GameObject targetChoice;
	public ReaderManager readerManager;

	private void Awake()
	{
		mood = readerManager.GetReaderSetting("mood");
		intensity = int.Parse(readerManager.GetReaderSetting("intensity"));
		createScene();
	}
		
	void createScene()
	{
		switch (mood)
		{
			case "1":
				Debug.Log ("mytique");
				moodName = "mysterieux";
				createObj (moodName);
				break;
			case "2":
				Debug.Log ("jui extraordinaire");
				moodName = "extraordinaire";
				createObj (moodName);
				break;
			case "3":
				Debug.Log ("jui paisible");
				moodName = "paisible";
				createObj(moodName);
				break;
		}
	}

	void createObj(string moodName)
	{
		targetChoice = GameObject.FindWithTag("sceneMarker");
		obj = Resources.Load("Prefabs/"+moodName+"/scene/scene"+intensity) as GameObject;
		cloneObj = Instantiate (obj);
		// Destroy(cloneObj.GetComponent("ButtonChoice"));
		cloneObj.transform.parent = targetChoice.transform;
		cloneObj.transform.localScale = new Vector3(1f,1f,1f);
		cloneObj.transform.localPosition = new Vector3(.4f,0.04f,0f);
	}
}
