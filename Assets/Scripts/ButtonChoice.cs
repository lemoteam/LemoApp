using UnityEngine;
using Vuforia;

public class ButtonChoice : MonoBehaviour, IVirtualButtonEventHandler {

	public GameObject virtualButton;
	public Animator animController;
	public ReaderManager readerManager;
	public int parameter;

	// Use this for initialization
	void Start () {
		virtualButton.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
		animController.GetComponent<Animator>();
	}

	public void OnButtonPressed(VirtualButtonBehaviour vb) {
		animController.Play("sphere_animation");
		readerManager.UpdateReaderSettings(parameter);
		Debug.Log("Btn pressed"+ parameter);
	}

	public void OnButtonReleased(VirtualButtonBehaviour vb){
		animController.Play("none");
		Debug.Log("Btn released");
	} 
	
}
