using UnityEngine;
using System.Collections;

public class MeleeEnemy : Enemy {
	[SerializeField]
	int attackDamage =20;
	bool attacked=false;
	float impactTime =0.1f;
	// Use this for initialization
	public override void Start () {
		base.Start ();
		this.Health = 20;
	}
	
	// Update is called once per frame
	public override void Update () {
		if (isStop) // used for time stop !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
			return;
		
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
			//if (shadow == null || ShadowHealth.Instance.stealth == true || Vector3.Distance (this.transform.position, player.transform.position) < Vector3.Distance (this.transform.position, shadow.transform.position))
                PlayerStatus.Instance.OnHit (attackDamage);
//			else
//				ShadowHealth.Instance.OnHit (attackDamage);
			
			attacked=true;
		}

	}

}
