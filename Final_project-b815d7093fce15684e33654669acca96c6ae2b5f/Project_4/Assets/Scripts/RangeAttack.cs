using UnityEngine;
using System.Collections;

public class RangeAttack : MonoBehaviour {


	public AnimationClip	 cast;
	public float 			speed;
	public Vector3 			offset1,offset2;



	GameObject fireBall;
	GameObject go;
	Animation Anim;
	float impactTime=0.65f;
	float nextCast=0.0f;
	float intervalCast = 2.01f;
	bool casted=false;
	bool speedset=false;
	// Use this for initialization
	void Awake () {
		fireBall = Resources.Load ("fireBall") as GameObject;
		Anim = this.GetComponent<Animation> ();

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Q)&&Time.time>nextCast) {
			Anim.Play (cast.name);
			casted = false;
			speedset = false;
			nextCast = Time.time + intervalCast;
		}
		castFire ();
	}

	void castFire(){
		if (Anim [cast.name].time >0.1f && casted ==false) {
			go = Instantiate (fireBall) as GameObject;

			go.GetComponent<TweenPosition> ().from = this.transform.position+offset1;
			go.GetComponent<TweenPosition> ().to = this.transform.position + offset2;
			casted = true;
		}
		if (Anim [cast.name].time > Anim [cast.name].length * impactTime && !speedset) {
			go.GetComponent<Rigidbody> ().velocity = transform.forward * speed;
			speedset = true;

		}
	}
	public void movement(){
		go.GetComponent<Rigidbody> ().velocity = transform.forward * speed;
	}
	public void cancel(){
		if (Anim.IsPlaying (cast.name))
			Destroy (go);
	}
}
