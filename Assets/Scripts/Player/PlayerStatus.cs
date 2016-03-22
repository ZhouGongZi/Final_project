using UnityEngine;
using System.Collections;

public class PlayerStatus : MonoBehaviour {
	public static PlayerStatus Instance;
	[SerializeField]
	int _health=100;
	int _fury =50;
	public AnimationClip death,getHit;
	Animation Anim;
	[SerializeField]
	public bool stealth;
	[SerializeField]
	bool maxFury ;
	float maxtime=0;
	float delaytime=10f;


	public int Health {
		get { return _health;}
		set {_health = value;
			if (_health <= 0) {
				_health = 0;
				die ();
			}
		}
	}


	public int Fury{
		get {return _fury; }
		set{ 

			if (value >= 100)
				_fury = 100;
			else if (value <= 0)
				_fury = 0;
			else
				_fury = value;
			//Debug.Log (Fury);
			if (_fury == 0)
				stealth = true;
			else
				stealth = false;
		}

	}

	// Use this for initialization
	void Awake(){
		Instance = this;
		Anim = this.GetComponent<Animation> ();
	}
	void Start(){
		InvokeRepeating ("timeUpdate", 1f, 2f);

	}


	void FixedUpdate(){
		


	}

	public void OnHit(int damage){
		Health -= damage;
		this.GetComponent<RangeAttack> ().cancel();
		AddFury (damage);
		if(!Anim.IsPlaying(death.name)&&!Anim.IsPlaying("attack 5"))
			Anim.Play(getHit.name);
		

	}
	void die(){
		Anim.Play(death.name);

	}

	void timeUpdate(){
		if (Fury == 100 && !maxFury) {
			maxFury = true;
			maxtime = Time.time;
			return;
		} 

		if (maxFury) {
			if (Time.time - delaytime > maxtime) {
				Fury -= 5;
				maxFury = false;

			}
		
		} else
			Fury -= 5;
	}

	public void AddFury(int damage){
		Fury +=damage;
	}
}
