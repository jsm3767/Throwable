using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour {

	[SerializeField] GameObject grenadePrefab;
	[SerializeField] Camera camera;
	[SerializeField] float throwStrength = 30;
	bool fireReady = true;
	private GameObject activeGrenade;


	// Use this for initialization
	void Start () {
		camera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Fire1") == 1 && fireReady) {
			GameObject grenadeSpawn = GameObject.Instantiate (grenadePrefab);
			activeGrenade = grenadeSpawn;
			activeGrenade.transform.position = camera.transform.position;
			activeGrenade.transform.position += camera.transform.forward * 2;
			activeGrenade.transform.forward = camera.transform.forward;
			fireReady = false;
			PulseGrenade pg = grenadeSpawn.GetComponent<PulseGrenade> ();
			if (pg) {
				pg.PullPin ();
			}
		} else if (Input.GetAxis ("Fire1") == 0 && !fireReady){
			fireReady = true;
			Rigidbody grenadeRB = activeGrenade.GetComponent<Rigidbody> ();
			grenadeRB.velocity = activeGrenade.transform.forward * throwStrength;
		} else if (Input.GetAxis ("Fire1") == 1 && !fireReady){
			activeGrenade.transform.position = camera.transform.position;
			activeGrenade.transform.position += camera.transform.forward * 2;
			activeGrenade.transform.forward = camera.transform.forward;
		}
	}
}
