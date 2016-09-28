using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {
	float score=0;
	public bool active=true;
	void Start () {
		DontDestroyOnLoad(this);
	}

	public void Increase(float s){if(active)score += s;}
	public float GetScore(){return score;}
}
