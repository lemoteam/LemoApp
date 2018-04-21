using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Firebase;
using Firebase.Auth;
using UnityEngine.SceneManagement;

public class FormManager : MonoBehaviour {

	// UI objects linked from the inspector
	public InputField emailInput;
	public InputField passwordInput;

	public Button signUpButton;
	public Button loginButton;

	public Text statusText;

	public AuthManager authManager;

	void Awake() {
		ToggleButtonStates (false);

		// Auth Delegate Subscription
		authManager.authCallback += HandleAuthCallback;
	}

	/// <summary>
	/// Validates the email input
	/// </summary>
	public void ValidateEmail() { 
		string email = emailInput.text;
		var regexPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
                + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
                + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
                + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

		if (email != "" && Regex.IsMatch(email, regexPattern)) {
			ToggleButtonStates (true);
		} else {
			ToggleButtonStates (false);
		}
	}

	// Firebase methods
	public void OnSignUp() {
		Debug.Log ("Sign Up");
		authManager.SignUpNewUser(emailInput.text, passwordInput.text);
	}

	public void OnLogin() {
		Debug.Log ("Login");
		authManager.LoginExistingUser(emailInput.text, passwordInput.text);
	}


	IEnumerator HandleAuthCallback (Task<Firebase.Auth.FirebaseUser> task, string operation) {
		if (task.IsCanceled) {
			UpdateStatus ("La création a été annulée.");
		}
		if (task.IsFaulted) {
			UpdateStatus ("Oups une erreur est survenue : " + task.Exception);
		}
		if (task.IsCompleted) {

			// Firebase user has been created.
			Firebase.Auth.FirebaseUser newUser = task.Result;
			Debug.LogFormat("Tu es bien inscrit à Lémo {0}!", newUser.Email);

			if (operation == "sign_up") {		
				Debug.Log ("SIGN SIGN SIGN");		
				Reader reader = new Reader(newUser.Email, 0, 1);
				GlobalManager.instance.database.CreateNewReader(reader, newUser.UserId);
			}

			UpdateStatus ("On charge ton expérience");

			yield return new WaitForSeconds (1.5f);
			GlobalManager.instance.sceneLoader.LoadScene ("Dashboard");
		}
	} 

	void OnDestroy() {
		// Auth Delegate Subscription
		authManager.authCallback -= HandleAuthCallback;
	}

	// Utilities
	private void ToggleButtonStates(bool toState) {
		signUpButton.interactable = toState;
		loginButton.interactable = toState;
	}

	private void UpdateStatus(string message) {
		statusText.text = message;
	}
}
