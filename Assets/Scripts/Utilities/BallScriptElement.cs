using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScriptElement : MonoBehaviour
{

	public GameObject plane;
	public GameObject spawnPoint;
	private Rigidbody elRigidbody;
	private Vector3 NewGravitySettings; 

	// Use this for initialization
	void Start ()
	{
		elRigidbody = transform.GetComponent<Rigidbody>();
		NewGravitySettings = new Vector3(0f, -100f, 0f); // Physics.gravity
	}
	
	// Update is called once per frame
	private void FixedUpdate()
	{
		elRigidbody.AddForce(NewGravitySettings * elRigidbody.mass);
	}
}
