using UnityEngine;
using System.Collections;

public class shadowStatus : MonoBehaviour {
	float startTime;
	public float lifeTime=8f;
	// Use this for initialization
	void Start () {
		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.time - startTime > lifeTime) {
			Destroy (this.gameObject);
			ShadowController.Instance.shadows.Remove (this.gameObject);
		}
	}
	public void bonusTime(){
		lifeTime += 3f;
	}


}
