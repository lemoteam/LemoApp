using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScriptElement : MonoBehaviour
{

	public GameObject plane;
	public GameObject spawnPoint;
	private Rigidbody elRigidbody;
	private Vector3 NewGravitySettings;
	private bool isReady = false;

	// Use this for initialization
	void Start ()
	{
		elRigidbody = transform.GetComponent<Rigidbody>();
		elRigidbody.isKinematic = true;
		StartCoroutine(addGravity());
	}
	

	private IEnumerator addGravity()
	{
		yield return new WaitForSeconds(1.5f);
		elRigidbody.isKinematic = false;
		NewGravitySettings = new Vector3(0f, -30f, 0f); // Physics.gravity
		isReady = true;
	}
}
