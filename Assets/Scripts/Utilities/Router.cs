using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;

public class Router : MonoBehaviour {

	private static DatabaseReference baseRef = FirebaseDatabase.DefaultInstance.RootReference;

	public static DatabaseReference Reader() {
		return baseRef.Child("reader");
	}

	public static DatabaseReference ReaderWithUID(string uid) {
		return baseRef.Child("reader").Child(uid);
	}
}
