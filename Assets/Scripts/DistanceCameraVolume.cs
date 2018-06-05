using UnityEngine;

public class DistanceCameraVolume : MonoBehaviour {

	private GameObject arCamera; 
	private AudioSource soundTarget;
	private GameObject sceneMarker;
	private GameObject tree;

	public float Map(float from, float to, float from2, float to2, float value) {
		if(value <= from2) {
			return from;
		} else if(value >= to2) {
			return to;
		} else {
			return (to - from) * ((value - from2) / (to2 - from2)) + from;
		}
	}

	// Use this for initialization
	void Start () {
		sceneMarker = GameObject.FindWithTag("sceneMarker");
		soundTarget = sceneMarker.GetComponent<AudioSource>();
		arCamera = GameObject.Find ("ARCamera"); 
		Debug.Log ("camera : " + arCamera);
		soundTarget.loop = true;
		Debug.Log ("audio volume : " + soundTarget.volume);
	}

	// Update is called once per frame
	void Update () {
		
		if (!tree)
		{
			tree = GameObject.FindWithTag("tree");
		}
		else
		{
			Vector3 distance = arCamera.transform.position - sceneMarker.transform.position;
			var Len = distance.magnitude;
			//Debug.Log ("dist :" + distance);
			//Debug.Log ("len :" + Len);
			//float volumeMap = Map(1.0f,0.02f,900.0f,1200.0f,Len);
			float volumeMap = Map(1.0f,0.02f,1200f,2200f,Len);
			soundTarget.volume = volumeMap;
		}
	
	}
}
