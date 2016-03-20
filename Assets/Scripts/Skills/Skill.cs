using UnityEngine;
using System.Collections;

public class Skill : MonoBehaviour {
	//properties
	public enum skillType{
		AOE,
		timeStop,
		backStash,
		shadowBackAttack
	}
	//enemy type

	protected skillType _skillType;
	private float _cooldown=0;
	public float Cooldown {
		get { return _cooldown; }
		set {
			if (value <= 0)
				_cooldown = 0;
			else
				_cooldown = value;
		}
	}
	public float maxCoolDown;
	public skillType SkillType{
		get { return _skillType;}
		set { _skillType = value;}
	}
		

	protected Animation Anim;
	private float startTime;


	public virtual void Start(){
	}


	public virtual void Update(){
		Cooldown = maxCoolDown - (Time.time - startTime);
			
	}
		

	public virtual void useSkill(){
		
		startTime = Time.time;
	}
	//chase the player

}
