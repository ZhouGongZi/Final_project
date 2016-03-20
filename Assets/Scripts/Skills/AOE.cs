using UnityEngine;
using System.Collections;

public class AOE : Skill {
	
	float radius = 5;
	// Use this for initialization



	public override void Update () {
		base.Update ();
	}

	public override void useSkill(){
		base.useSkill ();
	 	explodeAOE ();
			
	}

	public void explodeAOE(){
		//make all the other things except the player stop 

		Collider[] col = Physics.OverlapSphere(this.transform.position, radius);
		int i = 0;

		print ("in the explosion!!!!!!!!!!");

		while (i < col.Length) {
			if(col[i].tag=="Enemy")
			col[i].GetComponent<Enemy> ().Health = 0;
			i++;
		}
	}

}
