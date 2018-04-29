using UnityEngine;
using Vuforia;

public class getGemMood : MonoBehaviour {
	private string mood; 
	private string moodName; 
	private GameObject obj;
	private GameObject cloneObj;
	private GameObject[] targetChoice;
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
			Debug.Log ("mytique");
			moodName = "mysterieux";
			createObj (moodName);
			break;
		case "2":
			Debug.Log ("jui bien");
			moodName = "loufoque";
			createObj (moodName);
			break;
		case "3":
			Debug.Log ("jui bien");
			moodName = "paisible";
			break;
		}
	}

	void createObj(string moodName) {
		if (targetChoice == null)
			targetChoice = GameObject.FindGameObjectsWithTag("targetChoice2");
		var index = 0;

		foreach (GameObject imgTarget in targetChoice)
		{
			index++;
			obj = Resources.Load("Prefabs/"+moodName+"/mood"+index) as GameObject;
			cloneObj = Instantiate (obj);
			ButtonChoice btnScript = cloneObj.GetComponent<ButtonChoice>();
			btnScript.parameter = index;
			btnScript.readerManager = readerManager;
			var selectBbtn = imgTarget.transform.GetChild(0);
			btnScript.virtualButton = selectBbtn.gameObject;
			cloneObj.transform.parent = imgTarget.transform;
			cloneObj.transform.localScale = new Vector3(.5f,.5f,.5f);
		}
		
	}
		
}
