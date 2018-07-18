using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PulseGrenade : Grenade {

	float radius = 15;
	[SerializeField] float pushForce = 10;
	[SerializeField] SphereCollider explosionRadius;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (pinPulled) {
			fuseProgress += Time.deltaTime;
			if (fuseProgress >= fuseTime) {
				Explode ();
			}
		}
	}

	protected override void Explode () {
		pinPulled = !pinPulled;
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
		DestroySelf ();
	}
}
