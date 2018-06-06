using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingDistance : MonoBehaviour
{
	public GameObject emmi;
	public GameObject catiche;
	public GameObject charBase;
	public GameObject lightGameobject;
	private Light light;

	private Vector3 originPosition;
	private Vector3 destinationPosition;
	
	// Use this for initialization
	void Start ()
	{
		light = lightGameobject.GetComponent<Light>();
		originPosition = charBase.transform.position;
		destinationPosition = catiche.transform.position;
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
		var currentDistance = Vector3.Distance(catiche.transform.position, emmi.transform.position);
		light.intensity = Map(10, 1, 0.06f, 1f, currentDistance);
	}
}
