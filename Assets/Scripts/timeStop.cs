using UnityEngine;
using System.Collections;

public class timeStop : Skill {

	int countRecover = 0;
	// Use this for initialization
	int radius = 500;

	float fireRate = 10f;
	float nextfire = 0.0f;

	public override void Start () {
	
	}
	// Update is called once per frame
	public override void Update () {
		if (Time.time > nextfire) {
			makeFree ();
		}
	}

	public override void useSkill(){
		if (Time.time > nextfire) {
			makeStop ();
			nextfire = Time.time + fireRate;
		}
	}

	public void makeStop(){
		//make all the other things except the player stop 

		Collider[] col = Physics.OverlapSphere(gameObject.transform.position,radius, radius);
		int i = 0;

		print ("in the skill !!!!!!!!");

		while (i < col.Length) {
			col [i].GetComponent<Enemy> ().isStop = true;
			i++;
		}

	}

	void makeFree(){
		Collider[] col = Physics.OverlapSphere(gameObject.transform.position,radius, radius);
		int j = 0;
		while (j < col.Length) {
			col [j].GetComponent<Enemy> ().isStop = false;
			j++;
		}
	}

}
