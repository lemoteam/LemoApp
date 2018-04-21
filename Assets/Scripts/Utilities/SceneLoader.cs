using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {

	public GameObject loadingScene;
	public Text progressText;

	public void LoadScene (string sceneName) {
		
		StartCoroutine (LoadAsynchronously (sceneName));
	}

	IEnumerator LoadAsynchronously(string sceneName){
		AsyncOperation operation = SceneManager.LoadSceneAsync (sceneName);

		progressText.text = "";
		loadingScene.SetActive(true);

		while (!operation.isDone) {
			Debug.Log (operation.progress);
			float progress = Mathf.Clamp01 (operation.progress / .9f);
			progressText.text = progress * 100f + "%";
			yield return null;
			loadingScene.SetActive(false);
		}
	} 
}
