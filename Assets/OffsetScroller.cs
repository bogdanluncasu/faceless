﻿using UnityEngine;
using System.Collections;

public class OffsetScroller : MonoBehaviour {
	public float speed;
    private Vector2 savedOffset;
	public GameObject player;
	private GameObject enemy;
	public bool set;
    void Start () {
		savedOffset = GetComponent<Renderer>().sharedMaterial.GetTextureOffset ("_MainTex");
    }
	void Update () {                                      
		if (!set) {
			enemy=GameObject.Instantiate (Resources.Load("Tower")) as GameObject;
			set = true;
		} 
		player.transform.Rotate(0, -(Time.deltaTime+1)*speed*25, 0, Space.World);
		float f=Mathf.Repeat (Time.time * speed, 1);
		Vector2 v = new Vector2 (savedOffset.x,f);
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex",-v);
	}
	void OnDisable () {
		GetComponent<Renderer>().sharedMaterial.SetTextureOffset ("_MainTex", savedOffset);
	} 
	public void DestroyTower(){
		Destroy (enemy);
		set = false;
	}
	
}
