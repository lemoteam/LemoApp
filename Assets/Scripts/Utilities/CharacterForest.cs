using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Vuforia;

public class CharacterForest : MonoBehaviour, ITrackableEventHandler
{
	
	private TrackableBehaviour mTrackableBehaviour;
	
	public NavMeshAgent agent;
	private bool isAnimated = false;
	
	public GameObject forestElementsContainer;
	public GameObject forestTreesContainer;
	public GameObject character;

	public GameObject destination;
	public Light destinationLight;

	private List<Transform> nodes;
	private List<Transform> forestElNodes;
	private List<Transform> forestTrNodes;
	private Vector3 destinationPosition;
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
		
		// Trackable Behaviour
		mTrackableBehaviour = GetComponent<TrackableBehaviour>();
		if (mTrackableBehaviour)
			mTrackableBehaviour.RegisterTrackableEventHandler(this);
		
		// Launch Animation
		destinationPosition = destination.transform.position;
		agent.SetDestination(destinationPosition);
		StopMove();
	}
	

	private void FixedUpdate()
	{
		CheckForestElementsDistance();
		CheckForestTreesDistance();
	}
	
	
	public void OnTrackableStateChanged( TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus) {
		
		if (newStatus == TrackableBehaviour.Status.DETECTED ||
		    newStatus == TrackableBehaviour.Status.TRACKED ||
		    newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED) {
			
			Debug.Log("Custom Trackable " + mTrackableBehaviour.TrackableName + " found");
			OnTrackingFound();
		}
		
		else if (previousStatus == TrackableBehaviour.Status.TRACKED && newStatus == TrackableBehaviour.Status.NO_POSE) {
			
			Debug.Log("Trackable " + mTrackableBehaviour.TrackableName + " lost");
			OnTrackingLost();
		}
	}
	
	
	// Tracking Found
	private void OnTrackingFound()
	{
		StartMove();
	}
	
	// Tracking Lost
	private void OnTrackingLost()
	{
		StopMove();
	}
	

	private void CheckForestElementsDistance()
	{
		foreach (var node in forestElNodes)
		{
			var elementForest = node.GetComponent<ElementForest>();

			if (Vector3.Distance(character.transform.position, node.position) < .2f)
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

			
			if (Vector3.Distance(character.transform.position, node.position) < .3f)
			{
				
				if (!elementForest.isActive)
				{
					elementForest.activeAnimation();
				}
			} 
		}
	}
	
	// Start Move
	private void StartMove()
	{
		agent.Resume();
	}
	
	// Stop Move
	private void StopMove()
	{
		agent.Stop();
	} 

	// Stop
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

