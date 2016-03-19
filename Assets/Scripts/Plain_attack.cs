using UnityEngine;
using System.Collections;

public class Plain_attack : Skill {
	
	public AnimationClip 		MeleeClip;

	float 						fireRate = 0.3f;
	float 						nextfire = 0.0f;

	bool 						isAttacking = false; 
	float 						impactTime=0.40f;


	public override void Start () {
		base.Anim = this.GetComponent<Animation> ();
	}
	// Update is called once per frame
	public override void Update () {
		
	}

	public override void useSkill(){
		if (Time.time > nextfire) {
			attack ();
			nextfire = Time.time + fireRate;
		}
	}

	public void attack(){
		
	}

}
