using UnityEngine;
using System.Collections;

public class Create_shadow : MonoBehaviour {

	public GameObject 				shadow_indicator;
	public GameObject 				ancient;

	public float 					shortest_shadow_distance = 1.0f;
	public float 					longest_shadow_distance = 3.0f;

	float 							shadow_initial_cd_max = 200;
	float 							shadow_initial_cd = 0;

	private float 					createRate = 3f;
	private float 					nextCreate = 0.0f;

	bool 							hasIndicator = false;
	bool 							hasPressed = false;
	// Use this for initialization
	void Start () {
	
	}
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis("Y") > 0.9)
		{
			hasPressed = false;
			if (shadow_initial_cd < shadow_initial_cd_max / 2.0f)
			{
				float z_scale = shortest_shadow_distance + 2.0f * shadow_initial_cd / shadow_initial_cd_max * (longest_shadow_distance - shortest_shadow_distance);
				float z_pos = z_scale / 2.0f;
				shadow_indicator.GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, z_scale);
				shadow_indicator.GetComponent<Transform>().localPosition = new Vector3(0f, 1.5f, z_pos);
				shadow_initial_cd++;
			}
			else
			{
				float z_scale = longest_shadow_distance - ((2.0f * shadow_initial_cd - shadow_initial_cd_max) / shadow_initial_cd_max) * (longest_shadow_distance - shortest_shadow_distance);
				float z_pos = z_scale / 2.0f;
				shadow_indicator.GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, z_scale);
				shadow_indicator.GetComponent<Transform>().localPosition = new Vector3(0f, 1.5f, z_pos);
				shadow_initial_cd++;
				if (shadow_initial_cd == shadow_initial_cd_max)
				{
					shadow_initial_cd = 0;
				}
			}
		}
		else if (Input.GetAxis("B") > 0.9 && hasPressed == false)
		{
			hasPressed = true;
			if (!shadow_indicator.activeInHierarchy)
			{
				//make the shadow indicator appear 
				shadow_indicator.SetActive(true);
				hasIndicator = true;
			}
			else if(Time.time > nextCreate && hasIndicator)
			{
				//the cool down effect
				nextCreate = Time.time + createRate;

				//instantiate a new shadow
				GameObject go = Instantiate (ancient) as GameObject;
				ShadowController.Instance.shadows.Add (go);
				//GetComponent<Animation>().Play("attack 3");
				hasIndicator = false;

				//position of the new shadow
				Vector3 thePosition = shadow_indicator.transform.TransformPoint(Vector3.forward * 0.6f);
				thePosition.y = 0;
				go.transform.position = thePosition;
				hasPressed = false;	

				//set the rotation of the new shadow
				Quaternion rot = new Quaternion();
				rot.SetLookRotation(transform.forward);
				go.transform.rotation = rot;

				//indicator disappear
				shadow_indicator.SetActive(false);
			}
		}


	}
}
