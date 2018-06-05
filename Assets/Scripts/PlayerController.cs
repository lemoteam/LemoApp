using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

	public NavMeshAgent agent;
	public bool isAnimated = false;
	
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
				}
			}
		}
	}

	public void displaceAgent(Vector3 destination)
	{
		agent.SetDestination(destination);
		isAnimated = true;
	}
}
