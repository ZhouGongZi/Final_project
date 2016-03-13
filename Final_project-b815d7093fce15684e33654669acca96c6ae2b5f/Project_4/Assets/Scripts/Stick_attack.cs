using UnityEngine;
using System.Collections;

public class Stick_attack : MonoBehaviour {

	private float 	attack_value = 5f;
	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	void onTriggerEnter(Collider other){
		if (other.tag == "Enemy") {
			other.GetComponent<Enemy>().GetHit(5);
			//other.gameObject.onhit (attack_value);
		}
	}
}