using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

    public float forwardMovementSpeed = 90.0f;
    public float backwardMovementSpeed = 90.0f;
    public float leftMovementSpeed = 90.0f;
    public float rightMovementSpeed = 90.0f;

    public bool game_started;

    private Vector3 moveDirection = Vector3.zero;

    void Awake()
    {
        //anim = GetComponent<Animator>();

    }


    // Use this for initialization
    void Start () {
        game_started = true;

    }
	
	// Update is called once per frame
	void Update () {

        CharacterController controller = GetComponent<CharacterController>();

        if (game_started)
        {
            //horizontal_input = Input.GetAxis("LeftJoystickX_P" + joystickString);
            //vertical_input = Input.GetAxis("LeftJoystickY_P" + joystickString);
            float horizontal_input = Input.GetAxis("Horizontal");
            float vertical_input = Input.GetAxis("Vertical");

            if (vertical_input > 0)
            {
                moveDirection.z = vertical_input;
                print(moveDirection.ToString());
            }
            else
            {
                moveDirection.z = vertical_input;
                print(moveDirection.ToString());
            }

            if (horizontal_input > 0)
            {
                moveDirection.x = horizontal_input;
                print(moveDirection.ToString());
            }
            else
            {
                moveDirection.x = horizontal_input;
                print(moveDirection.ToString());
            }


            if (!(vertical_input==0&&horizontal_input==0)) {
                Quaternion rot = new Quaternion();
                rot.SetLookRotation(moveDirection);
                transform.rotation = rot;
                GetComponent<Animation>().Play("walk");
            }
            else {
                GetComponent<Animation>().Play("ready 2");
            }
            
            controller.Move(moveDirection * Time.deltaTime*10);
        }
	}
}