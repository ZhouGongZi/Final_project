using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChooseEnemy :MonoBehaviour{
	public static ChooseEnemy Instance;
	public LinkedList<GameObject> enemy=new LinkedList<GameObject>();
	public LinkedListNode<GameObject> target=null;

	void Awake(){
		Instance = this;

	}
	void Start(){
		GetEnemies ();
	}
	// Use this for initialization
	void Update()
	{

		if (Input.GetKeyDown (KeyCode.Tab))
			chooseEnemy();
		
	}

	void GetEnemies(){
		GameObject[] ene= GameObject.FindGameObjectsWithTag ("Enemy");
		foreach (GameObject en in ene) {
			if (Vector3.Distance (en.transform.position, this.transform.position) < 50 && Vector3.Dot (en.transform.position - this.transform.position, this.transform.forward) > 0){
				
				enemy.AddLast (en);

		}


		}
	}
	void chooseEnemy(){
		if (enemy.First == null)
			GetEnemies ();
		
		if (target != null)
			target.Value.GetComponent<Enemy> ().unchosed ();
		else
			GetEnemies ();
	
		
		if (target==null|| target.Next == null)
			target = enemy.First;
		else
			target = target.Next;
		target.Value.GetComponent<Enemy> ().chosed();
		Debug.Log (target.Value.name);

	}


}
