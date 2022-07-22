using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackController : MonoBehaviour
{
    Animator anim;
    PlayerMovement playermovement;

    [Header("普通攻擊")]
    public GameObject Z_Attack_Box;
    public float Z_Attack_CD;
    public float Z_Last_Time;
    public bool Z_Pressed;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        playermovement = GetComponent<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
        Z_Pressed = Input.GetKeyDown(KeyCode.Z);

        if(Z_Pressed && !playermovement.IsClimbing)
        {
            if(Time.time >= (Z_Last_Time + Z_Attack_CD) )
            {
                Z_Pressed = false;
                Z_Last_Time = Time.time;
                anim.SetTrigger("IsAttack");
            }
        }
    }

    void ZboxActive()
    {
        Z_Attack_Box.SetActive(true);
    }

    void ZboxUnActive()
    {
        Z_Attack_Box.SetActive(false);
    }
}
