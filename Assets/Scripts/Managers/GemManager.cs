using System;
using System.Collections.Generic;
using UnityEngine;

public class GemManager : MonoBehaviour {

	public GameObject Gem;
	public Animation animation;
	public ParticleSystem baseParticle, particles;
	
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

		if (baseParticle && particles && !baseParticle.isPlaying)
		{
			baseParticle.Play();
			particles.Play();
		}
	}

	public void StopAnimation()
	{
		if (animation) {
			animation.Stop();
		}
		
		if (baseParticle && particles && baseParticle.isPlaying)
		{
			baseParticle.Stop();
			particles.Stop();
		}
	}
}
