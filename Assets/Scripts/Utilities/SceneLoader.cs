﻿using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {

	public GameObject sceneContainer;
	private GameObject cFade;
	private Animator ani;
	public string previousScene;
	public GameObject prefab;
	public Text progressText;

	public void LoadScene (string sceneName)
	{
		previousScene = GlobalManager.instance.currentScene;
		GlobalManager.instance.currentScene = sceneName;
		cFade = GameObject.FindWithTag("cFade");
		ani = cFade.GetComponent<Animator>();
		
		StartCoroutine (LoadAsynchronously (sceneName));
	}


	private IEnumerator LoadAsynchronously(string sceneName)
	{
		
		ani.Play("sceneShow");
		yield return StartCoroutine (WaitBeforeLoadScene (sceneName));
		
	}
	
	
	private IEnumerator WaitBeforeLoadScene(string sceneName)
	{
		
		yield return new WaitForSeconds(.5f);
		
		StartCoroutine (NowLoadScene (sceneName));
	}
	

	
	private IEnumerator NowLoadScene(string sceneName)
	{
		
		var operation = SceneManager.LoadSceneAsync (sceneName);

		progressText.text = "";

		while (!operation.isDone) {
			Debug.Log (operation.progress);
			var progress = Mathf.Clamp01 (operation.progress / .9f);
			progressText.text = progress * 100f + "%";
			yield return null;
		}
		
		
		// Attach camera
		var globalManagerCanvas = GameObject.Find("GlobalManagerCanvas");
		globalManagerCanvas.GetComponent<Canvas>().worldCamera = Camera.main;
		
		// Show Loaded Message
		var messageManager = GameObject.FindGameObjectWithTag("messageManager");
		if (messageManager != null)
		{	
			// Show message
			messageManager.GetComponent<MessageManager>().OnLoadScene();
		}
		
		// Get Scene Text
		var textManager = GameObject.FindGameObjectWithTag("textManager");
		if (textManager != null)
		{	
			// Show message
			textManager.GetComponent<TextManager>().OnLoadScene();
		}
		
		
		StartCoroutine (ApplyScene());
	}
	
	
	private IEnumerator ApplyScene()
	{
		ani.Play("sceneHide");
		yield return null;

	}

}

