using UnityEngine;
using Vuforia;
using UnityEngine.SceneManagement;

public class LoadSceneOnDetection : MonoBehaviour,
	ITrackableEventHandler
{

	private TrackableBehaviour mTrackableBehaviour;

	void Start()
	{
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
		{
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		}
	}

	public void OnTrackableStateChanged(
		TrackableBehaviour.Status previousStatus,
		TrackableBehaviour.Status newStatus)
	{
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
		    newStatus == TrackableBehaviour.Status.TRACKED ||
		    newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
		{
			OnTrackingFound();
		}
	}

	private void OnTrackingFound()
	{

		Debug.Log("Custom Trackable " + mTrackableBehaviour.TrackableName + " found");

		switch( mTrackableBehaviour.TrackableName ){
			case "example_5-star_grayscale" :
				Debug.Log( "load detection scene");
				// GlobalManager.instance.sceneLoader.LoadScene ("InGame");
				// SceneManager.LoadScene( "sphereScene" );
				break;

			case "qrcode" :
				Debug.Log( "load cube scene");
				// SceneManager.LoadScene( "cubeScene" );
				break;
		}

	}			
}    

