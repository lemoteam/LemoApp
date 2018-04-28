using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Unity.Editor;
using System;
using System.Linq;
using Firebase.Database;

public class DatabaseManager : MonoBehaviour {

	public static void InitDatabase() {
		FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://unity-firebase-78a2d.firebaseio.com/");
		Debug.Log ("Database Ready");
	}
	
	public static void CreateNewReader(Reader reader, string uid) {
		Debug.Log ("readerJSON readerJSON readerJSON");
		var readerJSON = JsonUtility.ToJson(reader);
		Router.ReaderWithUID(uid).SetRawJsonValueAsync(readerJSON);
	}

	public static void GetReader(string UID, Action<Reader> completionBlock) {
		Router.ReaderWithUID(UID).GetValueAsync().ContinueWith (task => { 
			var reader = task.Result;
			var readerDict = (IDictionary<string, object>)reader.Value;
			var newReader = new Reader(readerDict);
			completionBlock(newReader);
		});
	}

	public static void GetMessages(Action<List<Message>> completionBlock)
	{
		var tmpList = new List<Message>();

		Router.Messages().GetValueAsync().ContinueWith(task =>
		{
			var messages = task.Result;

			tmpList.AddRange(from item in messages.Children let key = item.Key let value = item.Value.ToString() select new Message(key, value));
			completionBlock(tmpList);
		});
	}
}
