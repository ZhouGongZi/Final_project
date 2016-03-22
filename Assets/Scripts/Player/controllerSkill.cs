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


	private float left_x, left_y, right_x, right_y;
	private float A, B, X, Y;
	private float RB, LB, RT, LT;
	private float sprint;





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

		/*
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
		}*/
		left_x = Input.GetAxis("LeftX");
		left_y = Input.GetAxis("LeftY");
		print(left_y);
		right_x = Input.GetAxis("RightX");
		right_y = Input.GetAxis("RightY");
		A = Input.GetAxis("A");
		B = Input.GetAxis("B");
		X = Input.GetAxis("X");
		Y = Input.GetAxis("Y");
		RB = Input.GetAxis("RB");
		LB = Input.GetAxis("LB");
		RT = Input.GetAxis("RT");
		LT = Input.GetAxis("LT");

		sprint = Input.GetAxis("Sprint");

		if(LT==1 && B==1 &&spawnBattle.Cooldown==0){
			spawnBattle.useSkill ();
			this.GetComponent<RangeAttack> ().cancel();
		}
		if (LT==1 && Y==1 &&rangeattack.Cooldown==0) {
			rangeattack.useSkill ();
			ShadowController.Instance.fireball ();

		}
		if (LT==1 && X==1 && (left_y > 0) &&PlayerCharge.Cooldown==0) {
			PlayerCharge.useSkill ();
			this.GetComponent<RangeAttack> ().cancel();
		}
		if (X==1 && PlainAttack.Cooldown == 0) {
			PlainAttack.useSkill ();
			this.GetComponent<RangeAttack> ().cancel();
		}
		if (RT==1 && TimeStop.Cooldown == 0&&PlayerStatus.Instance.Fury==0) {
			TimeStop.useSkill ();
			this.GetComponent<RangeAttack> ().cancel();
			PlayerStatus.Instance.AddFury (50);
			isStop = true;
		}

		if (LT==1 && X==1 && (left_y < 0) && backstab.Cooldown == 0) {
			this.GetComponent<RangeAttack> ().cancel();
			backstab.useSkill ();
		}
		if (Y==1 && switchPlayer.Cooldown == 0 && !GetComponent<Create_shadow>().shadow_indicator.activeInHierarchy) {
			//is this good ??????????????????????????
			//zhen bu fang xin na !!!!!!!!!!!!!!!!!!!!!!!!!!!
			switchPlayer.useSkill ();
			this.GetComponent<RangeAttack> ().cancel();
		}

		if (RT==1 && AOE.Cooldown == 0 && PlayerStatus.Instance.Fury==100) {
			AOE.useSkill ();
			PlayerStatus.Instance.AddFury (-50);
		}

		if (X==1 && shadowFight.Cooldown == 0) {
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
