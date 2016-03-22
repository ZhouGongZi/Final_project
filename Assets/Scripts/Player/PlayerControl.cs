using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    //control inputs&parameters
    private float left_x, left_y, right_x, right_y;
    private float A, B, X, Y;
    private float RB, LB, RT, LT;
    private float sprint;
    private bool if_jump = false;
    private bool walk_forward = true;
    private Animation anim;
    //constants
    private const float forward_speed = 90f;
    private const float backward_speed = 50f;
    private const float stafe_speed = 50f;
    //state machine variables
    public AnimStateMachine anim_control;
    //Camera control
    public Camera cam;
	// Use this for initialization

    private void ChangeWalkDirection(bool walk_forward)
    {
        if(walk_forward)
        {
            anim["walk"].speed = 1;
            anim["walk"].time = 0;
        }
        else
        {
            anim["walk"].speed = -1;
            anim["walk"].time = anim["walk"].length;
        }
    }

	void Start () {
        anim = GetComponent<Animation>();
        anim_control = new AnimStateMachine(anim, new AnimState("ready 2", PlayerState.none, PlayerState.idle));
        cam = GameObject.Find("MainCamera").GetComponent<Camera>();
        
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        // Get the input parameters
        left_x = Input.GetAxis("LeftX");
        left_y = Input.GetAxis("LeftY");
        print(left_y);
        right_x = Input.GetAxis("RightX");
        right_y = Input.GetAxis("RightY");
        A = Input.GetAxis("A");
        B = Input.GetAxis("B");
        X = Input.GetAxis("X");
        Y = Input.GetAxis("Y");
        RB = Input.GetAxis("RB");
        LB = Input.GetAxis("LB");
        RT = Input.GetAxis("RT");
        LT = Input.GetAxis("LT");
        sprint = Input.GetAxis("Sprint");
        if (!if_jump)
        {
            if (X>0.1)
            {
                if_jump = true;
            }
        }
        //transitions of the states
        if (left_y >0.7)
        {
            if (!walk_forward)
            {
                walk_forward = true;
                ChangeWalkDirection(walk_forward);
            }
            if (sprint > 0.1)
            {
                anim_control.ChangeState(new AnimState("run", anim_control._current_state.player_state, PlayerState.moving));
            }
            else
            {
                anim_control.ChangeState(new AnimState("walk", anim_control._current_state.player_state, PlayerState.moving));
            }
            
        }
        else if (left_y < -0.7)
        {
            if (walk_forward)
            {
                walk_forward = false;
                ChangeWalkDirection(walk_forward);
            }
            anim_control.ChangeState(new AnimState("walk", anim_control._current_state.player_state, PlayerState.moving));
        }
        else if (left_x > 0.1)
        {
            anim_control.ChangeState(new AnimState("strafe right", anim_control._current_state.player_state, PlayerState.moving));
        }
        else if (left_x < -0.1)
        {
            anim_control.ChangeState(new AnimState("strafe left", anim_control._current_state.player_state, PlayerState.moving));
        }
        else
        {
            anim_control.SetToDefaultState();
            anim.Play("ready 2");
        }




        anim_control.Update();
        
        if (Input.GetKeyDown(KeyCode.Q))
            anim.Play("attack 1");
        if (Input.GetKeyDown(KeyCode.W))
            anim.Play("ready 2");
    }

}
