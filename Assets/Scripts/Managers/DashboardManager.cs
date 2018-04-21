using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;

public class DashboardManager : MonoBehaviour {

	public GameObject profilePanel; 
	
	Firebase.Auth.FirebaseAuth auth;

	void Awake()
	{
		auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
		GlobalManager.instance.database.GetReader(auth.CurrentUser.UserId, result => {
			Debug.Log(result.email);
			profilePanel.GetComponent<Profile>().Display(result);
		});
	}
}
 