using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    //control inputs&parameters
    private float left_x, left_y, right_x, right_y;
    private float A, B, X, Y;
    private float RB, LB, RT, LT;
    private bool if_jump = false;
    private Animation anim;
    //constants
    private const float forward_speed = 90f;
    private const float backward_speed = 50f;
    private const float stafe_speed = 50f;
    //state machine variables
    public AnimStateMachine anim_control;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animation>();
        anim_control = new AnimStateMachine(anim, new AnimState("ready 2", true, PlayerState.idle));
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // Get the input parameters
        left_x = Input.GetAxis("LeftX");
        left_y = Input.GetAxis("LeftY");
        right_x = Input.GetAxis("RightX");
        right_y = Input.GetAxis("RightY");
        A = Input.GetAxis("ButtonA");
        B = Input.GetAxis("ButtonB");
        X = Input.GetAxis("ButtonX");
        Y = Input.GetAxis("ButtonY");
        RB = Input.GetAxis("RB");
        LB = Input.GetAxis("LB");
        RT = Input.GetAxis("RT");
        LT = Input.GetAxis("LT");
        if (!if_jump)
        {
            if (X>0.1)
            {
                if_jump = true;
            }
        }
        anim_control.Update();
    }

}
