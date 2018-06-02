using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementForest : MonoBehaviour {

	private Vector3 elementPosition;
	private Vector3 destinationPosition;
	private Vector3 currentPosition;
	public bool isActive;
	private bool isCurrentPositionSet;
	
	private float gemReplaceFraction = 0;
	private float gemReplaceSpeed = 1f;
	

	private void Start()
	{
		isActive = false;
		elementPosition = transform.position;
		destinationPosition = new Vector3(elementPosition.x, 1, elementPosition.z);
	}


	private void FixedUpdate()
	{
		
		
		if (isActive)
		{			
			if (!(gemReplaceFraction < 1)) return;
			gemReplaceFraction += Time.deltaTime * gemReplaceSpeed;
			transform.position = new Vector3(transform.position.x, Mathf.SmoothStep(currentPosition.y, destinationPosition.y, CubicEaseOut(gemReplaceFraction)), transform.position.z);
			
		} else {
			
			if (!isCurrentPositionSet) {
				currentPosition = transform.position;
				isCurrentPositionSet = true;
			}
			
			if (!(gemReplaceFraction < 1));
			gemReplaceFraction += Time.deltaTime * gemReplaceSpeed;
			transform.position = new Vector3(transform.position.x, Mathf.SmoothStep(currentPosition.y, elementPosition.y, CubicEaseOut(gemReplaceFraction)), transform.position.z);
		}
	}


	public void activeAnimation()
	{
		isActive = true;
		gemReplaceFraction = 0;
		isCurrentPositionSet = false;
		currentPosition = transform.position;
	}



	public void unactiveAnimation() {
		isActive = false;
		gemReplaceFraction = 0;
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
