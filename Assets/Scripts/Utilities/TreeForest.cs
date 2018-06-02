using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeForest : MonoBehaviour {

	private Vector3 elementPosition;
	private Vector3 destinationPosition;
	private Vector3 currentPosition;
	public bool direction;
	public bool isActive;
	private bool isCurrentPositionSet;
	
	private float gemReplaceFraction = 0;
	private float gemReplaceSpeed = .6f;
	

	private void Start()
	{
		isActive = false;
		elementPosition = transform.position;

		if (direction) {
			destinationPosition = new Vector3(elementPosition.x - .6f, elementPosition.y, elementPosition.z);
		} else {
			destinationPosition = new Vector3(elementPosition.x + .6f, elementPosition.y, elementPosition.z);
		} 
	}


	private void FixedUpdate()
	{
		
		
		if (isActive)
		{			
			if (!(gemReplaceFraction < 1))
			{
				isActive = false;
			};
			gemReplaceFraction += Time.deltaTime * gemReplaceSpeed;
			transform.position = new Vector3(Mathf.SmoothStep(currentPosition.x, destinationPosition.x, CubicEaseOut(gemReplaceFraction)), transform.position.y, transform.position.z);
			
		} 
	}


	public void activeAnimation()
	{
		isActive = true;
		gemReplaceFraction = 0;
		isCurrentPositionSet = false;
		currentPosition = transform.position;
	}
	
	
	
	// EASING FUNCTIONS
	// https://github.com/acron0/Easings/blob/master/Easings.cs
	
	static public float CubicEaseOut(float p)
	{
		float f = (p - 1);
		return f * f * f + 1;
	}
	

	static public float BounceEaseOut(float p)
	{
		if(p < 4/11.0f)
		{
			return (121 * p * p)/16.0f;
		}
		else if(p < 8/11.0f)
		{
			return (363/40.0f * p * p) - (99/10.0f * p) + 17/5.0f;
		}
		else if(p < 9/10.0f)
		{
			return (4356/361.0f * p * p) - (35442/1805.0f * p) + 16061/1805.0f;
		}
		else
		{
			return (54/5.0f * p * p) - (513/25.0f * p) + 268/25.0f;
		}
	}
}
