using UnityEngine;
using System.Collections;

public class AOE : Skill {
	
	float radius = 500;
	// Use this for initialization
	float explodeSpeed = 0.1f;

	public override void Update () {
		base.Update ();
	}

	public override void useSkill(){
		base.useSkill ();
	 	explodeAOE ();
			
	}

	public void explodeAOE(){
		//make all the other things except the player stop 


		Collider[] col = Physics.OverlapSphere(transform.position, radius);
		int i = 0;

		while (i < col.Length) {

			if (col [i].tag == "Enemy") {
				float dist = Vector3.Distance(col[i].gameObject.transform.position, transform.position);

				//zeroHealth (col[i], dist * explodeSpeed);

				StartCoroutine(zeroHealth(col[i], dist*explodeSpeed));

			}
			i++;
		}
	}

	IEnumerator zeroHealth(Collider co, float delay){
		yield return new WaitForSeconds(delay);

		co.GetComponent<Enemy> ().Health = 0;
	}

}
