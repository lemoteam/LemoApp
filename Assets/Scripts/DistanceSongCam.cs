using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceSongCam : MonoBehaviour
{
	private GameObject catiche;
	private GameObject buisson;
	private GameObject emmi;
	private GameObject refSound;
	private GameObject[] players;
	private Camera camera;
	private float previousIndex = -1;
	private float minimum ;
	private AudioSource audioSource;
	private AudioClip audioClip;
	private bool isPlayin;
	private bool isLerpin;
	static float t = 0.0f;

	// Use this for initialization
	void Start () {
		buisson = GameObject.FindWithTag("buisson");
		catiche = GameObject.Find("RefCatiche");
		emmi = GameObject.Find("RefEmmi");
		refSound = GameObject.Find("RefSound");
		camera = Camera.main;
		players = GameObject.FindGameObjectsWithTag("Player");
		audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (buisson.GetComponent<MeshRenderer>().enabled)
		{
			//minimum = float.MaxValue;
			var emmiDist = Vector3.Distance(camera.transform.position, emmi.transform.position);
			var caticheDist = Vector3.Distance(camera.transform.position, catiche.transform.position);

			if (emmiDist < caticheDist)
			{
				
				Debug.Log("<color=green> Coucou : emmi "+"</color>");
				var distance = Vector3.Distance(camera.transform.position, refSound.transform.position);
				float volumeMap = Map(0.2f,.05f,1f,2.5f, distance);
				audioSource.volume = volumeMap;
				isPlayin = false;
				
			}
			else
			{
		
				Debug.Log("<color=green> Coucou : catiche "+"</color>");
				var distance = Vector3.Distance(camera.transform.position, refSound.transform.position);
				float volumeMap = Map(1.0f,0.2f,1f,2.5f, distance);
				audioSource.volume = volumeMap;
			}
		}
	}
	
	
	public float Map(float from, float to, float from2, float to2, float value) {
		if(value <= from2) {
			return from;
		} else if(value >= to2) {
			return to;
		} else {
			return (to - from) * ((value - from2) / (to2 - from2)) + from;
		}
	}
}
