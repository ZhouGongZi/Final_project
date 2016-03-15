using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Player : MonoBehaviour
{
    public static Player instance;

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
    public GameObject ancient;

    private Vector3 moveDirection = Vector3.zero;

    private bool isAttacking;
    private float nextMelee = 0.0f;
    private float impactTime = 0.8f;

    bool jump;

    void Awake()
    {
        //anim = GetComponent<Animator>();

    }


    // Use this for initialization
    void Start()
    {
        instance = this;
        game_started = true;

    }

    // Update is called once per frame
    void Update()
    {

        CharacterController controller = GetComponent<CharacterController>();
        CharacterController ancient_controller = ancient.GetComponent<CharacterController>();

        if (game_started)
        {
            //horizontal_input = Input.GetAxis("LeftJoystickX_P" + joystickString);
            //vertical_input = Input.GetAxis("LeftJoystickY_P" + joystickString); 
            float horizontal_input = Input.GetAxis("Horizontal");
            float vertical_input = Input.GetAxis("Vertical");

            moveDirection = horizontal_input * transform.right + vertical_input * transform.forward;

            #region press X
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
            #endregion

            #region press Z
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                if (!shadow_indicator.activeInHierarchy)
                {
                    shadow_indicator.SetActive(true);
                }
                else
                {
                    shadow_on = true;
                    ancient.SetActive(true);
                    GetComponent<Animation>().Play("attack 3");
                    Vector3 thePosition = shadow_indicator.transform.TransformPoint(Vector3.forward * 0.6f);
                    ancient.transform.position = thePosition;
                    Quaternion rot = new Quaternion();
                    rot.SetLookRotation(transform.forward);
                    ancient.transform.rotation = rot;
                    shadow_indicator.SetActive(false);
                }
            }
            #endregion

            #region press space
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                GetComponent<Animation>().Play("jump");
                ancient.GetComponent<Animation>().Play("jump");
            }
            #endregion

            #region press K
            else if (Input.GetKeyDown(KeyCode.K))
            {
                Melee_Attack();
                //nextMelee = Time.time + meleeRate;
            }
            #endregion

            #region press L
            else if (Input.GetKeyDown(KeyCode.L))
            {
                if (shadow_on)
                {
                    Vector3 temp_vec = Vector3.zero;
                    temp_vec = GetComponent<Transform>().position;
                    GetComponent<Transform>().position = ancient.GetComponent<Transform>().position;
                    ancient.GetComponent<Transform>().position = temp_vec;
                    ancient.GetComponent<Player_shadow>().onChangeShadowFollow();
                }
                
                //nextMelee = Time.time + meleeRate;
            }
            #endregion




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


                if (GetComponent<Animation>().IsPlaying("ready 2"))
                {
                    GetComponent<Animation>().Stop();
                }
                GetComponent<Animation>().PlayQueued("walk");
                if (shadow_on)
                    if (ancient.GetComponent<Animation>().IsPlaying("ready 2"))
                    {
                        ancient.GetComponent<Animation>().Stop();
                    }
                ancient.GetComponent<Animation>().PlayQueued("walk");
                if(!ancient.GetComponent<Player_shadow>().hasEnemyInRange)
                    ancient.transform.Rotate(0, horizontal_input * 150 * Time.deltaTime, 0);
                //ancient.transform.rotation = rot;
            }
            else
            {
                if (GetComponent<Animation>().IsPlaying("walk"))
                {
                    GetComponent<Animation>().Stop();
                }
                GetComponent<Animation>().PlayQueued("ready 2");
                if (shadow_on)
                    if (ancient.GetComponent<Animation>().IsPlaying("walk"))
                    {
                        ancient.GetComponent<Animation>().Stop();
                    }
                ancient.GetComponent<Animation>().PlayQueued("ready 2");
            }


            /*if (GetComponent<CharacterController>().isGrounded)
            {
                moveDirection.y = 0;
            }*/

            controller.Move(moveDirection * Time.deltaTime * 1);
            if (shadow_on)
                ancient_controller.Move(moveDirection * Time.deltaTime * 1);
            if (GetComponent<Animation>()["attack 5"].time > GetComponent<Animation>()["attack 5"].length * 0.90)
                isAttacking = false;
            
        }
    }
    void Melee_Attack()
    {
        GetComponent<Animation>().Play("attack 5");
        isAttacking = true;
        
    }

    void OnTriggerStay(Collider other)
    {
        print("enterrrrrrrrrrrrrrrrrrrrrrrrrrrrrrr");
        if (other.tag == "Enemy")
        {
            if (isAttacking == true)
            {
                print("success!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
                if (GetComponent<Animation>()["attack 5"].time > GetComponent<Animation>()["attack 5"].length * impactTime)
                {
                    other.GetComponent<Enemy>().GetHit(100);
                    isAttacking = false;
                    Debug.Log(other.GetComponent<Enemy>().Health);
                    
                }

            }
        }
    }
}