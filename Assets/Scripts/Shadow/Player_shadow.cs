using UnityEngine;
using System.Collections;

public class Player_shadow : MonoBehaviour {

	public bool 					isShadowFree = false; 

	private Animation				Anim;
	public AnimationClip 			Melee_Clip;
	private float 					impact_time = 0.4f;
	private float 					setRotateTime = 0.7f;

	private bool 					viewLocked = false;
	private bool 					hasAttacked = false;
	private float 					nextMelee = 0.0f;
	private float 					meleeRate;

	private bool 					isAttacking = false;
	public bool                     hasEnemyInRange = false;
	private bool                    isWell = false;
	// Use this for initialization
	void Start () {
		Anim = GetComponent<Animation> ();
		meleeRate = Anim [Melee_Clip.name].length;
	}

	// Update is called once per frame
	void Update () {
		/*if (Anim [Melee_Clip.name].time > Anim [Melee_Clip.name].length * setRotateTime) {
			this.transform.localEulerAngles = new Vector3 (0, 180, 0);
		}*/
		if (Anim[Melee_Clip.name].time > Anim[Melee_Clip.name].length * 0.8f && !isWell)
		{
			Vector3 temp = this.transform.localEulerAngles;
			temp.x = 0;
			temp.z = 0;
			this.transform.localEulerAngles = temp;
			isWell = true;
		}

		if (isShadowFree) {

		} else {
			if (Input.GetKeyDown (KeyCode.K) && Time.time > nextMelee && !Anim.IsPlaying("death")) {
				Melee_Attack();
				nextMelee = Time.time + meleeRate;
			}
			if (Anim [Melee_Clip.name].time > Anim [Melee_Clip.name].length * 0.90)
				isAttacking = false;
		}
	}

	void Melee_Attack(){
		Anim.Play (Melee_Clip.name);
		isAttacking = true;
	}


	public void onChangeShadowFollow(){
		//when change the state of follow, reset these variable
		isShadowFree = false; 
		hasAttacked = false;
		viewLocked = false;
		isAttacking = false;
	}

	void OnTriggerStay(Collider other){
		if (other.tag == "Enemy" && other.GetComponent<Enemy>().Health > 0) {
			hasEnemyInRange = true;
			isWell = false;
			if (isShadowFree) {
				if (Anim [Melee_Clip.name].time > Anim [Melee_Clip.name].length * impact_time && !hasAttacked) {
					other.GetComponent<Enemy> ().GetHit (8);
					viewLocked = false;
					hasAttacked = true;
				}
				if (!viewLocked && Time.time > nextMelee) {
					nextMelee = Time.time + meleeRate;
					transform.LookAt (other.transform);
					viewLocked = true;
					hasAttacked = false;
					Anim.Play (Melee_Clip.name);
				}
			} else {
				if (isAttacking == true) {
					print("success!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
					transform.LookAt (other.transform);
					viewLocked = true;
					if (Anim [Melee_Clip.name].time > Anim [Melee_Clip.name].length * impact_time) {
						other.GetComponent<Enemy> ().GetHit (8);
						isAttacking = false;
					}

				}
			}
		}
		if (other == null) {
			hasEnemyInRange = false;
			GetComponent<Transform> ().rotation = Player.instance.GetComponent<Transform> ().rotation;
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.tag == "Enemy")
		{
			hasEnemyInRange = false;
			GetComponent<Transform>().rotation = Player.instance.GetComponent<Transform>().rotation;
		}
	}

}
