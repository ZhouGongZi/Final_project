using UnityEngine;
using System.Collections;

public class shadowFight : Skill {

	/*
	public AnimationClip 		Melee_Clip;
	bool						isWell = false;			
	bool 						isAttacking = false;
	bool 						hasAttacked;
	bool  						viewLocked;
	private float 				impact_time = 0.4f;


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

	void OnTriggerStay(Collider other){
		if (other.tag == "Enemy" && other.GetComponent<Enemy>().Health > 0) {
			isWell = false;
			if (Anim [Melee_Clip.name].time > Anim [Melee_Clip.name].length * impact_time && !hasAttacked) {
				other.GetComponent<Enemy> ().GetHit (8);
				viewLocked = false;
				hasAttacked = true;
			}
			if (!viewLocked) {
				transform.LookAt (other.transform);
				viewLocked = true;
				hasAttacked = false;
				Anim.Play (Melee_Clip.name);
			}
		}

		if (other == null) {
			GetComponent<Transform> ().rotation = Player.instance.GetComponent<Transform> ().rotation;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Enemy")
		{
			GetComponent<Transform>().rotation = Player.instance.GetComponent<Transform>().rotation;
		}
	}*/

	public AnimationClip 		Melee_Clip;

	bool 						isAttacking = false; 
	float 						impactTime=0.40f;
	bool 						viewLock = false;
	bool 						isWell = false;

	public override void Start () {
		base.Anim = this.GetComponent<Animation> ();
	}
	// Update is called once per frame
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
		attack ();
	}

	public void attack(){
		if (viewLock) {
			Anim.Play (Melee_Clip.name);
			isWell = false;
			isAttacking = true;
		}
	}


	void OnTriggerStay(Collider other){

		if (other.tag == "Enemy") {
			transform.LookAt (other.transform);
			viewLock = true;
			if (isAttacking == true) {
				print("success!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
				if (Anim [Melee_Clip.name].time > Anim [Melee_Clip.name].length * impactTime) {
					other.GetComponent<Enemy> ().GetHit (30);
					isAttacking = false;
					viewLock = false;
				}

			}
		}
	}
	void OnTriggerExit(Collider other){
		viewLock = false;
	}


}
