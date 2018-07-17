using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseGrenade : MonoBehaviour {

	float radius = 15;
	[SerializeField] float pushForce = 10;
	[SerializeField] SphereCollider explosionRadius;
	List<GameObject> objInExplosion;
	[SerializeField] float fuseTime = 3;
	float fuseProgress = 0;
	bool pinPulled = false;

	// Use this for initialization
	void Start () {
		objInExplosion = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (pinPulled) {
			fuseProgress += Time.deltaTime;
		}
		if (fuseProgress >= fuseTime) {
			Explode ();
		}
	}

	public void PullPin () {
		print ("Pin pulled");
		pinPulled = true;
	}

	void Explode () {
		print ("Boom");
		foreach (GameObject go in objInExplosion) {
			if (!go) {
				break;
			}
			Rigidbody objRb = go.GetComponent<Rigidbody> ();
			if (objRb) {
				Vector3 fromGrenadeToObj = go.transform.position - this.gameObject.transform.position;
				fromGrenadeToObj.Normalize ();
				objRb.velocity += fromGrenadeToObj * pushForce;
			}
		}
		Destroy (this.gameObject, 0.1f);
	}

	void OnTriggerEnter (Collider c) {
		objInExplosion.Add (c.gameObject);
	}

	void OnTriggerExit (Collider c){
		objInExplosion.Remove (c.gameObject);
	}
}
