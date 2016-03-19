using UnityEngine;
using System.Collections;

public class controllerSkill : MonoBehaviour {
	public Skill backstab;
	public Skill rangeattack;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E)){
			backstab.useSkill ();
		}
		if (Input.GetKeyDown (KeyCode.Q)) {
			rangeattack.useSkill ();
			ShadowController.Instance.fireball ();
		}
	}
}
