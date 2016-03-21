using UnityEngine;
using System.Collections;

public class controllerSkill : MonoBehaviour {
	public Skill spawnBattle;
	public Skill rangeattack;
	public Skill PlayerCharge;
	public Skill PlainAttack;
	public Skill TimeStop;
	public Skill backstab;
	public Skill switchPlayer;
	public Skill AOE;
	public Skill shadowFight;

	public bool isStop = false;
	Skill cast;


	void Update () {
		if (isStop)
			decreaseCooldown ();
		else
			increaseCooldown ();
//		if (Input.GetKeyDown (KeyCode.E)) {
//			cast = Skill [0];
//		}
//		if (Input.GetKeyDown (KeyCode.Q)) {
//			cast = Skill [1];
//		}
//		if (Input.GetKeyDown (KeyCode.R)) {
//			cast = Skill [2];
//		}
//		if (Input.GetKeyDown (KeyCode.F)) {
//			cast = Skill [3];
//		}
//		if (Input.GetKeyDown (KeyCode.T)) {
//			cast = Skill [4];
//		}
//		if (Input.GetKeyDown (KeyCode.V)) {
//			cast = Skill [5];
//		}
//		if (Input.GetKeyDown (KeyCode.G)) {
//			cast = Skill [6];
//		}
//		if (Input.GetKeyDown (KeyCode.O)) {
//			cast = Skill [7];
//		}

		if(Input.GetKeyDown(KeyCode.E)&&spawnBattle.Cooldown==0){
			spawnBattle.useSkill ();
			this.GetComponent<RangeAttack> ().cancel();
		}
		if (Input.GetKeyDown (KeyCode.Q)&&rangeattack.Cooldown==0) {
			rangeattack.useSkill ();
			ShadowController.Instance.fireball ();

		}
		if (Input.GetKeyDown (KeyCode.R)&&PlayerCharge.Cooldown==0) {
			PlayerCharge.useSkill ();
			this.GetComponent<RangeAttack> ().cancel();
		}
		if (Input.GetKeyDown (KeyCode.F) && PlainAttack.Cooldown == 0) {
			PlainAttack.useSkill ();
			this.GetComponent<RangeAttack> ().cancel();
		}
		if (Input.GetKeyDown (KeyCode.T) && TimeStop.Cooldown == 0&&PlayerStatus.Instance.Fury==0) {
			TimeStop.useSkill ();
			this.GetComponent<RangeAttack> ().cancel();
			PlayerStatus.Instance.AddFury (50);
			isStop = true;
		}

		if (Input.GetKeyDown (KeyCode.V) && backstab.Cooldown == 0) {
			this.GetComponent<RangeAttack> ().cancel();
			backstab.useSkill ();
		}
		if (Input.GetKeyDown (KeyCode.G) && switchPlayer.Cooldown == 0) {
			switchPlayer.useSkill ();
			this.GetComponent<RangeAttack> ().cancel();
		}

		if (Input.GetKeyDown (KeyCode.O) && AOE.Cooldown == 0&&PlayerStatus.Instance.Fury==100) {
			AOE.useSkill ();
			PlayerStatus.Instance.AddFury (-50);
		}

		if (Input.GetKeyDown (KeyCode.C) && shadowFight.Cooldown == 0) {
			shadowFight.useSkill ();
		}

	}


	void decreaseCooldown(){
		spawnBattle.maxCoolDown = 1.0f;
		switchPlayer.maxCoolDown = 1.0f;

	}
	void increaseCooldown(){
		spawnBattle.maxCoolDown = 3.0f;
		switchPlayer.maxCoolDown = 3.0f;
	}
}
