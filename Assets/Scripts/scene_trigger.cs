using UnityEngine;
using System.Collections;

public class scene_trigger : MonoBehaviour {
    public bool triggered = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter (Collision coll)
    {
        if (coll.transform.tag == "Player" || coll.transform.tag =="Shadow")
        {
            triggered = true;
        }
    }
}
