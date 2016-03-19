using UnityEngine;
using System.Collections;

public class Plain_attack : Skill {
	
	public AnimationClip 		Melee_Clip;

	float 						fireRate = 0.3f;
	float 						nextfire = 0.0f;

	bool 						isAttacking = false; 
	float 						impactTime=0.40f;


	public override void Start () {
		base.Anim = this.GetComponent<Animation> ();
	}
	// Update is called once per frame
	public override void Update () {
		if (Anim [Melee_Clip.name].time > Anim [Melee_Clip.name].length * 0.90)
			isAttacking = false;
	}

	public override void useSkill(){
		if (Time.time > nextfire) {
			attack ();
			nextfire = Time.time + fireRate;
		}
	}

	public void attack(){
		Anim.Play (Melee_Clip.name);
		isAttacking = true;
	}


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

}
