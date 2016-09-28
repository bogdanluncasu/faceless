using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour {


	void Start () {
		Text score = gameObject.GetComponent<Text> ();
		score.text = ((int)GameObject.Find ("Score").GetComponent<Score> ().GetScore()).ToString();
	}
}
