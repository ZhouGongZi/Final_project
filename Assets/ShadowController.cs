using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShadowController : MonoBehaviour {
	
	public static ShadowController Instance;
	GameObject tar;
	public List<GameObject> shadows = new List<GameObject> ();
	// Use this for initialization
	void Awake () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (ChooseEnemy.Instance.target != null)
			tar = ChooseEnemy.Instance.target.Value;
	}
//
//	void GetShadows(){
//		GameObject[] shadows = GameObject.FindGameObjectsWithTag ("Shadow");
//
//	}
	public void fireball(){
		foreach (GameObject sha in shadows) {
			sha.transform.LookAt (tar.transform);
			RangeAttack range = sha.GetComponent<RangeAttack> ();
			range.useSkill ();
		}
	}
	public void charge(){
		foreach (GameObject sha in shadows) {
			sha.transform.LookAt (tar.transform);
			ChargeAttack charge = sha.GetComponent<ChargeAttack> ();
			charge.setTarget (tar);
			charge.charge = true;
			charge.attacked = false;
		}

	}


}
