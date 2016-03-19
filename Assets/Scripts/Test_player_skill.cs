using UnityEngine;
using System.Collections;

public class Test_player_skill : MonoBehaviour {

	public Skill test;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.P)) {
			test.useSkill ();
		}
	}
}
