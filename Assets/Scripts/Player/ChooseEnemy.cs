using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChooseEnemy :MonoBehaviour{
	public static ChooseEnemy Instance;
	public LinkedList<GameObject> enemy=new LinkedList<GameObject>();
	public LinkedListNode<GameObject> target=null;

	float fireRate = 0.5f;
	float nextfire = 0.0f;

	void Awake(){
		Instance = this;

	}
	void Start(){
		GetEnemies ();
	}
	// Use this for initializatio
	void Update()
	{

		GetEnemies ();
		if (Input.GetAxis ("Tab") > 0.5 && Time.time > nextfire) {
			chooseEnemy ();
			nextfire = Time.time + fireRate;
		}
		
	}

	void GetEnemies(){
		GameObject[] ene= GameObject.FindGameObjectsWithTag ("Enemy");

		foreach (GameObject en in ene) {
			if (Vector3.Distance (en.transform.position, this.transform.position) < 100 && Vector3.Dot (en.transform.position - this.transform.position, this.transform.forward) >- 0.3){
				
				enemy.AddLast (en);

		}


		}
	}
	void chooseEnemy(){
		if (enemy.First == null)
			GetEnemies ();
		
		if (target==null|| target.Next == null)
			target = enemy.First;
		else
			target = target.Next;
		//target.Value.GetComponent<Enemy> ().chosed();
		//Debug.Log (target.Value.name);

	}


}
