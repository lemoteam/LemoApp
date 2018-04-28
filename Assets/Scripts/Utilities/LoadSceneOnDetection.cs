using UnityEngine;
using Vuforia;
using UnityEngine.SceneManagement;

public class LoadSceneOnDetection : MonoBehaviour,
	ITrackableEventHandler
{

	private TrackableBehaviour mTrackableBehaviour;
	private string currentScene;

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
		getActiveSceneName();
		switch( mTrackableBehaviour.TrackableName )  {
			case "virtualbutton" :
				var sceneName = "Choice1";
				if (GlobalManager.instance.isLoggin && currentScene != sceneName)
				{
					Debug.Log( "load detection scene");
					GlobalManager.instance.sceneLoader.LoadScene (sceneName);
				}
				break;

			case "qrcode" :
				var sceneNameMain = "Main";
				if (currentScene != sceneNameMain)
				{
					Debug.Log( "load couverture scene");
					GlobalManager.instance.sceneLoader.LoadScene (sceneNameMain);
				}
				break;
			
			case "example_5-star_grayscale" :
				var sceneNameChoice2 = "Choice2";
				if (GlobalManager.instance.isLoggin && currentScene != sceneNameChoice2)
				{
					Debug.Log( "load scene choix 2");
					GlobalManager.instance.sceneLoader.LoadScene (sceneNameChoice2);
				}
				break;
		}

	}	
	
	
	private void getActiveSceneName()
	{
		var scene = SceneManager.GetActiveScene();
		currentScene = scene.name;
	}
	
}    

