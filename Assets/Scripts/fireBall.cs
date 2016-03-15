using UnityEngine;
using System.Collections;

public class fireBall : MonoBehaviour {
	bool damaged = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
		
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Enemy"&&!damaged) {
			col.gameObject.GetComponent<Enemy> ().GetHit (10);
			damaged = true;
			Destroy (this.gameObject);
		}
	}
}
