using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {

	public GameObject sceneContainer;
	public string previousScene;
	public GameObject prefab;
	public Text progressText;

	public void LoadScene (string sceneName)
	{
		previousScene = GlobalManager.instance.currentScene;
		GlobalManager.instance.currentScene = sceneName;
		
		StartCoroutine (LoadAsynchronously (sceneName));
	}


	private IEnumerator LoadAsynchronously(string sceneName){
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
	} 
}

