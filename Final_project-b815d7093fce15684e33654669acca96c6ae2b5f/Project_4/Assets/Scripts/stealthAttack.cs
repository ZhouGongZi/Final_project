using UnityEngine;
using System.Collections;

public class stealthAttack : MonoBehaviour {
	public AnimationClip BackStab;

	float impactTime=0.33f;
	Vector3 startPosition;
	GameObject opponent;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		setStart ();
		if (checkAttack ()) {
			if (Input.GetKeyDown (KeyCode.E))
				attack ();
			if(this.GetComponent<Animation>()[BackStab.name].time>=this.GetComponent<Animation>()[BackStab.name].length*impactTime)
				opponent.GetComponent<Enemy> ().GetHit (100);
		}


	}

	bool checkAttack(){
		RaycastHit hit;
		Debug.DrawRay(startPosition, transform.forward, Color.green);
		if (Physics.Raycast (startPosition, transform.forward, out hit, 1.0f)) {
			if (hit.collider.gameObject.tag == "Enemy") {
				opponent = hit.collider.gameObject;
				if (Vector3.Dot (this.transform.forward, opponent.transform.forward) > 0)
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
