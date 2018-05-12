using System;
using System.Collections.Generic;
using UnityEngine;

public class GemManager : MonoBehaviour {

	public GameObject Gem;
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
		if (!animation) return;
		Debug.Log("PLAYYYY");
		animation.Play();
	}

	public void StopAnimation()
	{
		if (!animation) return;
		Debug.Log("STOOOOOP");
		animation.Stop();
	}
}
