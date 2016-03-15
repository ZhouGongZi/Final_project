using UnityEngine;
using System.Collections;

public class Multi_trigger : MonoBehaviour {
    public GameObject[] triggers;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        bool ifAllTriggered = true;
        foreach (GameObject i in triggers)
        {
            if (!i.GetComponent<scene_trigger>().triggered)
            {
                ifAllTriggered = false;
            }
        }
        if (ifAllTriggered)
        {
            Destroy(transform.gameObject);
        }
	}
}
