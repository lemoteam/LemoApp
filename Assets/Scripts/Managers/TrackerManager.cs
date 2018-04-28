﻿using UnityEngine;
using Vuforia;
using Image = UnityEngine.UI.Image;


public class TrackerManager : MonoBehaviour, ITrackableEventHandler
{
	protected TrackableBehaviour mTrackableBehaviour;
	private GameObject scan;
	private GameObject text;

	protected virtual void Start() {
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
			onTrackingLost();
		}
	}
	
	
	// Tracking Found
	private void OnTrackingFound() {
		
		switch (mTrackableBehaviour.TrackableName) {
			case "qrcode":
				if (GlobalManager.instance.isLoggin) return;
				onScan();
				break;
			
			default:
				Debug.Log("Tracking FOUND but no specific action");
				break;
		}		 
	}
	
	
	// Tracking Lost
	private void onTrackingLost() {
		switch (mTrackableBehaviour.TrackableName) {
			case "qrcode":
				offScan();
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
	private void onScan() {
		
		scan = GameObject.FindWithTag("scan");
		text = GameObject.FindWithTag("text");
		
		AuthManager.Instance.OnLogin(mTrackableBehaviour.TrackableName);
		scan.GetComponent<Image>().color = new Color32(0,0,0,100);
		text.SetActive(false);
	}

	private void offScan() {
		scan.GetComponent<Image>().color = new Color32(255,255,255,100);
	}
	
	
}
