using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceSongCam : MonoBehaviour
{
	private GameObject catiche;
	private GameObject buisson;
	private GameObject emmi;
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
		catiche = GameObject.Find("catiche");
		emmi = GameObject.Find("emmi");
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
				
				if (audioSource.volume == 1f && !isLerpin)
				{
					Debug.Log("<color=green> Coucou : emmi "+"</color>");
					audioSource.volume = 0.15f;
					isPlayin = false;
					/*isLerpin = true;
					audioSource.volume = Mathf.Lerp(1f, 0.003f, t);
					t += 0.5f * Time.deltaTime;

					if (t > 1.0f)
					{
						isLerpin = false;
						t = 0.0f;
					}*/
				}
			}
			else
			{
				if (audioSource.volume == 0.15f && !isLerpin)
				{
					Debug.Log("<color=green> Coucou : catiche "+"</color>");
					audioSource.volume = 1f;
					/*audioSource.volume = Mathf.Lerp(0.003f, 1f, t);
					t += 0.5f * Time.deltaTime;

					if (t > 1.0f)
					{
						isLerpin = false;
						t = 0.0f;
					}
					isPlayin = true;*/
				}
			}
		}
	}
}
