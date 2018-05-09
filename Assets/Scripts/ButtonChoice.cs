using UnityEngine;
using Vuforia;

public class ButtonChoice : MonoBehaviour, IVirtualButtonEventHandler {

	public GameObject virtualButton;
	public ReaderManager readerManager;
	public int parameter;
	public ObjectPooler objectpooler;

	// Use this for initialization
	void Start () {
		virtualButton.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
	}

	public void OnButtonPressed(VirtualButtonBehaviour vb) {
		readerManager.UpdateReaderSettings(parameter);
		
		// reset object pooler
		if (objectpooler)
		{
			foreach (var objPooler in GlobalManager.instance.objectPoolerList)
			{
				objPooler.isLevitate = false;
			}
			// launch new object pooler levitation
			objectpooler.isLevitate = true;
		}
		
		Debug.Log("Btn pressed"+ parameter);
	}

	public void OnButtonReleased(VirtualButtonBehaviour vb)
	{
		// objectpooler.isLevitate = false;
		Debug.Log("Btn released");
	} 
	
}
