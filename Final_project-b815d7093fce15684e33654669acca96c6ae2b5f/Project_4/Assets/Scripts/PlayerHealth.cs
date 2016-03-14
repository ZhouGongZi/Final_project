﻿using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	public static PlayerHealth Instance;

	int _health=100;
	public AnimationClip death,getHit;
	Animation Anim;

	public int Health {
		get { return _health;}
		set {_health = value;
			if (_health <= 0) {
				_health = 0;
				die ();
			}
		}
	}
	// Use this for initialization
	void Awake(){
		Instance = this;
		Anim = this.GetComponent<Animation> ();
	}
	public void OnHit(int damage){
		Health -= damage;

		if(!Anim.IsPlaying(death.name)&&!Anim.IsPlaying("attack 1"))
			Anim.Play(getHit.name);
		

	}
	void die(){
		Anim.Play(death.name);

	}

	void OnTriggerEnter (Collider col){
		if (col.gameObject.tag == "Enemy")
			Debug.Log ("hit enemy");
	}
}
