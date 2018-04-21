using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using System;

public class DatabaseManager : MonoBehaviour {

	public void InitDatabase() {
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unity-firebase-78a2d.firebaseio.com/");
		Debug.Log ("Database Ready");
	}
	
	public void CreateNewReader(Reader reader, string uid) {
		Debug.Log ("readerJSON readerJSON readerJSON");
		string readerJSON = JsonUtility.ToJson(reader);
		Router.ReaderWithUID(uid).SetRawJsonValueAsync(readerJSON);
	}

	public void GetReader(string UID, Action<Reader> completionBlock) {
		Router.ReaderWithUID(UID).GetValueAsync().ContinueWith (task => { // Place a UID
			DataSnapshot reader = task.Result;
			var readerDict = (IDictionary<string, object>)reader.Value;
			Reader newReader = new Reader(readerDict);
			completionBlock(newReader);
		});
	}
}
