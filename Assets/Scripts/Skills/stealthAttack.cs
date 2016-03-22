using UnityEngine;
using System.Collections;

public class stealthAttack : MonoBehaviour {
	public AnimationClip BackStab;
	public bool launchattack=false;
	public int damage;
	float impactTime=0.40f;
	Vector3 startPosition;
	GameObject _opponent;
	public GameObject Opponent{
		set { _opponent = value;}
		get { return _opponent;}
	}


	// Update is called once per frame
	void Update () {
		if(Opponent!=null)
		this.transform.LookAt (Opponent.transform);
		setStart ();
		if (Opponent!=null&& launchattack&&Opponent.tag=="Enemy")
			attack ();
		if (launchattack&& this.GetComponent<Animation> () [BackStab.name].time >= this.GetComponent<Animation> () [BackStab.name].length * impactTime) {
			if (Opponent!=null&&Opponent.tag=="Enemy") {
				Opponent.GetComponent<Enemy> ().GetHit (damage);
				launchattack = false;
			}
		}



	}

	bool checkAttack(){
		RaycastHit hit;
		Debug.DrawRay(startPosition, transform.forward, Color.green);
		if (Physics.Raycast (startPosition, transform.forward, out hit, 1.0f)) {
			if (hit.collider.gameObject.tag == "Enemy") {
				_opponent = hit.collider.gameObject;
				if (Vector3.Dot (this.transform.forward, _opponent.transform.forward) > 0)
					//do something to show that you can bakcstab
					return true;
				else
					return false;

			} else
				return false; 

		} else
			return false;
	}

	void setStart(){
		startPosition.x = transform.position.x;
		startPosition.y = transform.position.y + 0.5f;
		startPosition.z = transform.position.z;
	}

	void attack(){
		this.GetComponent<Animation> ().Play (BackStab.name);


	}
}
