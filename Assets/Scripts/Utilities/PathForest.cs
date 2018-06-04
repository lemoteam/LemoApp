using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathForest : MonoBehaviour
{

	public Color lineColor;
	private List<Transform> nodes = new List<Transform>();


	private void OnDrawGizmos()
	{
		Gizmos.color = lineColor;

		var pathTransforms = GetComponentsInChildren<Transform>();
		nodes = new List<Transform>();

		for (var i = 0; i < pathTransforms.Length; i++)
		{
			if (pathTransforms[i] != transform)
			{
				nodes.Add(pathTransforms[i]);
			}
		}

		for (var i = 0; i < nodes.Count; i++)
		{
			var currentNode = nodes[i].position;
			var previousNode = Vector3.zero;

			if (i > 0)
			{
				previousNode = nodes[i - 1].position;
			} else if (i == 0 && nodes.Count > 1)
			{
				previousNode = nodes[nodes.Count - 1].position;
			}
			
			Gizmos.DrawLine(previousNode, currentNode);
			Gizmos.DrawWireSphere(currentNode, .01f);
		}
	}
}
