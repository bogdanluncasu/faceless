using UnityEngine;
using System.Collections;

public class HideLoggedInStuff : MonoBehaviour {
	public GameObject IsLoggedIn;
	public GameObject NotLoggedIn;
	// Use this for initialization
	void Start () {
		IsLoggedIn.SetActive (false);
		NotLoggedIn.SetActive (true);
	}
	

}
