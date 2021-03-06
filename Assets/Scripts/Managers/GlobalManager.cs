﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Vuforia;

public class GlobalManager : MonoBehaviour {

	public static GlobalManager instance = null;
	public DatabaseManager database;     
	public SceneLoader sceneLoader;
	public GameObject prefabContainer;
	
	protected internal Reader reader = null;
	protected internal bool dynamicHasChanded = false;
	protected internal bool isActiveMood = false;
	protected internal bool isActiveIntensity = false;
	protected internal bool isActiveDynamism = false;
	protected internal string currentReaderUid = null;
	protected internal bool isLoggin = false;
	protected internal string currentScene = "";
	protected internal List<Message> messageList = new List<Message>();
	protected internal List<Page> pageList = new List<Page>();
	protected internal List<GemManager> gemManagerList = new List<GemManager>();

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
		//messageManager = GetComponent<MessageManager> ();
		
		//Clear Message List
		messageList.Clear();

		// Set active camera on canvas
		GetComponentInChildren<Canvas>().worldCamera = Camera.main;
		
		//Call the InitGame function to initialize the database
		InitDatabase ();
	}
		
	// Do what you want when vuforia is load 	
	private static void OnVuforiaStarted(){
		GetNewGameObject();
	}

	private static void GetNewGameObject() {
		var vuforiaObj = GameObject.FindObjectsOfType<Transform>().Where(go => go.name == "New Game Object").ToList();
		var targetChoice = GameObject.FindGameObjectsWithTag("targetChoice");
		var targetImage = GameObject.FindGameObjectsWithTag("targetImage");
		
		foreach (var item in targetChoice) {
			item.gameObject.AddComponent<TrackerManager> ();
		}
		
		foreach (var item in targetImage) {
			item.gameObject.AddComponent<TrackerManager> ();
		}
		
		foreach (var item in vuforiaObj) {
			item.gameObject.AddComponent<TrackerManager> ();
		}
	}

	private void InitDatabase() {
		var vuforia = VuforiaARController.Instance;
		// Init Database
		DatabaseManager.InitDatabase();
		// Init Vuforia
		vuforia.RegisterVuforiaStartedCallback(OnVuforiaStarted);
		// Get Messages
		DatabaseManager.GetMessages();
		// Get Messages
		DatabaseManager.GetTexts();
        // Start coroutine
        StartCoroutine(MinWaitForLogoAnimation());
	}

	private IEnumerator MinWaitForLogoAnimation()
	{
		yield return new WaitForSeconds(3f);
        sceneLoader.LoadScene("Main");
	}
}
