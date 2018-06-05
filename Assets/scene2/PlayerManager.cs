using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	private PlayerManager instance;
	public GameObject player;
	public GameObject pursuer;
	private bool animatedLight = false;
	private float replaceFraction = 0;
	private float replaceSpeed = .2f;
	public int index = 0;
	public int nbPursuer;
	public int radiusPursuer;
	private List<GameObject> pursuerList;
	private List<Vector3> checkpointPositions;
	private GameObject[] checkpoints;
	
	private void Awake()
	{
		instance = this;
		
		// Set new list
		pursuerList = new List<GameObject>();
		
		// Get all points positions
		checkpointPositions = new List<Vector3>();
		checkpoints = GameObject.FindGameObjectsWithTag("Checkpoint");
		
		foreach (GameObject checkpoint in checkpoints)
		{
			checkpointPositions.Add(checkpoint.transform.position);
		}
		
		// Create Pursuer
		createPursuer();
	}
	private void Stop()
	{
		animatedLight = true;
	}

	
	private void createPursuer()
	{
		for (var i = 0; i < nbPursuer; i++)
		{
			// Prepare position
			var position = calcArroundPosition(i, radiusPursuer) + player.transform.position;
			
			// Instantiate
			var pursuerObj = Instantiate(pursuer);
			
			// Set position
			pursuerObj.transform.parent = transform;
			pursuerObj.transform.position = position;
			
			pursuerList.Add(pursuerObj);
		}
	}


	private Vector3 calcArroundPosition(int index, int radius)
	{
		var angle = (360 / nbPursuer) * index;
		var posX = Math.Sin((angle * Math.PI / 180)) * radius;
		var posY = Math.Cos((angle * Math.PI / 180)) * radius;
		
		return new Vector3(ToSingle(posX), 0, ToSingle(posY));
	}
	
	
	private float ToSingle(double value)
	{
		return (float)value;
	}
	
	
	
	// Update is called once per frame
	private void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			
			goToCheckpoint(this.index);
			if (this.index < this.checkpoints.Length - 1)
			{
				this.index++;
			}
			else
			{
				this.index = 0;
			}
		}
	}

	public void goToCheckpoint(int index)
	{
		var playerController = player.GetComponent<PlayerController>();
		playerController.displaceAgent(checkpointPositions[index]);
		setCheckpointActive(index);
		instance.StopCoroutine(displacePursuer(checkpointPositions[index]));
		instance.StartCoroutine(displacePursuer(checkpointPositions[index]));
	}


	private void setCheckpointActive(int index)
	{
		var i = 0;
		
		foreach (GameObject checkpoint in checkpoints)
		{
			if (i != index) 
			{
				//setCheckpointColor(checkpoint, Color.white, Color.white);
				setLightIntensity(checkpoint, 0);
			}
			else
			{
				//setCheckpointColor(checkpoint, Color.blue, Color.blue);
				setLightIntensity(checkpoint, 15f);
				// setCheckpointColor(checkpoint, Color.blue, Color.blue);
			}
			
			i++;
		}
		
	}


	private void setCheckpointColor(GameObject checkpoint, Color color, Color specular)
	{
		//Fetch the Renderer from the GameObject
		Renderer rend = checkpoint.GetComponent<Renderer>();

		//Set the main Color of the Material to green
		rend.material.shader = Shader.Find("_Color");
		rend.material.SetColor("_Color", color);

		//Find the Specular shader and change its Color to red
		rend.material.shader = Shader.Find("Specular");
		rend.material.SetColor("_SpecColor", specular);
	}
	
	private void setLightIntensity(GameObject checkpoint, float intensity)
	{
		//Fetch the Renderer from the GameObject
		var light = checkpoint.GetComponent<Light>();
		light.intensity = intensity;

	}
	
	
	private IEnumerator displacePursuer(Vector3 playerPosition)
	{
		yield return new WaitForSeconds(1f);
		foreach (var pursuer in pursuerList.Select((value, i) => new { i, value }))
		{
			var playerController = pursuer.value.GetComponent<PlayerController>();
			var position = playerPosition + calcArroundPosition(pursuer.i, radiusPursuer);
			playerController.displaceAgent(position);
		}
	}
	static public float CubicEaseOut(float p)
	{
		float f = (p - 1);
		return f * f * f + 1;
	}
}
