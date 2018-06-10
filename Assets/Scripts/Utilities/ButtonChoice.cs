        using UnityEngine;
using UnityEngine.Playables;
using Vuforia;

public class ButtonChoice : MonoBehaviour, IVirtualButtonEventHandler
{
	public GameObject virtualButton;
	public ReaderManager readerManager;
	public int parameter;
	public GemManager gemManager;

	// Use this for initialization
	void Start () {
		Debug.Log("Btn ready");
		virtualButton.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);

		if (gemManager) {
			foreach (var gem in GlobalManager.instance.gemManagerList)
			{
				// Stop Animation
				gem.PlayOnlyAnimation();
			}
		}
	}

	public void OnButtonPressed(VirtualButtonBehaviour vb) {
		readerManager.UpdateReaderSettings(parameter);
		
		// If animation
		if (gemManager) {
			foreach (var gem in GlobalManager.instance.gemManagerList)
			{
				// Stop Animation
				gem.StopBase();
			}
			
			// Play Animation
			gemManager.PlayBase();
			
			// Play sound
			gemManager.playSound();
		}
	

		Debug.Log("Btn pressed"+ parameter);
	}

	public void OnButtonReleased(VirtualButtonBehaviour vb) {
		Debug.Log("Btn released");
	} 
	
}
