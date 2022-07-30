using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttackController : MonoBehaviour
{
    Animator anim;
    PlayerMovement playermovement;
    PlayerController playercontroller;

    [Header("發射點")]
    public Transform ShootPoint;
    public Transform LightningPoint;

    [Header("普通攻擊")]
    public GameObject Z_Attack_Box;
    public float Z_Attack_CD;
    public float Z_Last_Time;
    public bool Attack_Pressed;

    [Header("手里劍")]
    public float Shuriken_CD;
    public float Shuriken_Last_Time;
    public bool Shuriken_Pressed;
    public int Shuriken_Cost;
    public GameObject Shuriken;

    [Header("雷切")]
    public float Lightning_CD;
    public float Lightning_Last_Time;
    public bool Lightning_Pressed;
    public int Lightning_Cost;
    public GameObject Lightning;
    public GameObject Lightning_Icon;

    [Header("元素披風")]
    public float Cloak_CD;
    public float Cloak_Last_Time;
    public bool Cloak_Pressed;
    public int Cloak_Cost;
    public GameObject Cloak;
    public GameObject Cloak_Icon;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playermovement = GetComponent<PlayerMovement>();
        playercontroller = GetComponentInChildren<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        Attack_Pressed = Input.GetKeyDown(KeyCode.Z);
        Shuriken_Pressed = Input.GetKeyDown(KeyCode.F);
        Lightning_Pressed = Input.GetKeyDown(KeyCode.X);
        Cloak_Pressed = Input.GetKeyDown(KeyCode.R);


        if(Attack_Pressed && !playermovement.IsClimbing)
        {
            Attack_Pressed = false;
            if(Time.time >= (Z_Last_Time + Z_Attack_CD) )
            {
                Z_Last_Time = Time.time;
                anim.SetTrigger("IsAttack");
            }
        }

        if(Shuriken_Pressed && !playermovement.IsClimbing && playercontroller.MPBall >= Shuriken_Cost )
        {
            Shuriken_Pressed = false;
            if(Time.time >= (Shuriken_Last_Time + Shuriken_CD) )
            {                
                playercontroller.MPBall -= Shuriken_Cost;
                Shuriken_Last_Time = Time.time;
                anim.SetTrigger("IsShoot");
                Instantiate(Shuriken,ShootPoint.position,transform.rotation);
            }
        }

        if(Time.time >= (Lightning_Last_Time + Lightning_CD))
        {
            Lightning_Icon.SetActive(true);
            if(Lightning_Pressed && !playermovement.IsClimbing && playercontroller.MPBall >= Lightning_Cost)
            {                
                Lightning_Pressed = false;
                Lightning_Icon.SetActive(false);
                playercontroller.MPBall -= Lightning_Cost;
                Lightning_Last_Time = Time.time;
                anim.SetTrigger("IsLightning");
                Instantiate(Lightning,LightningPoint.position,transform.rotation);
            }
        }      

        if(Time.time >= (Cloak_Last_Time + Cloak_CD))
        {
            Cloak_Icon.SetActive(true);
            if(Cloak_Pressed && !playermovement.IsClimbing && playercontroller.MPBall == Cloak_Cost)
            {
                Cloak_Pressed = false;
                Cloak_Icon.SetActive(false);
                playercontroller.MPBall -= Cloak_Cost;
                Cloak_Last_Time = Time.time;
                Cloak.SetActive(true);
            }
        }  

        if(Time.time >= (Cloak_Last_Time + 2f))
        {
            Cloak.SetActive(false);
        }
    }

    void ZboxActive()
    {
        Z_Attack_Box.GetComponent<BoxCollider2D>().enabled = true;
    }

    void ZboxUnActive()
    {
        Z_Attack_Box.GetComponent<BoxCollider2D>().enabled = false;
    }
}
