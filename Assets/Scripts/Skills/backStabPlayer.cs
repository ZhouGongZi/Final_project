using UnityEngine;
using System.Collections;

public class backStabPlayer :Skill {

	// Use this for initialization

	
	// Update is called once per frame
	public override void Update () {
		base.Update ();

	}
	public override void useSkill ()
	{
		base.useSkill ();
		ShadowController.Instance.backstab ();
	}

}
