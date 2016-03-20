using UnityEngine;
using System.Collections;

public class ChargeAttack : Skill {
	GameObject tar;
	CharacterController cc;
	public bool charge=false;
	public float speed=10f;
	public AnimationClip running;
	public bool attacked=false;


	// Use this for initialization
	void Awake () {
		Anim = this.GetComponent<Animation> ();
		cc = this.GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update ();
		if (tar == null)
			return;
		if (Vector3.Distance (this.transform.position, tar.transform.position) < 1 && !attacked) {
			this.transform.LookAt (tar.transform);
			this.GetComponent<stealthAttack> ().Opponent = tar;
			this.GetComponent<stealthAttack> ().launchattack = true;
			attacked = true;
		} else if (!attacked&&charge == true && Vector3.Distance (this.transform.position, tar.transform.position) >1f)
			chase ();
	
			
		

	}

	public override void useSkill ()
	{
		base.useSkill ();
		charge = true;
	}
	public void setTarget(GameObject target){
		tar = target;
	}
	void chase(){
		this.transform.LookAt (tar.transform);
		cc.SimpleMove (this.transform.forward * speed);
		Anim.Play (running.name);


	}

}
