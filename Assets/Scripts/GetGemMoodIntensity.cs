using UnityEngine;
using Vuforia;

public class GetGemMoodIntensity : MonoBehaviour {
	private string mood; 
	private string moodName;
	private GameObject obj;
	private GameObject cloneObj;
	private GameObject targetChoice;
	public ReaderManager readerManager;

	private void Awake()
	{
		mood = readerManager.GetReaderSetting("mood");
		createGemMood();
	}
		
	void createGemMood()
	{
		switch (mood)
		{
			case "1":
				Debug.Log ("jui paisible");
				moodName = "paisible";
				createObj (moodName);
				break;
			case "2":
				Debug.Log ("jui extraordinaire");
				moodName = "extraordinaire";
				createObj (moodName);
				break;
			case "3":
				Debug.Log ("jui mysterieux");
				moodName = "mysterieux";
				createObj (moodName);
				break;
		}
	}

	void createObj(string moodName)
	{
		targetChoice = GameObject.FindWithTag("targetChoice3");
		obj = Resources.Load("Prefabs/"+moodName+"/gem/dynamic") as GameObject;
		cloneObj = Instantiate (obj);
		cloneObj.transform.parent = targetChoice.transform;
		cloneObj.transform.localScale = new Vector3(4.7f,4.72f,4.7f);
	}
		
}
