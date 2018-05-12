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
		gemManager.animation = this.GetComponent<Animation>();
	}

	public void OnButtonPressed(VirtualButtonBehaviour vb) {
		readerManager.UpdateReaderSettings(parameter);
		
		// If animation
		if (gemManager) {
			foreach (var gem in GlobalManager.instance.gemManagerList)
			{
				// Stop Animation
				gem.StopAnimation();
			}
			
			// Play Animation
			gemManager.PlayAnimation();
		}
		
		
		
		Debug.Log("Btn pressed"+ parameter);
	}

	public void OnButtonReleased(VirtualButtonBehaviour vb) {
		Debug.Log("Btn released");
	} 
	
}
