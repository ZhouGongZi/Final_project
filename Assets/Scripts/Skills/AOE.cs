using UnityEngine;
using System.Collections;

public class AOE : Skill {
	public int damage;
	public float radius = 8;
	// Use this for initialization
	float explodeSpeed = 0.1f;
	GameObject AOEBALL;

	public override void Start ()
	{
		base.Start ();
		AOEBALL = Resources.Load ("AOEBALL") as GameObject;
	}

	public override void Update () {
		base.Update ();
	}

	public override void useSkill(){
		base.useSkill ();
	 	explodeAOE ();
			
	}

	public void explodeAOE(){
		//make all the other things except the player stop 
		GameObject go =Instantiate(AOEBALL)as GameObject;
		Vector3 pos = this.transform.position;
		pos.y = 0;
		go.transform.position = pos;
		go.GetComponent<TweenScale> ().to = new Vector3 (radius, radius, radius);
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

		co.GetComponent<Enemy> ().GetHit(damage);
	}

}
