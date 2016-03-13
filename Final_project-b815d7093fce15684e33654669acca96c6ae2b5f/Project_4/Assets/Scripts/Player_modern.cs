using UnityEngine;
using System.Collections;

public class Player_modern : MonoBehaviour {
	//animation for attack
	private Animation				Anim;
	public AnimationClip 			Melee_Clip;


	//melee attack cool down
	private float 					meleeRate = 0.3f;
	private float 					nextMelee = 0.0f;

	// Use this for initialization
	void Start () {
		Anim = this.GetComponent<Animation> ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.A) && Time.time > nextMelee) {
			Melee_Attack();
			nextMelee = Time.time + meleeRate;
		}
	}
	void Melee_Attack(){
		Anim.Play (Melee_Clip.name);
	}
}
