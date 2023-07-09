using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Boo : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    [Header("狀態數值")]
    //public float speed;
    public bool faceright;

    [Header("攻擊目標")]
    public GameObject Target;
    public GameObject shootPoint;
    Vector2 Direction;
    Vector2 targetpos;
    public float focustime;
    public float RechargeTime;

    [Header("狀態")]
    public Enemy_State enemy_State;
    
    [Header("階段")]
    public Statue statue;
    public enum Statue{Focus,Shoot};
    //public float PhaseTime;

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
        Direction = Target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {

        switch(enemy_State.current_Statue)
        {
            case Enemy_State.Statue.Fight:
            enemy_State.current_Statue = Enemy_State.Statue.Idle; //跳出迴圈
            statue = Statue.Focus; //開始戰鬥
            break;

            case Enemy_State.Statue.Dead:
            this.enabled = false; //停止腳本
            break;
        }

        switch (statue)
        {
            case Statue.Focus:
            targetpos = Target.transform.position;

            Direction = targetpos - (Vector2)shootPoint.transform.position;
            

            if(focustime > 0)
            {
                focustime -= Time.deltaTime;
                
                shootPoint.transform.right = Direction;

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

            }
            else
            {
                statue = Statue.Shoot;
            }

            break;

            case Statue.Shoot:
            anim.SetTrigger("Attack");
            AVmanager.instance.Play("Boo_Shoot");
            focustime = RechargeTime;
            statue = Statue.Focus;
            break;
        }
    }

    public void shootboo()
    {
        var boo = Orc_Boomerang_Pool.instance.GetFormPool(shootPoint.transform);
        boo.GetComponent<Boomerang>().enemycontroller = GetComponent<EnemyController>();
        boo.GetComponent<Rigidbody2D>().velocity = shootPoint.transform.right * boo.GetComponent<Boomerang>().speed;
    }
}
