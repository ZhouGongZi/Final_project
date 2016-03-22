using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerControl : MonoBehaviour {
    //control inputs&parameters
    CharacterController controller;
    public static PlayerControl instance;
    private float left_x, left_y, right_x, right_y;
    private float A, B, X, Y;
    private float RB, LB, RT, LT;
    private float sprint;
    private bool if_jump = false;
    private bool walk_forward = true;
    private Animation anim;
    //jump parameters
    private const float jumping_force = 100f;
    private const float gravity = 5f;
    //movements
    private Vector3 movement;
    private const float sprint_speed = 2.5f;
    private const float forward_speed = 1f;
    private const float backward_speed = 0.5f;
    private const float strafe_speed = 1f;
    //state machine variables
    public AnimStateMachine anim_control;
    //Camera control
    public Camera cam;
    public GameObject cam_container;
    private const float cam_speed = 150f;
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
    private void CheckJump()
    {
        {
            RaycastHit hitInfo;
#if UNITY_EDITOR
            // helper to visualise the ground check ray in the scene view
            Debug.DrawLine(transform.position + (Vector3.up * 0.1f), transform.position + (Vector3.up * 0.1f) + (Vector3.down * 0.1f));
#endif
            // 0.1f is a small offset to start the ray from inside the character
            // it is also good to note that the transform position in the sample assets is at the base of the character
            if (Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, 0.1f))
            {
                if_jump = false;
            }
            else
            {
                if_jump = true;
            }
        }
    }

	void Start () {
        anim = GetComponent<Animation>();
        anim_control = new AnimStateMachine(anim, new AnimState("ready 2", PlayerState.none, PlayerState.idle));
        cam = GameObject.Find("MainCamera").GetComponent<Camera>();
        instance = this;
        controller = GetComponent<CharacterController>();
        cam_container = GameObject.Find("CamContainer");
        anim["jump"].speed = 0.8f;
    }
	
	// Update is called once per frame
    void Update()
    {
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
    }

	void FixedUpdate () {

        //transitions of the states
        if (left_y > 0.7)
        {
            if (!walk_forward)
            {
                walk_forward = true;
                ChangeWalkDirection(walk_forward);
            }
            if (sprint > 0.1)
            {
                anim_control.ChangeState(new AnimState("run", anim_control._current_state.player_state, PlayerState.moving));
                movement = transform.forward * sprint_speed * Time.deltaTime;
            }
            else
            {
                anim_control.ChangeState(new AnimState("walk", anim_control._current_state.player_state, PlayerState.moving));
                movement = transform.forward * forward_speed * Time.deltaTime;
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
            movement = -transform.forward * backward_speed * Time.deltaTime;
        }
        else if (left_x > 0.1)
        {
            anim_control.ChangeState(new AnimState("strafe right", anim_control._current_state.player_state, PlayerState.moving));
            movement = transform.right * strafe_speed * Time.deltaTime;
        }
        else if (left_x < -0.1)
        {
            anim_control.ChangeState(new AnimState("strafe left", anim_control._current_state.player_state, PlayerState.moving));
            movement = -transform.right * strafe_speed * Time.deltaTime;
        }
        else if (left_x == 0 && left_y == 0 && (anim_control._current_state.player_state == PlayerState.moving || anim_control._current_state.player_state == PlayerState.idle))
        {
            anim_control.SetToDefaultState();
        }

        if (right_x != 0)
        {
            transform.Rotate(0, right_x * cam_speed * Time.deltaTime, 0);
        }
        if (X > 0.1)
        {
            anim_control.ChangeState(new AnimState("attack 5", anim_control.playing_state, PlayerState.melee_attack));
        }
        if (A>0.1)
        {
            anim_control.ChangeState(new AnimState("jump", anim_control.playing_state, PlayerState.jumping));
        }
        anim_control.Update();
        if (anim_control._current_state.player_state == PlayerState.moving)
        {
            controller.Move(movement);
        }
        
        
    } 



}
