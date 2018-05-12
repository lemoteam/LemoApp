using UnityEngine;
using Vuforia;

public class ButtonChoice : MonoBehaviour, IVirtualButtonEventHandler
{
	public GameObject virtualButton;
	public ReaderManager readerManager;
	public int parameter;
	public ObjectPooler objectpooler;
	public Animation animation;

	// Use this for initialization
	void Start () {
		Debug.Log("Btn ready");
		virtualButton.GetComponent<VirtualButtonBehaviour>().RegisterEventHandler(this);
		animation = this.GetComponent<Animation>();
		
	}

	public void OnButtonPressed(VirtualButtonBehaviour vb) {
		readerManager.UpdateReaderSettings(parameter);
		
		// reset object pooler
		if (objectpooler) {
			foreach (var objPooler in GlobalManager.instance.objectPoolerList)
			{
				objPooler.isLevitate = false;
			}
			// launch new object pooler levitation
			objectpooler.isLevitate = true;
		}

		if (animation)
		{
			animation.Play();
		}
		
		Debug.Log("Btn pressed"+ parameter);
	}

	public void OnButtonReleased(VirtualButtonBehaviour vb) {
		// objectpooler.isLevitate = false;
		Debug.Log("Btn released");
	} 
	
}
