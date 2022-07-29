using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    Animator anim;
    PlayerMovement playermovement;
    PlayerController playercontroller;

    [Header("發射點")]
    public Transform ShootPoint;

    [Header("普通攻擊")]
    public GameObject Z_Attack_Box;
    public float Z_Attack_CD;
    public float Z_Last_Time;
    public bool Attack_Pressed;

    [Header("手里劍")]
    public float Shuriken_CD;
    public float Shuriken_Last_Time;
    public bool Shuriken_Pressed;
    public GameObject Shuriken;

    [Header("雷切")]
    public GameObject lightning;

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



        if(Attack_Pressed && !playermovement.IsClimbing)
        {
            if(Time.time >= (Z_Last_Time + Z_Attack_CD) )
            {
                Attack_Pressed = false;
                Z_Last_Time = Time.time;
                lightning.SetActive(true);
                anim.SetTrigger("IsAttack");
            }
        }

        if(Shuriken_Pressed && !playermovement.IsClimbing && playercontroller.MPBall > 0)
        {
            if(Time.time >= (Shuriken_Last_Time + Shuriken_CD) )
            {
                playercontroller.MPBall -= 1;
                Shuriken_Pressed = false;
                Shuriken_Last_Time = Time.time;
                anim.SetTrigger("IsShoot");
                Instantiate(Shuriken,ShootPoint.position,transform.rotation);
            }
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
