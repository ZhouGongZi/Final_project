﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {
	//properties

	public bool isStop = false; // used for time stop !!!!!!!!!!!!!!!!!!!!!!!!!!!!!

	public enum enemyType{
		meleeEne,
		rangeEne
	}
	public enum enemyStatus
	{
		casual,fight,death
	}
	//enemy type
	protected enemyType _enemyType;

	public enemyType EnemyType{
		get { return _enemyType;}
		set { _enemyType = value;}
	}

	protected enemyStatus _enemyStatus;

	public enemyStatus EnemyStatus{
		get {return _enemyStatus; }
		set { _enemyStatus = value;}
	}


	//enemy health 
	protected int		 _health;

	public  int Health { 
		get {return _health;}
		set { _health = value;
			if (_health <= 0) {
				_health = 0;
				die ();
				//inform ();
			}
		}
	}

	//alert range
	public float alertRangeStd;
	public float alertRange;
	public float maxalertRange;
	public float informRange;
	//attack range public attackRange;
	public float attackRange;
	public float speed;
	//animations
	public AnimationClip idling,running,attacking,death,getHit,dancing;




	public bool informed {
		get;
		set;
	}
	[SerializeField]
	protected GameObject player;
	 //public  GameObject shadow;
	CharacterController controller;
	protected Animation Anim;


	public virtual void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
		controller = this.GetComponent < CharacterController> ();
		Anim =this.GetComponent<Animation> ();

	}


	public virtual void Update(){
		alertRange = attackRange + (ShadowController.Instance.shadows.Count+1)*4 * (alertRangeStd - attackRange) * ( (PlayerStatus.Instance.Fury *1.0f)/ 100)*((PlayerStatus.Instance.Fury*1.0f) / 100) ;
		//shadow = GameObject.FindGameObjectWithTag ("Shadow");
//		if (ChooseEnemy.Instance.target!= null) {
//			if (this.gameObject != ChooseEnemy.Instance.target.Value)
//				unchosed ();
//		}
		if (Anim.IsPlaying (death.name))
			return;

		if (Health <= 0 || Anim.IsPlaying(getHit.name))
			return;
		if (!Alert () && !InAttackRange ()) {
			idle ();

		} else if (!InAttackRange ()) {
			chase ();

		} else if ((PlayerStatus.Instance.Health>0 )&&!Anim.IsPlaying(death.name)) {
			attack ();
			inform();
		}
		else
			dance ();



	}

	//check if it should run to the player
	bool Alert(){
		if (!PlayerStatus.Instance.stealth)
			return Vector3.Distance (this.transform.position, player.transform.position) < alertRange;
		else
			return false;

			//if( ShadowHealth.Instance.stealth==true||Vector3.Distance(this.transform.position,player.transform.position)<Vector3.Distance(this.transform.position,shadow.transform.position))
		//return  (Vector3.Distance(this.transform.position,player.transform.position)<alertRange && Vector3.Dot(player.transform.position-this.transform.position,this.transform.forward)>-0.3&&!PlayerStatus.Instance.stealth);
		//else 
		//	return  (Vector3.Distance(this.transform.position,shadow.transform.position)<alertRange && Vector3.Dot(shadow.transform.position-this.transform.position,this.transform.forward)>-0.3);
	}

	//check if it is able to attack
	bool InAttackRange(){

		if (!PlayerStatus.Instance.stealth)
			return (Vector3.Distance (this.transform.position, player.transform.position) < attackRange);
		else
			return (Vector3.Distance (this.transform.position, player.transform.position) < attackRange && Vector3.Dot (player.transform.position - this.transform.position, this.transform.forward) > -0.3);

		//if( ShadowHealth.Instance.stealth==true||Vector3.Distance(this.transform.position,player.transform.position)<Vector3.Distance(this.transform.position,shadow.transform.position))
		//return (Vector3.Distance(this.transform.position,player.transform.position)<attackRange);
		//else return (Vector3.Distance(this.transform.position,shadow.transform.position)<attackRange);

	}

	//idle
	public virtual void idle(){
		print ("idle");
		//Vector3 idlerange = Vector3.Normalize( new Vector3 (Random.Range (-1, 1), 0, Random.Range (-1, 1)));
		//controller.SimpleMove (idlerange*0.5f);
		Anim.Play (dancing.name);
	}
	//chase the player
	public virtual void chase(){
		//if (  ShadowHealth.Instance.stealth == true || Vector3.Distance (this.transform.position, player.transform.position) < Vector3.Distance (this.transform.position, shadow.transform.position)) {
			this.transform.LookAt (player.transform.position);
			controller.SimpleMove (this.transform.forward * speed);
			Anim.Play (running.name);
//		} else {
//			this.transform.LookAt (shadow.transform.position);
//			controller.SimpleMove (this.transform.forward * speed);
//			Anim.Play (running.name);
//		}
	}

	public virtual void attack (){
		//if (  ShadowHealth.Instance.stealth == true || Vector3.Distance (this.transform.position, player.transform.position) < Vector3.Distance (this.transform.position, shadow.transform.position))
			this.transform.LookAt (player.transform);
//		else
//			this.transform.LookAt (shadow.transform);
		Anim.Play (attacking.name);
	}
	public virtual void die(){
		//Anim.Stop ();
		this.tag="Untagged";
		ChooseEnemy.Instance.enemy.Remove (this.gameObject);
		Anim.Play (death.name);
		ChooseEnemy.Instance.enemy.Remove (this.gameObject);
		StartCoroutine (disappear ());


	}
	public virtual void dance(){
		Anim.Play (dancing.name);
	}
	public virtual void GetHit(int damage){
		alertRange = maxalertRange;
		Health -= damage;
		PlayerStatus.Instance.AddFury(damage/2);
		if (Health <= 0)
			return;
		
		Anim [getHit.name].time = 0.15f;
		Anim.Play (getHit.name);

	}
	//once it is in the chase/attack or die mode inform the enemies around it.
	public virtual void inform(){
		GameObject[] enes = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject en in enes) {
			if (en == null)
				continue;
			if (Vector3.Distance (en.transform.position, transform.position) < informRange) {
				

				en.GetComponent<Enemy> ().alertRangeStd =en.GetComponent<Enemy>().maxalertRange;
			}
				
		}

	}
	public void chosed(){
		this.transform.localScale = new Vector3 (1.5f, 1.5f, 1.5f);
	}
	public void unchosed(){
		this.transform.localScale = new Vector3 (1f, 1f, 1f);
	}

	IEnumerator  disappear(){
		this.GetComponent<CharacterController> ().enabled = false;
		Vector3 pos= transform.position;
		while(pos.y>-0.2){
			pos.y-=0.001f;
			transform.position=pos;
			yield return null;
		}
		Destroy(this.gameObject);

	}
}
