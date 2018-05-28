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
		tree = GameObject.FindWithTag("tree");
		arCamera = GameObject.Find ("ARCamera"); 
		Debug.Log ("camera : " + arCamera);
		soundTarget.loop = true;
		Debug.Log ("audio volume : " + soundTarget.volume);
	}

	// Update is called once per frame
	void Update () {
		if (tree)
		{
			Vector3 distance = arCamera.transform.position - tree.transform.position;
			float Len = (float) distance.magnitude;
			// flot volume = Debug.Log("distance map : " + Map(0.050f,1.0f,0.05f,2000.0f,Len));	
			float volumeMap = Map(1.0f,0.02f,900.0f,1200.0f,Len);
			soundTarget.volume = volumeMap;
		}
	
	}
}
