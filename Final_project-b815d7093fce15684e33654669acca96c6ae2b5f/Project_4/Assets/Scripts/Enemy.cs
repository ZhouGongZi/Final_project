using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {
	//properties
	public enum enemyType{
		meleeEne,
		rangeEne
	}
	public enum enemyStatus
	{
		casual,fight
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
	protected int _health;

	public  int Health { 
		get {return _health;}
		set { _health = value;
			if (_health <= 0) {
				_health = 0;
				inform ();
				die ();
			}
		}
	}

	//alert range
	public float alertRange;
	public float maxalertRange;
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
		} else if (!InAttackRange ()) {
			chase ();
		} else if (PlayerHealth.Instance.Health > 0) {
			attack ();
			inform();
		}
		else
			dance ();



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
	public virtual void dance(){
		Anim.Play (dancing.name);
	}
	public virtual void GetHit(int damage){
		Health -= damage;

	}
	//once it is in the chase/attack or die mode inform the enemies around it.
	public virtual void inform(){
		GameObject[] enes = GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject en in enes) {
			if (Vector3.Distance (en.transform.position, transform.position) < 20)
				en.GetComponent<Enemy> ().alertRange =maxalertRange;

		}

	}

	// Use this for initialization

}
