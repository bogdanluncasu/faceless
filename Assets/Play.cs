using UnityEngine;
using System.Collections;

public class Play : MonoBehaviour {
	public void NewGame(){
		Application.LoadLevel("levels");
	}

	public void GoToMainMenu(){
		Application.LoadLevel("1");
	}

}
