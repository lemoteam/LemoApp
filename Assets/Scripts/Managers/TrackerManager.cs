using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;
using Image = UnityEngine.UI.Image;


public class TrackerManager : MonoBehaviour, ITrackableEventHandler
{
	private TrackableBehaviour mTrackableBehaviour;
	private GameObject scan;
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
		
		else if (previousStatus == TrackableBehaviour.Status.TRACKED && newStatus == TrackableBehaviour.Status.NO_POSE) {
			
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
			case "connexion":
				if (GlobalManager.instance.isLoggin) return;
				OnScan();
				break;
			
			case "A2" :
				ChangeScene("Choice1");
				break;
			
			case "B1" :
				ChangeScene("Choice2");
				break;
			
			case "C1" :
				ChangeScene("Choice3");
				break;
			
			case "S1" :
				ChangeScene("Scene1");
				break;
			
			case "S2" :
				ChangeScene("Scene2");
				break;
			
			case "S3a" :
				ChangeScene("Scene3");
				break;
			
			case "S4" :
				ChangeScene("Scene4");
				break;
			
			case "S5" :
				ChangeScene("Scene5");
				break;
			
			default:
				Debug.Log("Tracking FOUND but no specific action");
				break;
		}		 
	}
	
	
	// Tracking Lost
	private void OnTrackingLost() {
		switch (mTrackableBehaviour.TrackableName) {
			case "connexion":
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
		
		AuthManager.Instance.OnLogin(mTrackableBehaviour.TrackableName);
		scan.GetComponent<Image>().color = new Color32(0,0,0,100);
		// Show Loaded Message
		var messageManager = GameObject.FindGameObjectWithTag("messageManager");
		if (messageManager != null)
		{	
			// Show message
			messageManager.GetComponent<MessageManager>().OnScan();
		}
	}

	private void OffScan() {
		scan.GetComponent<Image>().color = new Color32(80,220,100,100);
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
