using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;


public class BallScriptElement :  MonoBehaviour, ITrackableEventHandler
{
	private Rigidbody elRigidbody;
	private Vector3 NewGravitySettings;
	private bool isReady = false;
	private GameObject character;
	private GameObject spawn;
	private Vector3 position;
	
	private TrackableBehaviour mTrackableBehaviour;

	void Start() {
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		
		character = GameObject.FindGameObjectWithTag("character");
		spawn = GameObject.FindGameObjectWithTag("characterSpawn");
		elRigidbody = character.GetComponent<Rigidbody>();
		elRigidbody.isKinematic = true;
		
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
		
		else if (previousStatus == TrackableBehaviour.Status.TRACKED && newStatus == TrackableBehaviour.Status.NO_POSE) {
			
			Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
			OnTrackingLost();
		}
	}
	

	private IEnumerator addGravity()
	{
		yield return new WaitForSeconds(1.5f);
		elRigidbody.isKinematic = false;
		NewGravitySettings = new Vector3(0f, -30f, 0f); // Physics.gravity
	}
	
	// Tracking Found
	private void OnTrackingFound() {
		StartCoroutine(addGravity());
	}
	
	// Tracking Lost
	private void OnTrackingLost() {
		elRigidbody.isKinematic = true;
		character.transform.position = spawn.transform.position;
	}
}
