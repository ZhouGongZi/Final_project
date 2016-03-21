using UnityEngine;
using System.Collections;

public class PlayerAnimationControl : MonoBehaviour {
    //animator parameters
    public Animator anim;
    private bool ranged_attack_input = false;
    private bool melee_attack_input = false;


	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        ranged_attack_input = Input.GetKeyDown(KeyCode.Q);
        melee_attack_input = Input.GetKeyDown(KeyCode.F);
        anim.SetBool("ranged_attack", ranged_attack_input);
        anim.SetBool("melee_attack", melee_attack_input);
        if (Input.GetKeyDown(KeyCode.F))
            anim.Play("Melee Attack");
	}
}
