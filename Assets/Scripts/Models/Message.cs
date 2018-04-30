using System.Collections.Generic;
using System;

public class Message {

	// Class properties
	public string key { get; private set; }
	public string value { get; private set; }

	public Message(string key, string value) {
		this.key = key;
		this.value = value;
	}
}
