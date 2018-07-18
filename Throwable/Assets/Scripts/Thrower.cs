using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thrower : MonoBehaviour {

	[SerializeField] GameObject grenadePrefab;
	[SerializeField] Camera camera;
	[SerializeField] float throwStrength = 3000;
	bool fireReady = true;
	bool fireOverride = false;
	private GameObject grenadeOBJ;
	private Grenade grenade;

	// Use this for initialization
	void Start () {
		camera = Camera.main;
		GameObject grenadeSpawn = GameObject.Instantiate (grenadePrefab);
		grenadeOBJ = grenadeSpawn;
		grenade = grenadeOBJ.GetComponent<Grenade> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Fire1") == 0 && fireReady) {
			grenadeOBJ.transform.position = camera.transform.position;
			grenadeOBJ.transform.forward = camera.transform.forward;
			Vector3 offset = new Vector3 ();
			offset += (0.75f * grenadeOBJ.transform.right) - (0.75f * grenadeOBJ.transform.up) + grenadeOBJ.transform.forward;
			grenadeOBJ.transform.position = grenadeOBJ.transform.position + offset;

		} else if (Input.GetAxis ("Fire1") == 1 && fireReady) {
			grenadeOBJ.transform.position = camera.transform.position;
			grenadeOBJ.transform.position += camera.transform.forward * 1;
			grenadeOBJ.transform.forward = camera.transform.forward;
			grenade.PullPin ();
			fireReady = false;

		} else if (Input.GetAxis ("Fire1") == 1 && !fireReady){
			if (grenadeOBJ) {
				grenadeOBJ.transform.position = camera.transform.position;
				grenadeOBJ.transform.position += camera.transform.forward * 1;
				grenadeOBJ.transform.forward = camera.transform.forward;
			} else {
				fireOverride = true;
			}
		} else if ((Input.GetAxis ("Fire1") == 0 && !fireReady) || fireOverride ){
			if (grenadeOBJ) {
				Rigidbody grenadeRB = grenadeOBJ.GetComponent<Rigidbody> ();
				grenadeRB.velocity = grenadeOBJ.transform.forward * throwStrength;
			}
			fireReady = true;
			GameObject grenadeSpawn = GameObject.Instantiate (grenadePrefab);
			grenadeOBJ = grenadeSpawn;
			grenade = grenadeOBJ.GetComponent<Grenade> ();
			fireOverride = false;
		}
	}
}
