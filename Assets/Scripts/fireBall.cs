using UnityEngine;
using System.Collections;

public class fireBall : MonoBehaviour {
	public float speed=20f;
	bool damaged = false;
	GameObject _target;
	bool _movement=false;
	Rigidbody rig;
	Vector3 offset = new Vector3 (0.0f, 0.9f, 0.0f);
	public bool Movement{
		get{ return _movement;}
		set{ _movement = value;}

	}
	// Use this for initialization
	void Start () {
		rig = this.GetComponent<Rigidbody> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Movement)
			move ();
	}
		
	void OnTriggerEnter(Collider col){
		if (col.gameObject.tag == "Enemy"&&!damaged) {
			col.gameObject.GetComponent<Enemy> ().GetHit (10);
			damaged = true;
			Destroy (this.gameObject);
		}
	}


	public void SetTarget(GameObject tar){
		_target = tar;
	}
	void move(){
		rig.velocity=Vector3.Normalize(_target.transform.position+offset-this.transform.position)*speed;
	}
}
