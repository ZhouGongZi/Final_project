using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    // Use this for initialization
    public GameObject player_pos;
    private float cam_speed = 0.5f;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        transform.LookAt(player_pos.transform);
	}
}
