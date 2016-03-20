using UnityEngine;
using System.Collections;

public class shadowFight : Skill {

	public AnimationClip 		Melee_Clip;
	bool						isWell;			
	bool 						isAttacking;

	public override void Start () {
		base.Anim = this.GetComponent<Animation> ();
	}
	public override void Update () {
		base.Update ();

		if (Anim[Melee_Clip.name].time > Anim[Melee_Clip.name].length * 0.8f && !isWell)
		{
			//make sure the shadow does not tilt !
			Vector3 temp = this.transform.localEulerAngles;
			temp.x = 0;
			temp.z = 0;
			this.transform.localEulerAngles = temp;
			isWell = true;
		}

		if (Anim [Melee_Clip.name].time > Anim [Melee_Clip.name].length * 0.90)
			isAttacking = false;
	}

	public override void useSkill(){
		base.useSkill ();
		//use the function here
		fight();

	}
		
	void fight(){
		Anim.Play (Melee_Clip.name);
		isAttacking = true;
	}

}
