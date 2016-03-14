using UnityEngine;
using System.Collections;

public class Player_shadow : MonoBehaviour {

	bool viewLocked = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerStay(Collider other){
		if (other.tag == "Enemy" && !viewLocked) {
			transform.LookAt (other.transform);
			viewLocked = true;

		}
	}

}
