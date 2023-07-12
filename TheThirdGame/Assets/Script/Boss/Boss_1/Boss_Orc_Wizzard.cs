using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Boss_Orc_Wizzard : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;
    public GameObject Player;
    [Header("傳送射擊")]
    public Orc_Bullet DiffusionBullet;
    public GameObject[] TransPoint;
    public int NumOfTrans; //傳送次數
    public float TransCD;
    public GameObject BackToGround;

    [Header("地熱波")]
    public GroundWave GroundWave;
    public GameObject TransToAir;

    [Header("移動射擊")]
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
    public enum Statue{Idle,PatrolAndShoot,TransAndShoot,Wave,SKillCD,GameOver}
    [Header("下個技能階段")]
    public int SkillPhase;
    public int SkillTime;
    public float SKillCD;
    public Statue Next_Skill_Statue;
    public bool canShoot;

    [Header("對話框")]
    public GameObject DialogTable;
    public GameObject TextPoint;
    public Text Dialog;

    // Start is called before the first frame update

    private void OnEnable() 
    {
        Player = GameObject.Find("Player");
        DialogTable.transform.position = TextPoint.transform.position;
        DialogTable.SetActive(true);
        Dialog.text = "驅逐入侵者";
    }
    void Start()
    {

        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        DialogTable.transform.position = TextPoint.transform.position;

        switch (SkillPhase)
        {
            case 0:
            Next_Skill_Statue = Statue.PatrolAndShoot;
            break;
            
            case 1:
            Next_Skill_Statue = Statue.TransAndShoot;
            break;
            
            case 2:
            Next_Skill_Statue = Statue.PatrolAndShoot;
            break;

            case 3:
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
                DialogTable.SetActive(false);
                Dialog.text = "";

                PhaseTime = 2f;
                current_Statue = Next_Skill_Statue;
            }
            
            break;

            case Statue.PatrolAndShoot:
            if(PhaseTime > 0)
            {
                anim.SetBool("Run",true);
                canShoot = true;
                PhaseTime -= Time.deltaTime;
                Movement();
            }
            else if(PhaseTime <= 0)
            {        
                DialogTable.SetActive(true);
                Dialog.text = "滾開";
                AVmanager.instance.Play("Wizard_FireSpell_1");

                anim.SetBool("Run",false);
                rb.velocity = new Vector2(0,0);

                if(canShoot)
                {
                    //轉向
                    if(faceright && transform.position.x > Player.transform.position.x )
                    {
                        flip();
                    }
                    else if(!faceright && transform.position.x < Player.transform.position.x)
                    {
                        flip();
                    }
                    anim.SetTrigger("Attack");//攻擊
                    canShoot = false;
                }
            }
            break;

            case Statue.TransAndShoot: //5
            DialogTable.SetActive(true);
            Dialog.text = "無處可逃";

            rb.gravityScale = 0;
            TransShoot();
            break;

            case Statue.Wave: //4
            rb.gravityScale = 0;
            if(PhaseTime > 0)
            {
                //施法
                PhaseTime -= Time.deltaTime;
                transform.position = TransToAir.transform.position;
                canShoot = true;
            }
            else if(PhaseTime <= 0)
            {
                DialogTable.SetActive(true);
                Dialog.text = "感受我的憤怒";

                GroundWave.Ground_Wave();
            } 

            if(GroundWave.NumOfWave == 4 && canShoot)
            {
                canShoot = false;
                Invoke("BackToGroundAfterWave",5f);
            }      
            break;

            case Statue.SKillCD:
            if(PhaseTime > 0)
            {
                PhaseTime -= Time.deltaTime;
            }
            else if(PhaseTime <= 0)
            {
                DialogTable.SetActive(false);
                Dialog.text = "";


                PhaseTime = 2f; //巡邏時間
                canShoot = true; //可以射擊
                current_Statue = Next_Skill_Statue; //回到當前階段

                if(SkillTime == 3) // 當技能次數達到三時
                {
                    PhaseTime = 2f; //待機時間
                    SkillTime = 0; //技能次數清零
                    current_Statue = Statue.Idle; //回到待機狀態
                }
            }
            break;
            
            case Statue.GameOver:
                StopEveryThing();
            break;
        }


        //Movement();
    }

    void flip() //轉向
    {
        faceright = !faceright;
        transform.Rotate(0,180,0);
    }

    void Movement() //移動
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

    void TransShoot()
    {
        if(TransCD > 0 && NumOfTrans < 6)
        {
            TransCD -= Time.deltaTime;
        }
        else if(TransCD <= 0 )
        {
            transform.position = TransPoint[NumOfTrans].transform.position; //傳送位置

            //轉向
            if(faceright && transform.position.x > Player.transform.position.x )
            {
                flip();
            }
            else if(!faceright && transform.position.x < Player.transform.position.x)
            {
                flip();
            }
    
            anim.SetTrigger("Attack");//攻擊

            AVmanager.instance.Play("Wizard_FireSpell_1");

            TransCD = 1.5f; //傳送間隔
            NumOfTrans++; //傳送次數
        }

        if(TransCD <= 0 && NumOfTrans == 5) //達到傳送上限
        {
            transform.position = BackToGround.transform.position;
            SkillPhase ++; //階段++
            PhaseTime = 2f; //待機時間
            SkillTime = 0; //技能次數清零
            current_Statue = Statue.Idle; //回到待機狀態
            NumOfTrans = 0;
            TransCD = 0;

            foreach(GameObject obj in TransPoint)
            {
                obj.GetComponent<TransTest>().ChangePos();
            }
        }
    }

    void ChaseOrDiffustion()
    {
        if(Next_Skill_Statue == Statue.PatrolAndShoot)
        {
            OrcChaseBullet.ChaseBullet(); //子彈追蹤
            SkillTime ++; //技能次數++
            current_Statue = Statue.SKillCD; //進入技能CD
            PhaseTime = 3.5f; //技能CD時間
            if(SkillTime == 3) //技能次數達三次時
            {
                SkillPhase ++;  //階段++
            }
        }
        else if(Next_Skill_Statue == Statue.TransAndShoot)
        {
            DiffusionBullet.SpawnProjectiles();
        }
    }
    
    void BackToGroundAfterWave()
    {
        transform.position = BackToGround.transform.position; //回到地面
        SkillPhase = 0; //階段清零
        GroundWave.NumOfWave = 0; //地熱波次數清零
        PhaseTime = 2; //待機時間
        current_Statue = Statue.Idle; //待機狀態
    }

    public void StopEveryThing()
    {
        anim.SetBool("Run",false);
        rb.velocity = new Vector2(0,0);
    }
}
