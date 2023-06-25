using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Fly : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    [Header("狀態數值")]
    public float speed;
    public bool faceright;

    [Header("攻擊目標")]
    public GameObject Target;
    public GameObject FocusPoint;
    Vector2 Direction;
    Vector2 targetpos;
    public float focustime;
    public GameObject Attack_Box;

    
    // [Header("無視平台")]
    // [SerializeField] Collider2D playerCollider;
    // public GameObject[] currentOneWayPlatform;

    [Header("狀態")]
    public Enemy_State enemy_State;

    [Header("階段")]
    public Statue statue;
    public enum Statue{Focus,Rush};
    public float PhaseTime;
    // Start is called before the first frame update
    private void Awake() 
    {
        this.enabled = true;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        enemy_State = GetComponent<Enemy_State>();

        Target = GameObject.FindGameObjectWithTag("Player");
        Direction = Target.transform.position;

        // //多於一個
        // currentOneWayPlatform = GameObject.FindGameObjectsWithTag("OneWayPlatform");

        // //無視所有
        // foreach(GameObject gameObject in currentOneWayPlatform)
        // {
        //     Physics2D.IgnoreCollision(playerCollider,gameObject.GetComponent<BoxCollider2D>());
        // }

        //BoxCollider2D platformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();
        //Physics2D.IgnoreCollision(playerCollider,platformCollider);
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
            anim.SetBool("Attack",false);

            //瞄準
            targetpos = Target.transform.position;

            Direction = targetpos - (Vector2)transform.position;
            
            if(focustime > 0)
            {
                focustime -= Time.deltaTime;

                transform.right = Direction;

                if(Target.transform.position.x < transform.position.x && faceright)
                {
                    faceright = false;
                    GetComponent<SpriteRenderer>().flipY = true;
                    //print("on your left");
                }
                else if(Target.transform.position.x > transform.position.x && !faceright)
                {
                    faceright = true;
                    GetComponent<SpriteRenderer>().flipY = false;
                    //print("on your right");
                }  
            }
            else
            {
                rb.velocity = transform.right * speed;
                PhaseTime = 2f;
                statue = Statue.Rush;
            }
            break;

            case Statue.Rush:
            anim.SetBool("Attack",true);

            if(PhaseTime > 0)
            {
                PhaseTime -= Time.deltaTime;
            }
            else if(PhaseTime <= 0)
            {
                rb.velocity = new Vector2(0,0);
                focustime = 3f;
                statue = Statue.Focus;
            }
            break;

        }

        #region  
                    /*switch (statue)
        {
            case Statue.Focus:
                anim.SetBool("Attack",false);

                if(PhaseTime > 0)
                {
                    PhaseTime -= Time.deltaTime;

                    Direction = Target.transform.position;

                    if(Target.transform.position.x < transform.position.x && faceright)
                    {
                        faceright = false;
                        transform.Rotate(0,180,0);
                        print("on your left");
                    }
                    else if(Target.transform.position.x > transform.position.x && !faceright)
                    {
                        faceright = true;
                        transform.Rotate(0,180,0);
                        print("on your right");
                    }   
                }
                else if(PhaseTime <= 0)
                {   
                    statue = Statue.Rush;
                }
            break;

            case Statue.Rush:
                anim.SetBool("Attack",true);

                transform.position = Vector2.MoveTowards(transform.position,Direction,speed * Time.deltaTime);

                if(Mathf.Abs(transform.position.x - Direction.x) < 0.1f)
                {
                    PhaseTime = 3f;
                    statue = Statue.Focus;
                }
            break;
        }*/

        #endregion

    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        //撞牆停止
        if(other.gameObject.CompareTag("Ground"))
        {
            rb.velocity = new Vector2(0,0);
            focustime = 3f;
            PhaseTime = 0f;
            statue = Statue.Focus;
            //print("A");
        }

        //print(other.gameObject.name);
    }

    void ZboxActive()
    {
        Attack_Box.GetComponent<BoxCollider2D>().enabled = true;
    }

    void ZboxUnActive()
    {
        Attack_Box.GetComponent<BoxCollider2D>().enabled = false;
    }
}
