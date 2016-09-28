using UnityEngine;
using System.Collections;

public class Transparent : MonoBehaviour {

	void Start(){
		gameObject.GetComponent<Renderer>().material=new Material(Shader.Find("Transparent/Diffuse"));
		gameObject.GetComponent<Renderer> ().material.color = new Color (0, 0, 0, 0);
	}
}
