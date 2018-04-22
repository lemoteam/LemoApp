using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Reader {

	// Class properties
	public string email;
	public int mood;
	public int dynamic;
	public int intensity;
	public bool isReady;

	public Reader(string email) {
		this.email = email;
		this.mood = 0;
		this.dynamic = 0;
		this.intensity = 0;
		this.isReady = false;
	}
	
	public Reader(IDictionary<string, object> dict) {
		this.email = dict["email"].ToString();
		this.mood = Convert.ToInt32(dict["mood"]);
		this.dynamic = Convert.ToInt32(dict["dynamic"]);
		this.intensity = Convert.ToInt32(dict["intensity"]);
		this.isReady = Convert.ToBoolean(dict["isReady"]);
	}
}
