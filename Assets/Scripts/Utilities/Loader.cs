using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {
	
	public GameObject globalManager; //GlobalManager prefab to instantiate.

	private void Awake (){
		
		//Check if a GlobalManager has already been assigned to static variable GlobalManager.instance or if it's still null
		if (GlobalManager.instance == null){
			//Instantiate globalManager prefab
			Instantiate(globalManager);
		}
	}
}
