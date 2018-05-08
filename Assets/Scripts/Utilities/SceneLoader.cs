using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {

	public GameObject loadingScene;
	public Text progressText;

	public void LoadScene (string sceneName) {
		
		StartCoroutine (LoadAsynchronously (sceneName));
	}

	private IEnumerator LoadAsynchronously(string sceneName){
		var operation = SceneManager.LoadSceneAsync (sceneName);

		progressText.text = "";
		//loadingScene.SetActive(true);

		while (!operation.isDone) {
			Debug.Log (operation.progress);
			var progress = Mathf.Clamp01 (operation.progress / .9f);
			progressText.text = progress * 100f + "%";
			yield return null;
			//loadingScene.SetActive(false);
			var transformPosition = GlobalManager.instance.camera.transform.position;
			transformPosition.x = 0f;
			transformPosition.y = 0f;
			transformPosition.z = 0f;
		}
	} 
}
