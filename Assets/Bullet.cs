using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float speed;
	public Transform target;
	public float damage=1f;
	private GameObject player;
	Vector3 dir;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Sphere");
		target = player.transform;
		dir = target.position - this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (this.transform.position.y < -2.5)
			Destroy (gameObject);
		float distThisFrame = speed * Time.deltaTime;
		transform.Translate(dir.normalized*distThisFrame,Space.World);
		this.transform.rotation=Quaternion.Lerp(this.transform.rotation,Quaternion.LookRotation(dir),Time.deltaTime*4);

	}
	void OnCollisionEnter(Collision col){
		print ("Collision");
		if (col.gameObject.name == "Sphere") {
			player.GetComponent<BallController> ().health -= damage;
			Destroy(gameObject);
		}
		Destroy (gameObject);

	}
}
