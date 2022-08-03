using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Orc_Wizzard : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    public GameObject[] TransPoint;
    public int TransTime;
    public float time;

    public Orc_Bullet DiffusionBullet;
    public Orc_Chase_Bullet OrcChaseBullet;

    [Header("角色面向")]
    public bool faceright;
    [Header("移動速度")]
    public float speed;
    [Header("移動範圍")]
    public Transform leftPoint;
    public Transform rightPoint;




    [Header("當前階段")]
    public Statue current_Statue;
    public float PhaseTime;
    public enum Statue{Idle,PatrolAndShoot,TransAndShoot,Wave,SKillCD}
    [Header("下個技能階段")]
    public int SkillPhase;
    public int SkillTime;
    public float SKillCD;
    public Statue Next_Skill_Statue;
    public bool canShoot;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (SkillPhase)
        {
            case 0:
            Next_Skill_Statue = Statue.PatrolAndShoot;
            break;
            
            case 1:
            Next_Skill_Statue = Statue.TransAndShoot;
            break;

            case 2:
            Next_Skill_Statue = Statue.Wave;
            break;
        }



        switch (current_Statue)
        {
            case Statue.Idle:
            if(PhaseTime > 0)
            {
                PhaseTime -= Time.deltaTime;
            }
            else if(PhaseTime <= 0)
            {
                PhaseTime = 2f;
                current_Statue = Next_Skill_Statue;
            }
            
            break;

            case Statue.PatrolAndShoot:
            if(PhaseTime > 0)
            {
                PhaseTime -= Time.deltaTime;
                Movement();
            }
            else if(PhaseTime <= 0)
            {   
                rb.velocity = new Vector2(0,0);

                if(canShoot)
                {
                anim.SetTrigger("Attack");
                }
            }
            break;

            case Statue.TransAndShoot: //5
      
            break;

            case Statue.Wave: //4
            if(PhaseTime > 0)
            {
                //施法
                PhaseTime -= Time.deltaTime;
            }
            else if(PhaseTime <= 0)
            {
                //移動
                SkillTime ++;
                PhaseTime = 2f;
                if(SkillTime == 4)
                {
                    SkillPhase ++;
                    SkillTime = 0;
                    current_Statue = Statue.Idle;
                }
            }       
            break;

            case Statue.SKillCD:
            if(PhaseTime > 0)
            {
                PhaseTime -= Time.deltaTime;
            }
            else if(PhaseTime <= 0)
            {
                PhaseTime = 2f;
                canShoot = true;
                current_Statue = Next_Skill_Statue;

                if(SkillTime == 3)
                {
                    PhaseTime = 2f;
                    SkillTime = 0;
                    current_Statue = Statue.Idle;
                }
            }
            break;
        }


        //Movement();
    }

    void flip()
    {
        faceright = !faceright;
        transform.Rotate(0,180,0);
    }

    void Movement()
    {
        speed = 6f;
        if(faceright)
        {
            rb.velocity = new Vector2(speed,rb.velocity.y);
            if(transform.position.x > rightPoint.position.x)
            {
                flip();
            }
        }
        else
        {
            rb.velocity = new Vector2(-speed,rb.velocity.y);
            if(transform.position.x < leftPoint.position.x)
            {
                flip();
            }
        }
    }

    void Trans()
    {
        if(time > 0 && TransTime < 5)
        {
            time -= Time.deltaTime;
        }
        else if(time <= 0 )
        {
            anim.SetTrigger("Attack");
            time = 1.5f;
            transform.position = TransPoint[TransTime].transform.position;
            TransTime++;

        }
    }

    void ChaseOrDiffustion()
    {
        if(Next_Skill_Statue == Statue.PatrolAndShoot)
        {
            OrcChaseBullet.ChaseBullet();
            SkillTime ++;
            current_Statue = Statue.SKillCD;
            PhaseTime = 3.5f;
            if(SkillTime == 3)
            {
                SkillPhase ++;
            }
        }
        else if(Next_Skill_Statue == Statue.TransAndShoot)
        {
            //DiffusionBullet;
        }
    }

    void canshoot()
    {
        canShoot = false;
    }
}
