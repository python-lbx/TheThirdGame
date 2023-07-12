using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss_Level_2 : MonoBehaviour
{
    Animator anim;
    Rigidbody2D rb;

    public GameObject Player;

    [Header("當前階段")]
    public Statue current_Statue;
    public float PhaseTime;
    public enum Statue{Idle,WalkAndWave,WalkAndFire,Teleport,Laser,skillCD,GameOver}

    [Header("下個技能階段")]
    public int SkillPhase;
    public int NumberOfSkillCasts; //技能施放次數
    public int NumberOfPhase; //技能階段次數
    public float SkillEnterCD; 
    public float SkillCD;
    public Statue Next_Skill_Statue;

    [Header("地動熱波")]
    public Transform left;
    public Transform right;
    public float FlameMoveSpeed;
    [Header("烽火連城")]
    public GameObject quartetflame;
    [Header("熔岩死光")]
    public GameObject TransToAir;
    public GameObject BackToGround;
    public GameObject Laser;
    public GameObject Laser_Trigger_PS;
    public GameObject shield;

    [Header("角色移動")]
    public bool faceright;
    public float speed;
    public Transform leftPoint;
    public Transform rightPoint;

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
        Dialog.text = "這次沒那麼好運";
    }


    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        //角色迴圈思路
        #region 
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
        #endregion

        //對話框位置
        DialogTable.transform.position = TextPoint.transform.position;

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
                anim.SetBool("Run",true);
                SkillEnterCD = 3f; //地動熱波間隔
                PhaseTime -= Time.deltaTime;
                Movement();
            }
            else if(PhaseTime <= 0)
            {   
                //地動熱波對話開始
                DialogTable.SetActive(true);
                Dialog.text = "快逃吧";

                anim.SetBool("Run",false);
                rb.velocity = new Vector2(0,0);

                //轉向
                if(faceright && transform.position.x > Player.transform.position.x )
                {
                    flip();
                }
                else if(!faceright && transform.position.x < Player.transform.position.x)
                {
                    flip();
                }

                MoveFrameWave();

                if(NumberOfSkillCasts == 3) //第三次
                {   
                    //地動熱波對話結束
                    DialogTable.SetActive(false);
                    Dialog.text = "";

                    NumberOfPhase ++; //波數+1
                    PhaseTime = 2f; //待機 Idle時間
                    NumberOfSkillCasts = 0;
                    current_Statue = Statue.Idle;
                }

                if(NumberOfPhase == 3)//第三波
                {
                    SkillPhase++;
                    PhaseTime = 2f; //待機 Idle時間
                    NumberOfPhase = 0;
                }
            }

            break;

            case Statue.WalkAndFire:
            quartetflame.SetActive(true);

            anim.SetBool("Run",true);

            Movement();

            //烽火連城對話
            if(quartetflame.GetComponent<Random_Fire>().canshoot)
            {
                DialogTable.SetActive(true);
                Dialog.text = "開火!!射擊!!";
            }
            else
            {
                DialogTable.SetActive(false);
                Dialog.text = "";
            }

            if(quartetflame.GetComponent<Random_Fire>().finished)
            {   
                anim.SetBool("Run",false);

                quartetflame.SetActive(false);
                SkillPhase++;
                PhaseTime = 2f; //待機時間
                current_Statue = Statue.Idle;
            }            
            break;

            case Statue.Teleport:
            transform.position = TransToAir.transform.position;
            
            if(PhaseTime > 0)
            {   
                //傳送對話
                DialogTable.SetActive(true);
                Dialog.text = "還沒完";

                PhaseTime -= Time.deltaTime;
            }
            else if(PhaseTime <= 0)
            {
                //施法對話
                DialogTable.SetActive(true);
                Dialog.text = "更強的力量...";

                anim.SetBool("Spelling",true);
                shield.SetActive(true);

                Laser_Trigger_PS.SetActive(true);
                SkillPhase++;
                current_Statue = Next_Skill_Statue;
            }
            break;

            case Statue.Laser:

            //死光時無敵
            gameObject.layer = LayerMask.NameToLayer("Invincible");

            if(Laser.GetComponent<Rotate_Laser_Controller>().allshutdown)
            {
                anim.SetBool("Spelling",false);
                shield.SetActive(false);
                PhaseTime = 3f;
                SkillPhase = 0;
                current_Statue = Statue.skillCD;
            }
            break;

            case Statue.skillCD:

            //無敵直至切換到Idle;
            gameObject.layer = LayerMask.NameToLayer("Invincible");

            if(PhaseTime > 0)
            {
                DialogTable.SetActive(true);
                Dialog.text = "可惡...";

                PhaseTime -= Time.deltaTime;
            }
            else if(PhaseTime <= 0)
            {
                DialogTable.SetActive(false);
                Dialog.text = "";

                PhaseTime = 2f;
                transform.position = BackToGround.transform.position;

                //落地解除無敵
                current_Statue = Statue.Idle;
            }
            break;

            case Statue.GameOver:
                StopEveryThing();
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
                anim.SetTrigger("Attack");
                AVmanager.instance.Play("Wizard_FireSpell_4");

                var movewaveleft = MoveWave_Pool.instance.GetFormPool(left);
                var movewaveright= MoveWave_Pool.instance.GetFormPool(right);

                movewaveleft.GetComponent<Rigidbody2D>().velocity = new Vector2(-FlameMoveSpeed,0);
                movewaveright.GetComponent<Rigidbody2D>().velocity = new Vector2(FlameMoveSpeed,0);
    
                SkillCD = SkillEnterCD;

                NumberOfSkillCasts++;
            }
        }
    }

    public void StopEveryThing()
    {
        //動畫關
        anim.SetBool("Run",false);
        anim.SetBool("Spelling",false);
        //技能關
        quartetflame.SetActive(false);

        Laser_Trigger_PS.SetActive(false);
        Laser.GetComponent<Rotate_Laser_Controller>().laser.SetActive(false);
        Laser.SetActive(false);

        shield.SetActive(false);
        //動量歸零
        GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
    }
}
