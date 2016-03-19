using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShadowController : MonoBehaviour {
	public GameObject [] shadows;
	public static ShadowController Instance;
	// Use this for initialization
	void Awake () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		GetShadows ();
	}

	void GetShadows(){
		GameObject[] shadows = GameObject.FindGameObjectsWithTag ("Shadow");

	}
	public void fireball(){
		foreach (GameObject sha in shadows) {
			RangeAttack range = sha.GetComponent<RangeAttack> ();
			range.useSkill ();
		}



	}


}
