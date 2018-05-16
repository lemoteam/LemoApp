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

	/*private void FixedUpdate()
	{	
		// Levitation
		var newGemPosition = Gem.transform.position;
		newGemPosition.y += (Mathf.Cos(Time.time) / 10f)* Time.deltaTime;
		// newGemPosition.y = (Mathf.PerlinNoise(Mathf.Cos(90), Time.time / 50f) * .2f) - 2f;

		// newGemPosition.x = (Mathf.PerlinNoise(Mathf.Cos(90), Time.time / 50f) * .2f) - 2f;
		// newGemPosition.z = (Mathf.PerlinNoise(Mathf.Sin(45), Time.time / 10f) * 2f) - 2f;
		//Debug.Log(Mathf.PerlinNoise(Mathf.Cos(90), Time.time / 10f) * 2f);
		Gem.transform.position = newGemPosition;
	}*/

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
