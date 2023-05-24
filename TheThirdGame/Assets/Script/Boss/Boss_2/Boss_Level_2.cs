using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Level_2 : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;

    [Header("當前階段")]
    public Statue current_Statue;
    public float PhaseTime;
    public enum Statue{Idle,WalkAndWave,WalkAndFire,Teleport,Laser,skillCD}

    [Header("下個技能階段")]
    public int SkillPhase;
    public int NumberOfSkillCasts; //技能施放次數
    public int NumberOfPhase; //技能階段次數
    public float SkillEnterCD; 
    public float SkillCD;
    public Statue Next_Skill_Statue;
    public bool canShoot;

    [Header("地動熱波")]
    public Transform left;
    public Transform right;
    public float FlameMoveSpeed;
    public GameObject movewave;
    [Header("烽火連城")]
    public GameObject quartetflame;
    [Header("熔岩死光")]
    public GameObject TransToAir;
    public GameObject BackToGround;
    public GameObject Laser;

    [Header("角色移動")]
    public bool faceright;
    public float speed;
    public Transform leftPoint;
    public Transform rightPoint;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //待機
        //移動 數秒後原地放技能
        //地熱波 施放三次 每次2-3波 三次後進入待機
        //待機
        //移動 數秒後持續移動
        //烽光連城 施放四次 四次後進入待機
        //待機
        //移動 數秒後原地放技能
        //地熱波 施放三次 每次2-3波 三次後進入待機
        //待機
        //傳送中間 
        //毀滅死光
        //傳送地面
        //待機
        //迴圈繼續
        switch (SkillPhase)
        {
            case 0:
            Next_Skill_Statue = Statue.WalkAndWave;
            break;
            
            case 1:
            Next_Skill_Statue = Statue.WalkAndFire;
            break;

            case 2:
            Next_Skill_Statue = Statue.WalkAndWave;
            break;

            case 3:
            Next_Skill_Statue = Statue.WalkAndFire;
            break;

            case 4:
            Next_Skill_Statue = Statue.Teleport;
            break;

            case 5:
            Next_Skill_Statue = Statue.Laser;
            break;
        }

        switch(current_Statue)
        {
            case Statue.Idle:
            if(PhaseTime > 0)
            {
                rb.velocity = new Vector2(0,0);

                PhaseTime -= Time.deltaTime;
            }
            else if(PhaseTime <= 0)
            {
                SkillCD = SkillEnterCD; //地動熱波間隔
                PhaseTime = Random.Range(2.5f,4f); //移動時間
                NumberOfSkillCasts = 0;

                if(SkillPhase == 4) //傳送階段
                {           
                    PhaseTime = 3f; //傳送時間
                }

                current_Statue = Next_Skill_Statue;
            }
            break;

            case Statue.WalkAndWave:
            if(PhaseTime > 0)
            {   
                SkillEnterCD = 3f; //地動熱波間隔
                PhaseTime -= Time.deltaTime;
                Movement();
            }
            else if(PhaseTime <= 0)
            {
                rb.velocity = new Vector2(0,0);

                MoveFrameWave();

                if(NumberOfSkillCasts == 3) //第三次
                {
                    NumberOfPhase ++; //波數+1
                    PhaseTime = 2f; //待機 Idle時間
                    NumberOfSkillCasts = 0;
                    current_Statue = Statue.Idle;
                }

                if(NumberOfPhase == 3)//第三波
                {
                    SkillPhase++;
                    PhaseTime = 4f; //待機 Idle時間
                    NumberOfPhase = 0;
                }
            }

            break;

            case Statue.WalkAndFire:
            quartetflame.SetActive(true);

            Movement();

            if(quartetflame.GetComponent<Random_Fire>().finished)
            {
                quartetflame.SetActive(false);
                SkillPhase++;
                PhaseTime = 3f; //待機時間
                current_Statue = Statue.Idle;
            }
            break;

            case Statue.Teleport:
            transform.position = TransToAir.transform.position;
            
            if(PhaseTime > 0)
            {
                PhaseTime -= Time.deltaTime;
            }
            else if(PhaseTime <= 0)
            {
                Laser.SetActive(true);
                SkillPhase++;
                current_Statue = Next_Skill_Statue;
            }
            break;

            case Statue.Laser:
            if(!Laser.activeSelf)
            {
                PhaseTime = 3f;
                SkillPhase = 0;
                transform.position = BackToGround.transform.position;
                current_Statue = Statue.Idle;
            }
            break;

            case Statue.skillCD:
            break;

        }
    }

    
    void flip()
    {
        faceright = !faceright;
        transform.Rotate(0,180,0);
        FlameMoveSpeed = -FlameMoveSpeed;
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

    void MoveFrameWave()
    {
        for(int i = 0 ; i < 3 ; i++)
        {
            if(SkillCD > 0)
            {
                SkillCD -= Time.deltaTime;
            }
            else if(SkillCD <= 0)
            {
                var movewaveleft = MoveWave_Pool.instance.GetFormPool(left);
                movewaveleft.GetComponent<Rigidbody2D>().velocity = new Vector2(-FlameMoveSpeed,0);
                var movewaveright= MoveWave_Pool.instance.GetFormPool(right);
                movewaveright.GetComponent<Rigidbody2D>().velocity = new Vector2(FlameMoveSpeed,0);

                SkillCD = SkillEnterCD;

                NumberOfSkillCasts++;
            }
        }
    }
}
