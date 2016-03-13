using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {
	//properties
	public enum enemyType{
		meleeEne,
		rangeEne
	}

	//enemy type
	protected enemyType _enemyType;

	public enemyType EnemyType{
		get { return _enemyType;}
		set { _enemyType = value;}
	}

	//enemy health 
	protected int _health;

	public  int Health { 
		get {return _health;}
		set { _health = value;
			if (_health <= 0) {
				_health = 0;
				die ();
			}
		}
	}

	//alert range
	public float alertRange;
	//attack range public attackRange;
	public float attackRange;
	public float speed;
	//animations
	public AnimationClip idling,running,attacking,death,getHit;

	[SerializeField]
	GameObject player;
	CharacterController controller;
	protected Animation Anim;


	public virtual void Start(){
		player = GameObject.FindGameObjectWithTag ("Player");
		controller = this.GetComponent < CharacterController> ();
		Anim =this.GetComponent<Animation> ();
	}
		

	public virtual void Update(){
		if (!Alert () && !InAttackRange ()) {
			idle ();
		} else if (!InAttackRange ())
			chase ();
		else
			attack ();



	}

	//check if it should run to the player
	bool Alert(){
		return  (Vector3.Distance(this.transform.position,player.transform.position)<alertRange);

	}

	//check if it is able to attack
	bool InAttackRange(){
		return (Vector3.Distance(this.transform.position,player.transform.position)<attackRange);

	}

	//idle
	public virtual void idle(){

		Anim.Play (idling.name);
	}
	//chase the player
	public virtual void chase(){
		this.transform.LookAt (player.transform.position);
		controller.SimpleMove (this.transform.forward*speed);
		Anim.Play (running.name);
	}

	public virtual void attack (){
		this.transform.LookAt (player.transform);
		Anim.Play (attacking.name);
	}
	public virtual void die(){
		Anim.Play (death.name);
		if (Anim [death.name].time > 0.95 * Anim [death.name].length)
			Destroy (this.gameObject);
	}

	public virtual void GetHit(int damage){
		Health -= damage;

	}
	// Use this for initialization

}
