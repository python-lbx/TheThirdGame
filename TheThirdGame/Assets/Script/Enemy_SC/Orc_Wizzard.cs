using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Wizzard : MonoBehaviour
{
    Animator anim;

    [Header("狀態數值")]
    public bool faceright;
    [Header("攻擊目標")]
    public GameObject Target;

    [Header("狀態")]
    public Enemy_State enemy_State;

    [Header("階段")]
    public Statue statue;
    public enum Statue{Idle,Spell};
    public float PhaseTime;
    public float RechargeTime;

    private void Awake() 
    {
        this.enabled = true;
    }
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        enemy_State = GetComponent<Enemy_State>();
        Target = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {

        if(Target.transform.position.x < transform.position.x && faceright)
        {
            faceright = false;
            transform.Rotate(0,180,0);
            //print("on your left");
        }
        else if(Target.transform.position.x > transform.position.x && !faceright)
        {
            faceright = true;
            transform.Rotate(0,180,0);
            //print("on your right");
        }        
        
        switch(enemy_State.current_Statue)
        {
            case Enemy_State.Statue.Fight:
            enemy_State.current_Statue = Enemy_State.Statue.Idle; //跳出迴圈
            statue = Statue.Idle; //開始戰鬥
            break;

            case Enemy_State.Statue.Dead:
            this.enabled = false; //停止腳本
            break;
        }
        
        switch (statue)
        {
            case Statue.Idle:
            if(PhaseTime > 0)
            {
                PhaseTime -= Time.deltaTime;
            }
            else if(PhaseTime <= 0)
            {
                statue = Statue.Spell;
            }
            break;

            case Statue.Spell:
            anim.SetTrigger("Attack");
            PhaseTime = RechargeTime;
            statue = Statue.Idle;
            break;
        }
    }
}
