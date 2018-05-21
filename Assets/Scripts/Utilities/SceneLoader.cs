using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour {

	public GameObject sceneContainer;
	public string previousScene;
	public GameObject prefab;
	public Text progressText;

	private void Awake()
	{
		sceneContainer = GameObject.FindGameObjectWithTag("SceneContainer");
	}

	public void LoadScene (string sceneName)
	{
		previousScene = GlobalManager.instance.currentScene;
		GlobalManager.instance.currentScene = sceneName;
		
		Debug.Log("<color=blue>"+ "Function loadScene " + sceneContainer.transform.childCount +"</color>");
		
		// Active
		for (var i = 0; i < sceneContainer.transform.childCount; i++)
		{			
			var scene = sceneContainer.transform.GetChild(i);
			
			if (scene.name == sceneName)
			{
				var test = scene.transform.childCount;

				for (var j = 0; j < scene.transform.childCount; j++)
				{
					var sceneChild = scene.transform.GetChild(j);
					
					if (sceneChild.name == "GetGem")
					{
						var lol = sceneChild.gameObject;
						lol.GetComponent<GetGem>().launchAnimation();
					}

					if (sceneChild.name == "GetScene")
					{
						var lol = sceneChild.gameObject;
						lol.GetComponent<GetScene>().launchAnimation();
					}
				}			
			}
		}		
	}
}