using UnityEngine;
using System.Collections;

public class controllerSkill : MonoBehaviour {
	public Skill backstab;
	public Skill rangeattack;
	public Skill PlayerCharge;
	public Skill PlainAttack;
	public Skill TimeStop;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.E)&&backstab.Cooldown==0){
			backstab.useSkill ();
		}
		if (Input.GetKeyDown (KeyCode.Q)&&rangeattack.Cooldown==0) {
			rangeattack.useSkill ();
			ShadowController.Instance.fireball ();
		}
		if (Input.GetKeyDown (KeyCode.R)&&PlayerCharge.Cooldown==0) {
			PlayerCharge.useSkill ();
		}
		if (Input.GetKeyDown (KeyCode.F) && PlainAttack.Cooldown == 0) {
			PlainAttack.useSkill ();
		}
		if (Input.GetKeyDown (KeyCode.T) && TimeStop.Cooldown == 0&&PlayerStatus.Instance.Fury==100) {
			TimeStop.useSkill ();
			PlayerStatus.Instance.AddFury (50);
		}
	}
}
