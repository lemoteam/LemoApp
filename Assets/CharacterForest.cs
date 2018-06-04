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

	private List<Transform> nodes;
	private List<Transform> forestElNodes;
	private List<Transform> forestTrNodes;
	private int currentNode = 0;
	private bool move = true;

	private void Start()
	{
		var forestElements = forestElementsContainer;
		var forestTrees = forestTreesContainer;
		
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
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			var ray = cam.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
	
			if (Physics.Raycast(ray, out hit))
			{
				agent.SetDestination(hit.point);
				isAnimated = true;
				Debug.Log(agent.pathPending);
			}
		}
		
		if (!agent.pathPending && isAnimated)
		{
			if (agent.remainingDistance <= agent.stoppingDistance)
			{
				if (!agent.hasPath || agent.velocity.sqrMagnitude == 0f)
				{
					Debug.Log("Je suis arrivé");
					isAnimated = false;
				}
			}
		}
	}
}
