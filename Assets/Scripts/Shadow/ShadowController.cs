using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShadowController : MonoBehaviour {
	
	public static ShadowController Instance;
	GameObject tar;
	public List<GameObject> shadows = new List<GameObject> ();
	// Use this for initialization
	void Awake () {
		Instance = this;
	}
	
	// Update is called once per frame
	void Update () {
		if (ChooseEnemy.Instance.target != null)
			tar = ChooseEnemy.Instance.target.Value;
	}
//
//	void GetShadows(){
//		GameObject[] shadows = GameObject.FindGameObjectsWithTag ("Shadow");
// 
//	}
	public void fireball(){
		foreach (GameObject sha in shadows) {
			sha.transform.LookAt (tar.transform);
			RangeAttack range = sha.GetComponent<RangeAttack> ();
			range.useSkill ();
		}
	}
	public void charge(){
		foreach (GameObject sha in shadows) {
			ChargeAttack charge = sha.GetComponent<ChargeAttack> ();


			if (charge.Cooldown == 0) {
				sha.transform.LookAt (tar.transform);
				charge.setTarget (tar);
				charge.useSkill ();
				charge.attacked = false;
			}
		}

	}
	public void backstab(){
		foreach (GameObject sha in shadows) {
			if(sha.GetComponent<stealthAttack>().Opponent!=null&& Vector3.Distance(sha.transform.position,sha.GetComponent<stealthAttack>().Opponent.transform.position)<2f)
			sha.GetComponent<stealthAttack> ().launchattack = true;
			sha.GetComponent<shadowStatus> ().bonusTime ();
		}
	}


}
