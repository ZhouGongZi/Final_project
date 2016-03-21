using UnityEngine;
using System.Collections;

public class coolDownCounter : MonoBehaviour {
	public  Skill spell;
	UISprite mask;
	public bool timeStop;
	public bool AOE;
	// Use this for initialization
	void Start () {
		mask = this.GetComponent<UISprite> ();
		GameObject player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (timeStop) {
			if (PlayerStatus.Instance.Fury != 0) {
				mask.fillAmount = 1;
				return;
			}
		} else if (AOE) {
			if (PlayerStatus.Instance.Fury != 100) {
				mask.fillAmount = 1;
				return;
			}
		}
		mask.fillAmount = spell.Cooldown / spell.maxCoolDown;
	}
}
