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
	void Start ()
	{
		Init();
	}


	private void OnEnable()
	{
		Init();
	}

	private void Init()
	{
		sceneMarker = GameObject.FindWithTag("sceneMarker");
		soundTarget = GetComponent<AudioSource>();
		arCamera = GameObject.Find ("ARCamera"); 
		Debug.Log ("camera : " + arCamera);
		soundTarget.loop = true;
		Debug.Log ("audio volume : " + soundTarget.volume);
	}
	
	// Update is called once per frame
	void Update () {
		
		//if (!tree)
		//{
			var distance = Vector3.Distance(arCamera.transform.position, transform.position);
			//var Len = distance.magnitude;
			//Debug.Log ("dist :" + distance);
			//Debug.Log ("dist :" + distance);
			//float volumeMap = Map(1.0f,0.02f,900.0f,1200.0f,Len);
			//float volumeMap = Map(1.0f,0.02f,1200f,2200f,Len);
			float volumeMap = Map(1.0f,0.02f,1f,2.5f,distance);
			soundTarget.volume = volumeMap;
		//}
	}
}
