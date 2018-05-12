using System;
using System.Collections.Generic;
using UnityEngine;

public class GemManager : MonoBehaviour {

	public GameObject Gem;
	private Vector3 CurrentGemPosition;
	
	private bool isCurrentPositionSet;
	
	private float GemReplaceFraction = 0;
	private float GemReplaceSpeed = 1f;
	private float RotationSpeed = 0f;

	public Animation animation;
	
	// Lifecycle
	void Start()
	{
		GlobalManager.instance.gemManagerList.Add(this);
	}


	private void FixedUpdate()
	{	
		// Levitation
		var newGemPosition = Gem.transform.position;
		newGemPosition.y += (Mathf.Cos(Time.time) / 10f)* Time.deltaTime;
		Gem.transform.position = newGemPosition;
	}

	private void OnDestroy()
	{
		GlobalManager.instance.gemManagerList.Remove(this);
	}
	
	
	// Methods
	public void PlayAnimation()
	{	
		if (animation) {
			animation.Play();
		}
	}

	public void StopAnimation()
	{
		if (animation) {
			animation.Stop();
		}
	}
}
