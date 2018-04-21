using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Reader {

	// Class properties
	public string email;
	public int score;
	public int level;

	public Reader(string email, int score, int level) {
		this.email = email;
		this.score = score;
		this.level = level;
	}
	
	public Reader(IDictionary<string, object> dict) {
		this.email = dict["email"].ToString();
		this.score = Convert.ToInt32(dict["score"]);
		this.level = Convert.ToInt32(dict["level"]);
	}
}
