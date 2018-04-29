using UnityEngine;
using Vuforia;

public class GetGemMoodIntensity : MonoBehaviour {
	private string mood; 
	private string moodName;
	private int intensity;
	private GameObject obj;
	private GameObject cloneObj;
	private GameObject targetChoice;
	public ReaderManager readerManager;

	private void Awake()
	{
		mood = readerManager.GetReaderSetting("mood");
		intensity = int.Parse(readerManager.GetReaderSetting("intensity"));
		createGemMood();
	}
		
	void createGemMood()
	{
		switch (mood)
		{
			case "1":
				Debug.Log ("mytique");
				moodName = "mysterieux";
				createObj (moodName);
				break;
			case "2":
				Debug.Log ("jui loufoque");
				moodName = "loufoque";
				createObj (moodName);
				break;
			case "3":
				Debug.Log ("jui paisible");
				moodName = "paisible";
				break;
		}
	}

	void createObj(string moodName)
	{
		targetChoice = GameObject.FindWithTag("targetChoice3");
		obj = Resources.Load("Prefabs/"+moodName+"/mood"+intensity) as GameObject;
		cloneObj = Instantiate (obj);
		cloneObj.transform.parent = targetChoice.transform;
		cloneObj.transform.localScale = new Vector3(.5f,.5f,.5f);
	}
		
}
