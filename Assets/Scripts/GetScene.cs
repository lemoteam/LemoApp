﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetScene : MonoBehaviour {
	
	private string mood; 
	private string moodName;
	private int intensity;
	private GameObject obj;
	private GameObject cloneObj;
	private GameObject targetChoice;

	public int IDScene;
	public ReaderManager readerManager;

	public void launchAnimation()
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
				moodName ="paisible";
				StartCoroutine(createObj(moodName));
				break;
			case "2":
				moodName = "extraordinaire";
				StartCoroutine(createObj(moodName));
				break;
			case "3":
				moodName = "mysterieux";
				StartCoroutine(createObj(moodName));
				break;
		}
	}

	private IEnumerator createObj(string moodName)
	{
		 // Load
		var operation = Resources.LoadAsync("Prefabs/"+moodName+"/scene/scene"+IDScene+"/scene"+IDScene+ "-" + intensity, typeof(GameObject));
					 
		targetChoice = GameObject.FindWithTag("targetImage");
		
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
						
			
			// Position cloneObj
			cloneObj.transform.parent = targetChoice.transform;
			cloneObj.transform.localScale = new Vector3(1f,1f,1f);
			cloneObj.transform.localPosition = new Vector3(.4f,0.04f,0f);
			cloneObj.transform.localRotation = new Quaternion(0f, 0f, 0f, 0f);
			cloneObj.SetActive(true);
		}
		
	}
	
}
