using UnityEngine;
using UnityEngine.UI;

public class ReaderManager : MonoBehaviour {
			 
	// Need cause cannot add to parameters on OnClick UI Button method
	public string parameterKey; // not visible without these properties   
	
	public void UpdateReaderSettings(int value) {
		// key list : mood / dynamic / intensity
		
		// Update in Firebase
		Router.CurrentReader().Child(parameterKey).SetValueAsync(value);
		
		// Update in GlobalManager
		var prop = GlobalManager.instance.reader.GetType().GetProperty(parameterKey);
		if (prop != null) prop.SetValue(GlobalManager.instance.reader, value, null);
		
		// Reset intensity 
		ResetIntensity();	
	}
	
	// Get reader setting by key
	public string GetReaderSetting(string key)
	{
		return GlobalManager.instance.reader.GetType().GetProperty(key).GetValue(GlobalManager.instance.reader, null).ToString();
	}
	
	private void ResetIntensity() {
		if (parameterKey != "mood") return;
		Router.CurrentReader().Child("intensity").SetValueAsync(0);
		var prop = GlobalManager.instance.reader.GetType().GetProperty("intensity");
		if (prop != null) prop.SetValue(GlobalManager.instance.reader, 0, null);
	}
}
  