﻿using UnityEngine;

public class PlaneCorrector : MonoBehaviour {

	public GameObject Plane;
	private WebCamTexture wc;
	private GameObject backgroundPlane; 
	// Use this for initialization
	void Start ()
	{

		var camera = GetComponent<Camera>();
		// var test = camera.Instance.GetBackgroundTexture();
		// camera.Web
		/*Texture2D t = (Texture2D)GetComponent().material.mainTexture;

		Debug.Log(t.GetPixel(100, 100));*/
		// backgroundPlane = null;
	}
	
	// Update is called once per frame
	void Update () {
		/*if (!backgroundPlane)
		{
			
		}*/
		if (Plane.GetComponent<MeshRenderer>().enabled)
		{
			GetColorFromScreen(Screen.width - Screen.width / 2 , Screen.height / 2, Screen.width / 10, Screen.height / 10);
		}
	}

	public void GetColorFromScreen(int x, int y, int width, int height) {
		
	Color [] pix = wc.GetPixels(x, y, width, height);
		float r = 0;
		float g = 0;
		float b = 0;
		float a = 0;
		foreach (Color col in pix){
			r += col.r;
			g += col.g;
			b += col.b;
			a += col.a;
		} 

		r /= pix.Length;
		g /= pix.Length;
		b /= pix.Length;
		a /= pix.Length;
		//Plane.GetComponent<MeshRenderer>().material.color = new Color (r, g, b, a);
		Plane.GetComponent<MeshRenderer>().material.SetColor("_EmissionColor",  new Color (r, g, b, a));
		// RenderSettings.ambientLight = new Color (r, g, b, a);
	}
}
