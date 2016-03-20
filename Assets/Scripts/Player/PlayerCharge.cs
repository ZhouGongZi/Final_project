using UnityEngine;
using System.Collections;

public class PlayerCharge : Skill {


	
	// Update is called once per frame
	public override void Update () {
		base.Update ();
	}
	public override void useSkill ()
	{
		base.useSkill ();
		ShadowController.Instance.charge ();
	}
}
