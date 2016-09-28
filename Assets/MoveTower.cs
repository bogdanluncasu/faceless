using UnityEngine;
using System.Collections;

public class MoveTower : MonoBehaviour {
	private Vector3 force;
	private GameObject bk;
	private GameObject player;
	Transform towerTransform;
	private float y;
	float range=10f;
	public GameObject bullet;
	float coolDown=1.5f,coolDownLeft=0;


	void Start(){
		player=GameObject.Find ("Sphere");
		towerTransform=gameObject.transform.Find("Tower_Top");
		y = Time.time;
		bk = GameObject.Find ("Background");
		Revalidate ();
	}
	void Update () {
		//float d = Vector3.Distance (this.transform.position, player.transform.position);
		if (gameObject.transform.position.x < -15) {
			bk.GetComponent<OffsetScroller>().DestroyTower();
		}
		Vector3 dist = player.transform.position-towerTransform.position;
		Quaternion look = Quaternion.LookRotation (dist);
		//print (look.eulerAngles);
		towerTransform.localRotation=Quaternion.Euler(0,look.eulerAngles.y-30,0);
		coolDownLeft -= Time.deltaTime;
		if (coolDownLeft <= 0&&dist.magnitude<=range) {
			coolDownLeft=coolDown;
			Shoot();
		}
		Revalidate ();
		//towerTransform.Rotate(0,look.eulerAngles.y,0,Space.World);
	}
	private void Revalidate(){
		force = new Vector3 (-(Time.time-y)*1.5f+14.5f, gameObject.transform.position.y,0);
		gameObject.transform.position=force;
	}
	private void Shoot(){
		Instantiate (bullet, this.transform.position, this.transform.rotation);
	}
}
