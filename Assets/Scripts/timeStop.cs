using UnityEngine;
using System.Collections;

public class timeStop : Skill {

	// Use this for initialization
	public override void Start () {
	
	}
	
	// Update is called once per frame
	public override void Update () {
	
	}

	void makeStop(Vector3 center, float radius){
		Collider[] col = Physics.OverlapSphere(center, radius);
		int i=0;
		while (i < col.Length) {

			col [i].GetComponent<Enemy> ().isStop = true;
			i++;
		}
	}

}
