using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterForest : MonoBehaviour
{
	public Camera cam;
	public NavMeshAgent agent;
	public bool isAnimated = false;
	
	public GameObject forestElementsContainer;
	public GameObject forestTreesContainer;

	public GameObject destination;
	public Light destinationLight;

	private List<Transform> nodes;
	private List<Transform> forestElNodes;
	private List<Transform> forestTrNodes;
	private int currentNode = 0;
	private bool move = true;
	private bool animatedLight = false;
	private Light lt;
	
	private float replaceFraction = 0;
	private float replaceSpeed = .2f;
	
	
	private void Start()
	{
		var forestElements = forestElementsContainer;
		var forestTrees = forestTreesContainer;

		lt = destinationLight.GetComponent<Light>();
		
		forestElNodes = new List<Transform>();
		forestTrNodes = new List<Transform>();

		for (var i = 0; i < forestElements.transform.childCount; i++)
		{			
			forestElNodes.Add(forestElements.transform.GetChild(i));
		}
		
		for (var i = 0; i < forestTrees.transform.childCount; i++)
		{			
			forestTrNodes.Add(forestTrees.transform.GetChild(i));
		}
		
		
		// Launch Animation
		var destinationPosition = destination.transform.position;
		Move(destinationPosition);

	}
	

	private void FixedUpdate()
	{
		CheckForestElementsDistance();
		CheckForestTreesDistance();
	}

	private void CheckForestElementsDistance()
	{
		foreach (var node in forestElNodes)
		{
			var elementForest = node.GetComponent<ElementForest>();

			if (Vector3.Distance(transform.position, node.position) < .2f)
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
			
			// Debug.Log(Vector3.Distance(transform.position, node.position));

			
			if (Vector3.Distance(transform.position, node.position) < .3f)
			{
				
				if (!elementForest.isActive)
				{
					elementForest.activeAnimation();
				}
			} 
		}
	}

	private void Move(Vector3 destinationPos)
	{
		agent.SetDestination(destinationPos);
		isAnimated = true;
	}


	private void Stop()
	{
		animatedLight = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (!agent.pathPending && isAnimated)
		{
			if (agent.remainingDistance <= agent.stoppingDistance)
			{
				if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
				{
					Debug.Log("Je suis arrivé");
					isAnimated = false;
					Stop();
				}
			}
		}


		if (animatedLight)
		{	
			if (!(replaceFraction < 1));
			replaceFraction += Time.deltaTime * replaceSpeed;
			lt.intensity = Mathf.SmoothStep(0, 50, CubicEaseOut(replaceFraction));
		}
	}
	
	
	static public float CubicEaseOut(float p)
	{
		float f = (p - 1);
		return f * f * f + 1;
	}

}

