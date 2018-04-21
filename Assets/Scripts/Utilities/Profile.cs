using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;

public class Profile : MonoBehaviour {

    public Text emailString;

    public void Display(Reader reader) {
        this.emailString.text = string.Format("You are signin as {0}", reader.email);
    }
}
