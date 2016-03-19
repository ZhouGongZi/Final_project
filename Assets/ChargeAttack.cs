using UnityEngine;
using System.Collections;

public class ChargeAttack : MonoBehaviour {
	GameObject tar;
	CharacterController cc;
	public bool charge=false;
	public float speed=10f;
	public AnimationClip running;
	Animation Anim;
	public bool attacked=false;


	// Use this for initialization
	void Awake () {
		Anim = this.GetComponent<Animation> ();
		cc = this.GetComponent<CharacterController> ();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log (Vector3.Distance (this.transform.position, tar.transform.position));
		if (tar == null)
			return;
		if (charge == true && Vector3.Distance (this.transform.position, tar.transform.position) > 1.5f)
			chase ();
		else if(Vector3.Distance (this.transform.position, tar.transform.position) < 1.5f&&!attacked){
			this.GetComponent<stealthAttack> ().Opponent = tar;
			this.GetComponent<stealthAttack> ().launchattack = true;
			attacked = true;
		}
			
		

	}
	public void setTarget(GameObject target){
		tar = target;
	}
	void chase(){
		this.transform.LookAt (tar.transform);
		cc.SimpleMove (this.transform.forward * speed);
		Anim.Play (running.name);


	}

}
