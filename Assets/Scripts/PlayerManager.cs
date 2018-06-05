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
	private float replaceFraction = 1;
	private float replaceSpeed = .2f;
	private float lightIntensity = 10f;
	public int index = 0;
	private int prevIndex = -2;
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
	private void Update ()
	{	
		// On click
		if (Input.GetMouseButtonDown(0))
		{
			// Destination
			goToCheckpoint(index);
			
			if (index < checkpoints.Length - 1)
			{
				prevIndex = index;
				index++;
			}
			else
			{
				prevIndex = checkpoints.Length - 1;
				index = 0;
			}
		}
		
		// If animated
		if (replaceFraction < 1)
		{
			replaceFraction += Time.deltaTime * replaceSpeed;
			
			// Light On
			var indexL = index - 1 == -1 ? checkpoints.Length - 1 : index - 1;
			var indexPrev = prevIndex - 1 == -1 ? checkpoints.Length - 1 : prevIndex - 1;
			
			var light = checkpoints[indexL].GetComponent<Light>();
			light.intensity = Mathf.SmoothStep(0, lightIntensity, CubicEaseOut(replaceFraction));
			
			if (indexPrev != indexL) {
				var prevlight = checkpoints[indexPrev].GetComponent<Light>();
				prevlight.intensity = Mathf.SmoothStep(lightIntensity, 0, CubicEaseOut(replaceFraction));
			}
		
			// Light Off
			/*if (prevIndex != -2)
			{
				var indexPL = prevIndex - 1 == -1 ? checkpoints.Length - 1 : index - 1;
				var prevlight = checkpoints[indexPL].GetComponent<Light>();
				prevlight.intensity = Mathf.SmoothStep(lightIntensity, 0, CubicEaseOut(replaceFraction));
			}*/
			
		}
		
		
	}

	
	private void LookAtemmi(IEnumerable<GameObject> pursuerList)
	{
		foreach (var pursuer in pursuerList)
		{
			pursuer.transform.LookAt(player.transform);
		}
	}

	
	public void goToCheckpoint(int index)
	{
		replaceFraction = 0;
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
				//setLightIntensity(checkpoint, 0);
			}
			else
			{
				//setCheckpointColor(checkpoint, Color.blue, Color.blue);
				//setLightIntensity(checkpoint, 15f);
				LookAtemmi(pursuerList);
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
