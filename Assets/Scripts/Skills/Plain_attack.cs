using UnityEngine;
using System.Collections;

public class Plain_attack : Skill {
	
	public AnimationClip 		Melee_Clip;
	public int 					damage;
	bool 						isAttacking = false; 
	float 						impactTime = 0.40f;




	public override void Start () {
		base.Anim = this.GetComponent<Animation> ();
	}
	// Update is called once per frame
	public override void Update () {
		base.Update ();
		if (Anim [Melee_Clip.name].time > Anim [Melee_Clip.name].length * 0.90)
			isAttacking = false;
	}

	public override void useSkill(){
		base.useSkill ();
		attack ();
	
	}

	public void attack(){
		//Anim.Play (Melee_Clip.name);

		PlayerControl.instance.anim_control.ChangeState (new AnimState (Melee_Clip.name, PlayerControl.instance.anim_control._current_state.player_state, PlayerState.attacking));
		Debug.Log ("attacked pushed");
		PlayerControl.instance.anim_control.Update ();


		isAttacking = true;
	}


	void OnTriggerStay(Collider other){

		if (other.tag == "Enemy") {
			if (isAttacking == true) {
				print("success!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
				if (Anim [Melee_Clip.name].time > Anim [Melee_Clip.name].length * impactTime) {
					other.GetComponent<Enemy> ().GetHit (damage);
					isAttacking = false;
				}

			}
		}
	}

}
