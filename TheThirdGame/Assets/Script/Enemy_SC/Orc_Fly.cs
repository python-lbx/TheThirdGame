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
    
    [Header("無視平台")]
    [SerializeField] Collider2D playerCollider;
    public GameObject currentOneWayPlatform;


    [Header("階段")]
    public Statue statue;
    public enum Statue{Focus,Rush};
    public float PhaseTime;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Target = GameObject.FindGameObjectWithTag("Player");
        Direction = Target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {   
        
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
                    print("on your left");
                }
                else if(Target.transform.position.x > transform.position.x && !faceright)
                {
                    faceright = true;
                    GetComponent<SpriteRenderer>().flipY = false;
                    print("on your right");
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
            statue = Statue.Focus;
        }

        //無視平台
        if(other.gameObject.CompareTag("OneWayPlatform"))
        {
            currentOneWayPlatform = other.gameObject;
            BoxCollider2D platformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();
            Physics2D.IgnoreCollision(playerCollider,platformCollider);
        }    
    }
}
