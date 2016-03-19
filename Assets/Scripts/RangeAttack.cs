using UnityEngine;
using System.Collections;

public class RangeAttack : Skill {


	public AnimationClip	 cast;
	public float 			speed;

	Vector3 			offset1=new Vector3 (0.2f,0.9f,0.4f);
	Vector3 			offset2=new Vector3 (0.2f,1.2f,0.4f);


	GameObject shadow;
	GameObject fireBall;
	GameObject go;
	GameObject tar=null;


	float impactTime=0.65f;
	float nextCast=0.0f;
	float intervalCast = 2.01f;
	bool launch;
	bool casted=false;
	bool speedset=false;

	// Use this for initialization
	void Awake () {
		fireBall = Resources.Load ("fireBall") as GameObject;
		Anim = this.GetComponent<Animation> ();
		//shadow = GameObject.FindGameObjectWithTag ("Shadow");

	}


	public override void useSkill ()
	{
		launch = true;
	}



	// Update is called once per frame
	public override void Update () {
		if(ChooseEnemy.Instance.target!=null)
			tar = ChooseEnemy.Instance.target.Value;
		if (launch&&Time.time>nextCast&&(tar!=null)) {
			Anim.Play (cast.name);
			casted = false;
			nextCast = Time.time + intervalCast;
			launch = false;
			speedset = false;
		}
		castFire ();
	}

	void castFire(){

		if (tar == null) {
			Debug.Log ("no target availbale");
			return;
		}  else {

			if (Anim [cast.name].time > 0.1f && casted == false) {
				go = Instantiate (fireBall) as GameObject;

				go.GetComponent<TweenPosition> ().from = this.transform.position + offset1;
				go.GetComponent<TweenPosition> ().to = this.transform.position + offset2;
				casted = true;
			}
			if (Anim [cast.name].time > Anim [cast.name].length * impactTime && !speedset) {
				go.GetComponent<fireBall> ().SetTarget (tar);
				go.GetComponent<fireBall> ().Movement = true;
				speedset = true;
				casted = true;

			} 



			}  


		}


	public void cancel(){
		if (Anim.IsPlaying (cast.name))
			Destroy (go);
	}

	//	Vector3 calfocus(){
	//		RaycastHit hit;
	//		GameObject player = GameObject.FindGameObjectWithTag ("Player");
	//        //Debug.DrawRay(player.transform.position + offset3, player.transform.forward, Color.blue);
	//		if (Physics.Raycast (player.transform.position+offset3, player.transform.forward, out hit, 1000f)) {
	//			//this.transform.LookAt (hit.transform.position);
	//			Debug.DrawRay (player.transform.localPosition + offset3, player.transform.forward,Color.blue);
	//			Debug.Log (player.transform.forward);
	//			Debug.Log (hit.transform.position);
	//			return hit.transform.position;
	//
	//
	//		}  else
	//			return Vector3.zero;
	//	
	//
	//	}
}