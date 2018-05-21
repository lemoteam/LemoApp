﻿using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;
using Image = UnityEngine.UI.Image;


public class TrackerManager : MonoBehaviour, ITrackableEventHandler
{
	private TrackableBehaviour mTrackableBehaviour;
	private GameObject scan;
	private GameObject text;
	private string currentScene;

	void Start() {
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		
		// Trackable Behaviour
		if (mTrackableBehaviour)
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		
	}

	
	public void OnTrackableStateChanged( TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus) {
		
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
			newStatus == TrackableBehaviour.Status.TRACKED ||
			newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
			
			Debug.Log("Custom Trackable " + mTrackableBehaviour.TrackableName + " found");
			OnTrackingFound();
		}
		
		else if (previousStatus == TrackableBehaviour.Status.TRACKED && newStatus == TrackableBehaviour.Status.NOT_FOUND) {
			
			Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
			OnTrackingLost();
		}
	}
	
	
	// Tracking Found
	private void OnTrackingFound() {
		
		GetActiveSceneName();

		if (GlobalManager.instance.dynamicHasChanded)
		{	
			// Persist to firebase
			Router.CurrentReader().Child("dynamic").SetValueAsync(GlobalManager.instance.reader.dynamic);
			GlobalManager.instance.dynamicHasChanded = false;
		}
				
		switch (mTrackableBehaviour.TrackableName) {
			case "qrcode":
				if (GlobalManager.instance.isLoggin) return;
				OnScan();
				break;
			
			case "drone-btn" :
				MessageManager.ShowMessage("scanChoice1", 4f);
				ChangeScene("Choice1");
				MessageManager.ShowMessage("scanAfter", 4f);
				break;
			
			case "astronaut-btn" :
				ChangeScene("Choice2");
				break;
			
			case "image" :
				MessageManager.ShowMessage("scanChoice3", 3f);
				ChangeScene("Choice3");
				break;
			
			case "galet" :
				MessageManager.ShowMessage("scanScene1", 3f);
				ChangeScene("Scene1");
				break;
			
			default:
				Debug.Log("Tracking FOUND but no specific action");
				break;
		}		 
	}
	
	
	// Tracking Lost
	private void OnTrackingLost() {
		switch (mTrackableBehaviour.TrackableName) {
			case "qrcode":
				OffScan();
				break;
			
			default:
				Debug.Log("Tracking LOST but no specific action");
				break;
		}
	}
	
	
	///////////////
	// Methods
	///////////////
	
	// Scan
	private void OnScan() {
		
		scan = GameObject.FindWithTag("scan");
		text = GameObject.FindWithTag("text");
		
		AuthManager.Instance.OnLogin(mTrackableBehaviour.TrackableName);
		scan.GetComponent<Image>().color = new Color32(0,0,0,100);
		//text.SetActive(false);

		MessageManager.ShowMessage("scan", 2f);
	}

	private void OffScan() {
		scan.GetComponent<Image>().color = new Color32(255,255,255,100);
	}
	
	
	// Scenes
	private void GetActiveSceneName() {
		currentScene = GlobalManager.instance.currentScene;
		Debug.Log("<color=yellow>LA scene courante est : "+ currentScene +"</color>");
	}

	private void ChangeScene(string sceneName) {
		Debug.Log("<color=purple>"+ sceneName +"</color>");
		if (!GlobalManager.instance.isLoggin || currentScene == sceneName) return;
		Debug.Log("<color=purple>"+ "LOAD SCENE" +"</color>");
		GlobalManager.instance.sceneLoader.LoadScene (sceneName);
	}
	
}
