using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
	public GameObject player;
	public void Move(){
		gameObject.transform.position = player.transform.position;
	}
}
