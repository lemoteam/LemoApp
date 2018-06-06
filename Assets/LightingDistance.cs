using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingDistance : MonoBehaviour
{
	public GameObject emmi;
	public GameObject catiche;
	public GameObject lightGameobject;
	private Light light;
	// Use this for initialization
	void Start ()
	{
		light = lightGameobject.GetComponent<Light>();
	}
	
	public float Map(float from, float to, float from2, float to2, float value) {
		if(value <= from2) {
			return from;
		} else if(value >= to2) {
			return to;
		} else {
			return (to - from) * ((value - from2) / (to2 - from2)) + from;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		var distance = emmi.transform.position - catiche.transform.position;
		var magnitude = distance.magnitude;
		//var intensityMap =
		//	light.intensity = intensityMap;
	}
}
