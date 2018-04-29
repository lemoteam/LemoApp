using UnityEngine;
using UnityEngine.UI;

public class ReaderManager : MonoBehaviour {
	
	Firebase.Auth.FirebaseAuth auth;
	
	// UI objects linked from the inspector
	public Text userEmail;
	public Text userValue;

	private void Awake()
	{		
		UpdateEmail("L'utilisateur connecté est " + GlobalManager.instance.reader.email);
	}
	 
	// Need cause cannot add to parameters on OnClick UI Button method
	public string parameterKey; // not visible without these properties   

	
	public void UpdateReaderSettings(int value) {
		// key list : mood / dynamic / intensity
		
		// Update in Firebase
		Router.CurrentReader().Child(parameterKey).SetValueAsync(value);
		
		// Update in GlobalManager
		var prop = GlobalManager.instance.reader.GetType().GetProperty(parameterKey);
		if (prop != null) prop.SetValue(GlobalManager.instance.reader, value, null);
		
		// Print value
		UpdateValue(parameterKey.ToString() + " est le " + GetReaderSetting(parameterKey));

	}
	
	// Get reader setting by key
	public string GetReaderSetting(string key)
	{
		return GlobalManager.instance.reader.GetType().GetProperty(key).GetValue(GlobalManager.instance.reader, null).ToString();
	}
	
	// Utilities
	private void UpdateEmail(string message){
		userEmail.text = message;
	}
	
	private void UpdateValue(string message) {
		userValue.text = message;
	}
}
  