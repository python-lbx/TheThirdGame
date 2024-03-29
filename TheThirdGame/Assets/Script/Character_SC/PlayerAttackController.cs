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
    public GameObject Lightning_Text;

    [Header("元素披風")]
    public float Cloak_CD;
    public float Cloak_Last_Time;
    public bool Cloak_Pressed;
    public int Cloak_Cost;
    public GameObject Cloak;
    public GameObject Cloak_Icon;
    public GameObject Cloak_Text;

    [Header("無敵")]
    public float currentTime;
    public float newtime;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playermovement = GetComponent<PlayerMovement>();
        playercontroller = GetComponentInChildren<PlayerController>();

        Lightning_Icon.SetActive(false);
        Lightning_Text.SetActive(false);

        Cloak_Icon.SetActive(false);
        Cloak_Text.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // Attack_Pressed = Input.GetKeyDown(KeyCode.Z);
        // Shuriken_Pressed = Input.GetKeyDown(KeyCode.F);
        // Lightning_Pressed = Input.GetKeyDown(KeyCode.X);
        // Cloak_Pressed = Input.GetKeyDown(KeyCode.R);

        Attack_Pressed = Input.GetKeyDown(GameManager.GM.attack);
        Shuriken_Pressed = Input.GetKeyDown(GameManager.GM.shuriken);
        Lightning_Pressed = Input.GetKeyDown(GameManager.GM.s_attack);
        Cloak_Pressed = Input.GetKeyDown(GameManager.GM.shield);


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
                AVmanager.instance.Play("F_Shoot");
                playercontroller.MPBall -= Shuriken_Cost;
                Shuriken_Last_Time = Time.time;
                anim.SetTrigger("IsShoot");
                //Instantiate(Shuriken,ShootPoint.position,transform.rotation);
                Shuriken_Pool.instance.GetFormPool(ShootPoint.transform); //改成對象池生成
            }
        }

        if(playercontroller.MPBall < Lightning_Cost)
        {
            Lightning_Icon.SetActive(false);
            Lightning_Text.SetActive(false);        
        }

        if(playercontroller.MPBall < Cloak_Cost)
        {
            Cloak_Icon.SetActive(false);
            Cloak_Text.SetActive(false);
        }

        if(Time.time >= (Lightning_Last_Time + Lightning_CD))
        {
            if(playercontroller.MPBall >= Lightning_Cost)
            {
                Lightning_Icon.SetActive(true);
                Lightning_Text.SetActive(true);
            }

            if(Lightning_Pressed && !playermovement.IsClimbing && playercontroller.MPBall >= Lightning_Cost)
            {                
                Lightning_Pressed = false;
                AVmanager.instance.Play("X_Spell");
                playercontroller.MPBall -= Lightning_Cost;
                Lightning_Last_Time = Time.time;
                anim.SetTrigger("IsLightning");
                Instantiate(Lightning,LightningPoint.position,transform.rotation);
            }
        }      

        if(Time.time >= (Cloak_Last_Time + Cloak_CD))
        {
            if(playercontroller.MPBall >= Cloak_Cost)
            {
                Cloak_Icon.SetActive(true);
                Cloak_Text.SetActive(true);
            }

            if(Cloak_Pressed && playercontroller.MPBall == Cloak_Cost)
            {
                newtime = 2f;
                Cloak_Pressed = false;

                AVmanager.instance.Play("R_Spell");
                playercontroller.MPBall -= Cloak_Cost;
                Cloak_Last_Time = Time.time;
                Cloak.SetActive(true);
            }
        }  

        if(Time.time >= (Cloak_Last_Time + 2f))
        {
            Cloak.SetActive(false);
        }

        if(newtime > currentTime) //時間更新
        {
            currentTime = newtime;
            newtime = 0;
        }

        if(currentTime > 0) //無敵中
        {
            gameObject.layer = LayerMask.NameToLayer("Invincible");
            currentTime -= Time.deltaTime;
        }
        else if(currentTime <= 0) //結束無敵
        {
            gameObject.layer = LayerMask.NameToLayer("Player");
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
