using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour {

	public static GlobalManager instance = null;
	public DatabaseManager database;     
	public SceneLoader sceneLoader;
	
	protected internal Reader reader = null;
	protected internal string currentReaderUid = null;

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

	private void InitDatabase() {
		DatabaseManager.InitDatabase();
		StartCoroutine(MinWaitForLogoAnimation());
	}

	private IEnumerator MinWaitForLogoAnimation()
	{
		yield return new WaitForSeconds(1.5f);
		sceneLoader.LoadScene ("Main");
	}
}
