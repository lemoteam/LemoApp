using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using System.Threading.Tasks;



public class AuthManager : MonoBehaviour {

	// Instance 
	public static AuthManager Instance;

	// Firebase API Variables
	Firebase.Auth.FirebaseAuth auth;

	// Delegates
	public delegate IEnumerator AuthCallback(Task<Firebase.Auth.FirebaseUser> task, string operation);
	public event AuthCallback authCallback;

	// UI objects linked from the inspector
	public Text statusText;

	// Prepare values
	private string markerId;


	/// <summary>
	/// Awake this instance.
	/// </summary>
	private void Awake() {
		Instance = this;
		auth = FirebaseAuth.DefaultInstance;
		// Auth Delegate Subscription
		authCallback += HandleAuthCallback;
	}



	// Firebase methods
	private void OnSignUp() {
		Debug.Log ("Sign Up");
		var email = markerId + "@lemo.fr";
		const string password = "IDduMarker" + "IDduMarker";
		SignUpNewUser(email, password);
	}

	public void OnLogin(string idMarker) {
		markerId = idMarker;
		Debug.Log ("Login");
		var email = markerId + "@lemo.fr";
		const string password = "IDduMarker" + "IDduMarker";
		LoginExistingUser(email, password);
	}



	// Actions
	public void SignUpNewUser(string email, string password) {
		auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
			StartCoroutine(authCallback(task, "sign_up"));
		});
	}
		
	public void LoginExistingUser(string email, string password) {
		auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task => {
			StartCoroutine(authCallback(task, "login"));
		});
	}



	// HandleCallback
	private IEnumerator HandleAuthCallback (Task<Firebase.Auth.FirebaseUser> task, string operation) {
		if (task.IsCanceled) {
			UpdateStatus ("La création a été annulée.");
		}

		if (task.IsFaulted) {
			UpdateStatus ("Nous te créons un compte.");

			// If there is no account create
			if (operation == "login") {
				OnSignUp();
				yield break;
			}
		}

		if (!task.IsCompleted) yield break;

		// Firebase user has been created.
		var newUser = task.Result;
		Debug.LogFormat("Tu es bien inscrit à Lémo {0}!", newUser.Email);

		if (operation == "sign_up") {		
			var reader = new Reader(newUser.Email);
			DatabaseManager.CreateNewReader(reader, newUser.UserId);
		}

		UpdateStatus ("Nous chargeons ton expérience");
		
		SetGlobalReader();

		yield return new WaitForSeconds (1.5f);
		UpdateStatus ("Allez va faire tes choix mané");
		GlobalManager.instance.isLoggin = true;
		MessageManager.ShowMessage("scanAfter");
	}


	private void SetGlobalReader() {
		DatabaseManager.GetReader(auth.CurrentUser.UserId, result => {
			GlobalManager.instance.reader = result;
			GlobalManager.instance.currentReaderUid = auth.CurrentUser.UserId;
		});
	}


	// Destroy
	private void OnDestroy() {
		// Auth Delegate Subscription
		authCallback -= HandleAuthCallback;
	}



	// Utilities
	private void UpdateStatus(string message) {
		statusText.text = message;
	}
}
