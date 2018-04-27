using UnityEngine;
using Firebase.Database;

public class Router : MonoBehaviour {

	private static readonly DatabaseReference baseRef = FirebaseDatabase.DefaultInstance.RootReference;

	public static DatabaseReference ReaderWithUID(string uid) {
		return baseRef.Child("reader").Child(uid);
	}
	
	public static DatabaseReference CurrentReader() {
		return baseRef.Child("reader").Child(GlobalManager.instance.currentReaderUid);
	}
}
