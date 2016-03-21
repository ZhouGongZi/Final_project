using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class switchposition : Skill {
	public GameObject target;
	int index=0;
	// Use this for initialization
	public override void Start () {
		base.Start ();

	}


	// Update is called once per frame
	public override void useSkill ()
	{
		target = ShadowController.Instance.shadows [index];
		if ((index+1) == ShadowController.Instance.shadows.Count)
			index = 0;
		else
			index++;
		Vector3 temp = this.transform.position;
		this.transform.position = target.transform.position;
		target.transform.position = temp;
		this.transform.LookAt (target.transform);


	}
}
