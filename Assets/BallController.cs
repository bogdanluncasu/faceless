using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {
	public float health=5f;
	Score score;

	void Start(){
		score = GameObject.Find ("Score").GetComponent<Score> () as Score;
		score.active = true;
	}
	void Update () {
		if (health <= 0)
			EndGame ();
		//float horizontal = Input.GetAxis ("Horizontal");
		float vertical = Input.GetAxis ("Vertical");
		Vector2 force = new Vector2 (0, vertical*3);
		gameObject.GetComponent<Rigidbody> ().AddForce(force);
		score.Increase (Time.deltaTime);
		gameObject.transform.Rotate((Time.deltaTime+1)*vertical*25, 0, 0, Space.World);
	}
	void EndGame(){
		score.active = false;
		print ("End Game");
		Application.LoadLevel("EndGame");
	}
}
