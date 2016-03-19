using UnityEngine;
using System.Collections;

public class RangeAttack : MonoBehaviour {


	public AnimationClip	 cast;
	public float 			speed;
	public Vector3 			offset1,offset2,offset3;
    public GameObject spawnPoint;


	GameObject shadow;
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
		//shadow = GameObject.FindGameObjectWithTag ("Shadow");

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

            go.GetComponent<TweenPosition>().from = this.transform.position+offset1;
            go.GetComponent<TweenPosition>().to = this.transform.position + offset2;
			casted = true;
		}
		if (Anim [cast.name].time > Anim [cast.name].length * impactTime && !speedset) {

			if (this.gameObject.tag == "Shadow") {

				Vector3 direction= calfocus ();
				if (direction == Vector3.zero) {
					go.GetComponent<Rigidbody> ().velocity = this.transform.forward*speed;
				} else {
					Vector3 vel = Vector3.Normalize (direction - (this.transform.position + offset2)) * speed;
					vel.y = 0f;
					go.GetComponent<Rigidbody> ().velocity = vel;
					go.transform.LookAt (direction);
				}



			} else if (this.gameObject.tag == "Player") {
				go.GetComponent<Rigidbody> ().velocity = transform.forward * speed;
                Vector3 direction = calfocus();
                go.transform.LookAt(direction);
            }
				

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

	Vector3 calfocus(){
		RaycastHit hit;
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
        //Debug.DrawRay(player.transform.position + offset3, player.transform.forward, Color.blue);
		if (Physics.Raycast (player.transform.position+offset3, player.transform.forward, out hit, 1000f)) {
			//this.transform.LookAt (hit.transform.position);
			Debug.DrawRay (player.transform.localPosition + offset3, player.transform.forward,Color.blue);
			Debug.Log (player.transform.forward);
			Debug.Log (hit.transform.position);
			return hit.transform.position;


		} else
			return Vector3.zero;
	

	}
}
