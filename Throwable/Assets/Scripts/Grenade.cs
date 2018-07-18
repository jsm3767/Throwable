using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour {

	public float fuseTime = 3;
	protected float fuseProgress = 0;
	protected bool pinPulled = false;
	protected List<GameObject> objInExplosion = new List<GameObject> ();

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public void PullPin () {
		pinPulled = true;
	}

	protected virtual void Explode () {
		DestroySelf ();
	}

	protected void DestroySelf(){
		Destroy (this.gameObject, 0.1f);
	}

	private void OnTriggerEnter (Collider c) {
		objInExplosion.Add (c.gameObject);
	}

	private void OnTriggerExit (Collider c){
		objInExplosion.Remove (c.gameObject);
	}
}
