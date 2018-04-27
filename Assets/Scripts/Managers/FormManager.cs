using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using Firebase.Auth;

public class FormManager : MonoBehaviour {

	// UI objects linked from the inspector
	public Text statusText;
	
	// Manager
	public AuthManager authManager;
	private string markerId;
	
	// Awake
	private void Awake() {

		// Auth Delegate Subscription
		authManager.authCallback += HandleAuthCallback;
	}

	
	// Firebase methods
	// NOTE: FACTORISER - Ici on passera l'email et le password grace à l'ID du marker scanné
	private void OnSignUp() {
		Debug.Log ("Sign Up");
		var email = markerId + "@lemo.fr";
		const string password = "IDduMarker" + "IDduMarker";
		authManager.SignUpNewUser(email, password);
	}
	
	
	public void OnLogin(string idMarker)
	{
		markerId = idMarker;
		Debug.Log ("Login");
		var email = markerId + "@lemo.fr";
		const string password = "IDduMarker" + "IDduMarker";
		authManager.LoginExistingUser(email, password);
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

		yield return new WaitForSeconds (1.5f);
		UpdateStatus ("Allez va faire tes choix mané");
		GlobalManager.instance.isLoggin = true;

		// GlobalManager.instance.sceneLoader.LoadScene ("InGame");
	}

	
	private void OnDestroy() {
		// Auth Delegate Subscription
		authManager.authCallback -= HandleAuthCallback;
	}

	
	// Utilities
	private void UpdateStatus(string message) {
		statusText.text = message;
	}
}
