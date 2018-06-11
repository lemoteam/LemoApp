using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinLight : MonoBehaviour {
	Light light;
	private float minIntensity = 1f;
	private float maxIntensity = 10f;	
	private float frequency = 0.25f;
	private float phase = 4f;
	
	// Use this for initialization
	void Start () {
		light = GetComponent<Light>();
	}
	
	// Update is called once per frame
	void Update () {
		float x = (Time.time + phase) * frequency;
		x = x - Mathf.Floor(x);
		light.intensity = maxIntensity * Mathf.Sin(2 * Mathf.PI * x) + minIntensity;
	}
}
