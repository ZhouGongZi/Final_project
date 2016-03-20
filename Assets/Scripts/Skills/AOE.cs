using UnityEngine;
using System.Collections;

public class AOE : Skill {
	
	int radius = 500;
	// Use this for initialization
	float fireRate = 10f;
	float nextfire = 0.0f;

	void Start () {
		
	}

	public override void Update () {
		base.Update ();
	}

	public override void useSkill(){
		base.useSkill ();
		if (Time.time > nextfire) {
			explodeAOE ();
			nextfire = Time.time + fireRate;
		}
	}

	public void explodeAOE(){
		//make all the other things except the player stop 

		Collider[] col = Physics.OverlapSphere(this.transform.position, radius);
		int i = 0;

		print ("in the explosion!!!!!!!!!!");

		while (i < col.Length) {
			col [i].GetComponent<Enemy> ().Health = 0;
			i++;
		}
	}

}
