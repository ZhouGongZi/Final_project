using UnityEngine;
using System.Collections;

public class controllerSkill : MonoBehaviour {
	public Skill spawnBattle;
	public Skill rangeattack;
	public Skill PlayerCharge;
	public Skill PlainAttack;
	public Skill TimeStop;
<<<<<<< HEAD
	public Skill backstab;
	public Skill switchPlayer;
=======
	public Skill AOE;
>>>>>>> 42f29b47fb3a2de9ba9d067687f9a72adbdc7532
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
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
		if (Input.GetKeyDown (KeyCode.T) && TimeStop.Cooldown == 0&&PlayerStatus.Instance.Fury==100) {
			TimeStop.useSkill ();
			this.GetComponent<RangeAttack> ().cancel();
			PlayerStatus.Instance.AddFury (50);
		}
<<<<<<< HEAD
		if (Input.GetKeyDown (KeyCode.V) && backstab.Cooldown == 0) {
			this.GetComponent<RangeAttack> ().cancel();
			backstab.useSkill ();
		}
		if (Input.GetKeyDown (KeyCode.G) && switchPlayer.Cooldown == 0) {
			switchPlayer.useSkill ();
			this.GetComponent<RangeAttack> ().cancel();
		}

=======
		if (Input.GetKeyDown (KeyCode.O) && AOE.Cooldown == 0) {
			AOE.useSkill ();
		}
>>>>>>> 42f29b47fb3a2de9ba9d067687f9a72adbdc7532
	}
}
