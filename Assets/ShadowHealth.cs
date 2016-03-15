using UnityEngine;
using System.Collections;

public class ShadowHealth : MonoBehaviour {
	public static ShadowHealth Instance;



	public AnimationClip death,getHit;
	public bool stealth=true;

	Animation Anim;
	int _health=100;

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
		stealth = true;
	}

	void Update(){
		if (Anim [death.name].time > Anim [death.name].length * 0.95)
			this.gameObject.SetActive (false);

	}
	public void OnHit(int damage){
		Health -= damage;
		this.GetComponent<RangeAttack> ().cancel();

		if(!Anim.IsPlaying(death.name)&&!Anim.IsPlaying("attack 1"))
			Anim.Play(getHit.name);


	}
	void die(){
		Anim.Play(death.name);

			

	}


}
