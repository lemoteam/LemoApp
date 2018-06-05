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
				
				if (audioSource.volume == 1f)
				{
					Debug.Log("<color=green> Coucou : emmi "+"</color>");
					audioSource.volume = 0.06f;
					isPlayin = false;
				}

			}
			else
			{
				if (audioSource.volume == 0.06f)
				{
					Debug.Log("<color=green> Coucou : catiche "+"</color>");
					audioSource.volume = 1f;
					isPlayin = true;
				}
			
			}
/*			for (int i = 0; i < players.Length; i++)
			{
				var distance = Vector3.Distance(camera.transform.position, players[i].transform.position);
				if (distance < minimum)
				{
					minimum = distance;	
					var currentIndex = i;
					//Debug.Log("<color=pink> min distance : "+ minimum +"</color>");

					if (previousIndex != currentIndex)
					{
						Debug.Log("<color=green> Coucou : "+ players[i].name +"</color>");
						if (players[i].name == "catiche")
						{
							audioSource.Play();
						}
						else
						{
							audioSource.Stop();
						}
						previousIndex = currentIndex;
					}
				}
			}*/


			//Debug.Log("<color=white> distance emmi : "+ Vector3.Distance(camera.transform.position,emmi.transform.position) +"</color>");
			//		Debug.Log("<color=yellow> distance catiche : "+ Vector3.Distance(camera.transform.position,catiche.transform.position) +"</color>");
		}
	}
}
