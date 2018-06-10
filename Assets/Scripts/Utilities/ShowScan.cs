using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowScan : MonoBehaviour
{

	public float time;
	
	// Use this for initialization
	void Start ()
	{
		StartCoroutine(Animate());
	}
	
	private IEnumerator Animate()
	{
		yield return new WaitForSeconds(time);
		var ani = this.GetComponent<Animator>();
		ani.Play("scanShow");
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
