﻿using UnityEngine;
using System.Collections;

public class MeleeEnemy : Enemy {
	[SerializeField]
	int attackDamage =20;
	bool attacked=false;
	float impactTime =0.1f;
	// Use this for initialization
	public override void Start () {
		base.Start ();
		this.Health = 100;
	}
	
	// Update is called once per frame
	public override void Update () {
		base.Update ();	
	
		if( attacked && Anim[attacking.name].time>Anim[attacking.name].length*0.9){
			
			attacked=false;
		}
	}
	public override void idle ()
	{
		base.idle ();
	}
	public override void chase ()
	{
		base.chase ();
	}
	public override void attack ()
	{	base.attack ();
		if( !attacked && Anim[attacking.name].time>this.GetComponent<Animation>()[attacking.name].length*impactTime&&Anim[attacking.name].time<Anim[attacking.name].length*0.9){
			//do something attack
			PlayerStatus.Instance.OnHit(attackDamage);

			Debug.Log("attack");
			attacked=true;
		}

	}
}
