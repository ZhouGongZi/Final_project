using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Hud : MonoBehaviour {

	public Slider health_bar;
	public Slider fury_bar;

	public Camera gui_cam;
	public Camera main_cam;
	public GameObject lock_on_icon;
	Vector3 offset = new Vector3 (0f, 0.7f, 0f);

	Vector3 lock_on = Vector3.zero;

	Vector3 pos= Vector3.zero;

	Vector3 temp= Vector3.zero;


	// Use this for initialization
	void Start () {
		health_bar.maxValue = 100;
		fury_bar.maxValue= 100;
		main_cam = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		if (ChooseEnemy.Instance.target == null || ChooseEnemy.Instance.target.Value.GetComponent<Enemy> ().Health <= 0)
			lock_on_icon.SetActive (false);
		else
			lock_on_icon.SetActive (true);
		health_bar.value = PlayerStatus.Instance.Health;
		print (PlayerStatus.Instance.Health);
		fury_bar.value = PlayerStatus.Instance.Fury;
		if (ChooseEnemy.Instance.target!= null) {
			pos = ChooseEnemy.Instance.target.Value.transform.GetChild(2).transform.position;
		}
		//Debug.Log (pos);

		temp = main_cam.WorldToViewportPoint (pos);
		temp = gui_cam.ViewportToWorldPoint (temp);

		//temp.x -= 470;
		//temp.y -= 450;
		//temp.y = 0;
		lock_on = temp;
		lock_on_icon.transform.position = lock_on;
	}
}
