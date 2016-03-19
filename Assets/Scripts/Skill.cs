using UnityEngine;
using System.Collections;

public class Skill : MonoBehaviour {
	//properties
	public enum skillType{
		AOE,
		timeStop,
		backStash
	}
	//enemy type
	protected skillType _skillType;

	public skillType SkillType{
		get { return _skillType;}
		set { _skillType = value;}
	}
		

	protected Animation Anim;


	public virtual void Start(){
	}


	public virtual void Update(){
	}
		

	public virtual void useSkill(){
	}
	//chase the player

}
