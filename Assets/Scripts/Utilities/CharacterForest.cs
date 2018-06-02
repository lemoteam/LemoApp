using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterForest : MonoBehaviour
{
	public Transform path;
	public GameObject forestElementsContainer;
	public GameObject forestTreesContainer;
	public float maxSteerAngle = 45f; 
	public WheelCollider wheelL;
	public WheelCollider wheelR;
	private List<Transform> nodes;
	private List<Transform> forestElNodes;
	private List<Transform> forestTrNodes;
	private int currentNode = 0;
	private bool move = true;

	private void Start()
	{
		var pathTransforms = path.GetComponentsInChildren<Transform>();
		var forestElements = forestElementsContainer;
		var forestTrees = forestTreesContainer;
		
		nodes = new List<Transform>();
		forestElNodes = new List<Transform>();
		forestTrNodes = new List<Transform>();

		for (var i = 0; i < pathTransforms.Length; i++)
		{
			if (pathTransforms[i] != path.transform)
			{
				nodes.Add(pathTransforms[i]);
			}
		}

		for (var i = 0; i < forestElements.transform.childCount; i++)
		{			
			forestElNodes.Add(forestElements.transform.GetChild(i));
		}
		
		for (var i = 0; i < forestTrees.transform.childCount; i++)
		{			
			forestTrNodes.Add(forestTrees.transform.GetChild(i));
		}
	}

	private void FixedUpdate()
	{
		ApplySteer();

		if (move)
		{
			Move();
		}
		else
		{
			Stop();
		}
		
		CheckWaypointDistance();
		CheckForestElementsDistance();
		CheckForestTreesDistance();
	}

	private void Move()
	{
		wheelL.motorTorque = 5f;
		wheelR.motorTorque = 5f;
	}
	
	private void Stop()
	{
		wheelL.motorTorque = 0f;
		wheelR.motorTorque = 0f;
		Debug.Log("STOOOOOOP");
	}

	private void ApplySteer()
	{
		var relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);
		var wheelAngle = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
		wheelL.steerAngle = wheelAngle;
		wheelR.steerAngle = wheelAngle;
	}

	private void CheckWaypointDistance()
	{
		if (Vector3.Distance(transform.position, nodes[currentNode].position) < 0.8f)
		{
			if (currentNode == nodes.Count - 1)
			{
				currentNode = 0;
				move = false;
				//Debug.Log("STOOOOP PLEASE");
				Debug.Log(currentNode);
			}
			else
			{
				currentNode++;
				Debug.Log(currentNode);
			}
		}
	}


	private void CheckForestElementsDistance()
	{
		foreach (var node in forestElNodes)
		{
			var elementForest = node.GetComponent<ElementForest>();

			if (Vector3.Distance(transform.position, node.position) < 1f)
			{
				if (!elementForest.isActive)
				{
					elementForest.activeAnimation();
				}
			} else {
				if (elementForest.isActive)
				{
					elementForest.unactiveAnimation();
				}
			}
		}
	}
	
	
	
	private void CheckForestTreesDistance()
	{
		foreach (var node in forestTrNodes)
		{
			var elementForest = node.GetComponent<TreeForest>();
			
			Debug.Log(Vector3.Distance(transform.position, node.position));

			
			if (Vector3.Distance(transform.position, node.position) < 1.6f)
			{
				
				if (!elementForest.isActive)
				{
					elementForest.activeAnimation();
				}
			} 
		}
	}
}
