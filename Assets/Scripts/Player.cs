using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Player : MonoBehaviour
{

    public float forwardMovementSpeed = 90.0f;
    public float backwardMovementSpeed = 90.0f;
    public float leftMovementSpeed = 90.0f;
    public float rightMovementSpeed = 90.0f;

    public float shortest_shadow_distance = 1.0f;
    public float longest_shadow_distance = 3.0f;

    float shadow_initial_cd_max = 200;
    float shadow_initial_cd = 0;

    public bool game_started;
    public bool shadow_on = false;
    public List<GameObject> player_shadow_list;
    public GameObject shadow_indicator;
    public GameObject acient;

    private Vector3 moveDirection = Vector3.zero;

    private float nextMelee = 0.0f;

    bool jump;

    void Awake()
    {
        //anim = GetComponent<Animator>();

    }


    // Use this for initialization
    void Start()
    {
        game_started = true;


    }

    // Update is called once per frame
    void Update()
    {

        CharacterController controller = GetComponent<CharacterController>();
        CharacterController acient_controller = acient.GetComponent<CharacterController>();

        if (game_started)
        {
            //horizontal_input = Input.GetAxis("LeftJoystickX_P" + joystickString);
            //vertical_input = Input.GetAxis("LeftJoystickY_P" + joystickString);
            float horizontal_input = Input.GetAxis("Horizontal");
            float vertical_input = Input.GetAxis("Vertical");

            //if (vertical_input > 0)
            //{
            //    moveDirection.z = vertical_input;
            //    //print(moveDirection.ToString());
            //}
            //else
            //{
            //    moveDirection.z = vertical_input;
            //    //print(moveDirection.ToString());
            //}

            //if (horizontal_input > 0)
            //{
            //    moveDirection.x = horizontal_input;
            //    //print(moveDirection.ToString());
            //}
            //else
            //{
            //    moveDirection.x = horizontal_input;
            //    //print(moveDirection.ToString());
            //}

            moveDirection = horizontal_input * transform.right + vertical_input * transform.forward;





            if (Input.GetKey(KeyCode.X))
            {
                if (shadow_initial_cd < shadow_initial_cd_max / 2.0f)
                {
                    float z_scale = shortest_shadow_distance + 2.0f * shadow_initial_cd / shadow_initial_cd_max * (longest_shadow_distance - shortest_shadow_distance);
                    float z_pos = z_scale / 2.0f;
                    shadow_indicator.GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, z_scale);
                    shadow_indicator.GetComponent<Transform>().localPosition = new Vector3(0f, 0f, z_pos);
                    shadow_initial_cd++;
                }
                else
                {
                    float z_scale = longest_shadow_distance - ((2.0f * shadow_initial_cd - shadow_initial_cd_max) / shadow_initial_cd_max) * (longest_shadow_distance - shortest_shadow_distance);
                    print(shadow_initial_cd);
                    print(z_scale);
                    float z_pos = z_scale / 2.0f;
                    shadow_indicator.GetComponent<Transform>().localScale = new Vector3(0.1f, 0.1f, z_scale);
                    shadow_indicator.GetComponent<Transform>().localPosition = new Vector3(0f, 0f, z_pos);
                    shadow_initial_cd++;
                    if (shadow_initial_cd == shadow_initial_cd_max)
                    {
                        shadow_initial_cd = 0;
                    }
                }
            }

            else if (Input.GetKeyDown(KeyCode.Z))
            {
                if (!shadow_indicator.activeInHierarchy)
                {
                    shadow_indicator.SetActive(true);
                }
                else
                {
                    shadow_on = true;
                    acient.SetActive(true);
                    GetComponent<Animation>().Play("attack 3");
                    Vector3 thePosition = shadow_indicator.transform.TransformPoint(Vector3.forward * 0.6f);
                    acient.transform.position = thePosition;
                    Quaternion rot = new Quaternion();
                    rot.SetLookRotation(transform.forward);
                    acient.transform.rotation = rot;
                    shadow_indicator.SetActive(false);
                }
            }


            else if (Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<Animation>().Play("jump");
            }

            else if (Input.GetKeyDown(KeyCode.K))
            {
                Melee_Attack();
                //nextMelee = Time.time + meleeRate;
            }




            else if (vertical_input < -0.5 || vertical_input > 0.5 || horizontal_input > 0.5 || horizontal_input < -0.5)
            {
                //Quaternion rot = new Quaternion();
                //rot.SetLookRotation(moveDirection);
                //transform.rotation = rot;
                //Vector3 Rot = moveDirection;
                //Rot.x = 0;
                //Rot.z = 0;
                //transform.Rotate(Rot);
                //transform.RotateAround(transform.position, transform.up, Time.deltaTime * 90f);

                transform.Rotate(0, horizontal_input * 150 * Time.deltaTime, 0);


                if (GetComponent<Animation>().IsPlaying("ready"))
                {
                    GetComponent<Animation>().Stop();
                }
                GetComponent<Animation>().PlayQueued("walk");
                if (shadow_on)
                    if (acient.GetComponent<Animation>().IsPlaying("ready"))
                    {
                        acient.GetComponent<Animation>().Stop();
                    }
                acient.GetComponent<Animation>().PlayQueued("walk");
                //acient.transform.rotation = rot;
            }
            else
            {
                if (GetComponent<Animation>().IsPlaying("walk"))
                {
                    GetComponent<Animation>().Stop();
                }
                GetComponent<Animation>().PlayQueued("ready");
                if (shadow_on)
                    if (acient.GetComponent<Animation>().IsPlaying("walk"))
                    {
                        acient.GetComponent<Animation>().Stop();
                    }
                acient.GetComponent<Animation>().PlayQueued("ready");
            }




            controller.Move(moveDirection * Time.deltaTime * 10);
            if (shadow_on)
                acient_controller.Move(moveDirection * Time.deltaTime * 10);
        }
    }
    void Melee_Attack()
    {
        GetComponent<Animation>().Play("attack 5");
        
    }
}