using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Vuforia;

public class GlobalManager : MonoBehaviour {

	public static GlobalManager instance = null;
	public DatabaseManager database;     
	public SceneLoader sceneLoader;
	
	protected internal Reader reader = null;
	protected internal string currentReaderUid = null;
	protected internal bool isLoggin = false;

	public void Awake() {

		//Check if instance already exists
		if (instance == null) {
			//if not, set instance to this
			instance = this;
		//If instance already exists and it's not this:
		} else if (instance != this) {
			//Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
			Destroy (gameObject);    
		}		
			
		//Sets this to not be destroyed when reloading scene
		DontDestroyOnLoad(gameObject);

		//Get a component reference to the attached DatabaseManager
		database = GetComponent<DatabaseManager>();
		sceneLoader = GetComponent<SceneLoader> ();
		
		//Call the InitGame function to initialize the database
		InitDatabase ();
	}
		
	// Do what you want when vuforia is load 	
	private void OnVuforiaStarted(){
		GetNewGameObject();
	}
	void GetNewGameObject() {
		var obj = GameObject.FindObjectsOfType<Transform>().Where(go => go.name == "New Game Object").ToList();
		foreach (var item in obj) {
			item.gameObject.AddComponent<LoadSceneOnDetection> ();
		}
	}

	private void InitDatabase() {
		var vuforia = VuforiaARController.Instance;
		DatabaseManager.InitDatabase();
		vuforia.RegisterVuforiaStartedCallback(OnVuforiaStarted);
		StartCoroutine(MinWaitForLogoAnimation());
	}

	private IEnumerator MinWaitForLogoAnimation()
	{
		yield return new WaitForSeconds(1.5f);
		sceneLoader.LoadScene ("Main");
	}
}
