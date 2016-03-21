using UnityEngine;
using System.Collections;

public class ShadowSpawnBattle: Skill {
	GameObject tar;
	public GameObject shadow;
	// Use this for initialization
	public override void useSkill(){
		base.useSkill ();
		tar = ChooseEnemy.Instance.target.Value;
		if (tar == null) {
			Debug.Log ("no shadow available");

		} else {
			GameObject go = Instantiate (shadow) as GameObject;
			ShadowController.Instance.shadows.Add (go);

			go.transform.position = tar.transform.position + tar.transform.forward * (-1f);
			go.transform.LookAt (tar.gameObject.transform);	
			go.GetComponent<stealthAttack> ().Opponent = tar;
//			go.GetComponent<stealthAttack> ().launchattack = true;


		}
	}
}
