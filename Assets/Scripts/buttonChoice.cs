using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class buttonChoice : MonoBehaviour, IVirtualButtonEventHandler {

	public GameObject virtualButton;
	public Animator animController;
	public ReaderManager readerManager;
	public int parameterKey;

	// Use this for initialization
	void Start () {
		virtualButton.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
		animController.GetComponent<Animator>();
	}

	public void OnButtonPressed(VirtualButtonBehaviour vb) {
		animController.Play("sphere_animation");
		readerManager.UpdateReaderSettings(parameterKey);
		Debug.Log("Btn pressed");
	}

	public void OnButtonReleased(VirtualButtonBehaviour vb){
		animController.Play("none");
		Debug.Log("Btn released");
	} 
}
