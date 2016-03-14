using UnityEngine;
using System.Collections;

public class Player_modern : MonoBehaviour {
	//animation for attack
	private Animation				Anim;
	public AnimationClip 			Melee_Clip;


	//melee attack cool down
	private float 					meleeRate = 0.3f;
	private float 					nextMelee = 0.0f;
	private	bool 					isAttacking = false; 
	private float 					impactTime=0.45f;
	// Use this for initialization
	void Start () {
		Anim = this.GetComponent<Animation> ();
	}

	// Update is called once per frame
	void Update () {
		
		if (Input.GetKeyDown (KeyCode.A) && Time.time > nextMelee && !Anim.IsPlaying("death")) {
			Melee_Attack();
			nextMelee = Time.time + meleeRate;
		}
	}
	void Melee_Attack(){
		this.GetComponent<RangeAttack> ().cancel();
		Anim.Play (Melee_Clip.name);
		isAttacking = true;
	}

	/*
	void OnTriggerEnter(Collider other){
		
		if (other.tag == "Enemy") {
			//Anim.Play (Melee_Clip.name);

			//other.GetComponent<Enemy> ().GetHit (100);
		}
	}*/
		
	void OnTriggerStay(Collider other){

		if (other.tag == "Enemy") {
			if (isAttacking == true) {
				print("success!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
				if (Anim [Melee_Clip.name].time > Anim [Melee_Clip.name].length * impactTime) {
					other.GetComponent<Enemy> ().GetHit (100);
					isAttacking = false;
				}

			}
		}
	}

	/*
	void OnTriggerExit(Collider other){

		if (other.tag == "Enemy") {
			//Anim.Play (Melee_Clip.name);
			canAttack = false;
			//other.GetComponent<Enemy> ().GetHit (100);
		}
	}*/

}
