using UnityEngine;
using System.Collections;

public class stealthEffect : MonoBehaviour {
	public Material mat,hair;
	// Use this for initialization
	void Start () {
		mat = GetComponent<SkinnedMeshRenderer>().materials[0];
		hair = GetComponent<SkinnedMeshRenderer> ().materials [1];
	}
	
	// Update is called once per frame
	void Update () {
		if (PlayerStatus.Instance.Fury == 0) {
			mat.SetFloat ("_Mode", 2f);
			hair.SetFloat ("_Mode", 2f);
		} else {
			mat.SetFloat ("_Mode", 0f);
			hair.SetFloat("_Mode",0f);
		}
	}
}
