using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Level_3 : MonoBehaviour
{
    Animator anim;
    public bool faceright;
    
    [Header("階段")]
    public Statue current_Statue;
    public Statue Next_Skill_Statue;
    public enum Statue{Idle,FourSideShoot,EightBall,MagicCircle,LaserShield};
    public float PhaseTime;
    public int SkillPhase;

    [Header("彈幕排位")]
    public GameObject FourSideShoot; //擴散射擊

    [Header("吃球防炸")]
    public int ballamount; //吃球數
    public GameObject eigh_ball; //能量球
    public GameObject crushwave; //衝擊波

    [Header("魔法陣承受傷害")]
    public int[] numbers = { 0, 1, 2, 3 }; //隨機數
    public int i; //魔法陣i
    public int j; //爆炸j
    public GameObject[] magic_circle_List = new GameObject[4]; //魔法陣
    public GameObject[] bomb_List = new GameObject[4]; //魔法陣爆炸
    public float magic_appear_time; //魔法陣出現
    public float magic_appear_time_CD; //魔法陣冷卻
    public float  bomb_appear_time; //爆炸出現
    public float bomb_appear_time_CD; //爆炸冷卻

    [Header("死光破盾")]
    public GameObject[] shieldList = new GameObject[4];
    public int active_shield_amount; //存活護盾數
    public GameObject Target; //目標
    public GameObject originObject; // 圓心 object
    public GameObject Laser; //發射物
    public GameObject LaserShield; //自身護盾
    public float radius = 5f; // 圓的半徑
    Vector2 Direction; //方向
    Vector2 targetpos; //目標位置
    Vector2 origin; // 圓心

    public float focustime; //瞄準時間
    public float RechargeTime; //充填時間
    public int shoottime; //射擊次數

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Target = GameObject.FindGameObjectWithTag("Player");
        origin = originObject.transform.position; // 取得圓心位置    

        shuffleArray(numbers);
        Debug.Log(string.Join(", ", numbers));

        PhaseTime = 2f;

        faceright = true;
    }

    // Update is called once per frame
    void Update()
    {
        #region  可控技能
        // if(Input.GetKeyDown(KeyCode.Keypad1)) //彈幕排位
        // {
        //     FourSideShoot.SetActive(true); 
        // }

        // if(Input.GetKeyDown(KeyCode.Keypad2)) //吃球
        // {
        //     crushwave.SetActive(true);
        //     eigh_ball.SetActive(true);
        // }

        // if(Input.GetKeyDown(KeyCode.Keypad3)) //魔法陣
        // {
        //     shuffleArray(numbers);
        //     Debug.Log(string.Join(", ", numbers));

        //     magic_appear_time = 0; //重新施法
        //     i = 0; //次數歸0
        //     j = 0; //次數歸0
        // }

        // if(Input.GetKeyDown(KeyCode.Keypad4)) //死光破盾
        // {
        //     for(int i = 0 ; i < shieldList.Length ; i++) //監禁開始
        //     {
        //         if(shieldList[i].GetComponentInParent<NPC>().HP > 0) //NPC存活才會觸發
        //         {
        //             shieldList[i].SetActive(true);
        //         }
        //     }

        //     LaserShield.SetActive(true); //自身護盾
        //     focustime = RechargeTime; //重新充能
        //     shoottime = 0; //射擊次數歸0
        //     active_shield_amount = 0;
        // }    
        #endregion


        switch (current_Statue)
        {
            case Statue.Idle: //轉階段 一切以此為先

            faceside();

            if(PhaseTime > 0)
            {
                PhaseTime -= Time.deltaTime;
            }
            else if(PhaseTime <= 0) //倒數結束後才轉階段    
            {
                if(Next_Skill_Statue == Statue.FourSideShoot)
                {
                    if(!FourSideShoot.activeSelf) //未激活才觸發
                    {
                        FourSideShoot.SetActive(true); 
                    }
                }

                else if(Next_Skill_Statue == Statue.EightBall)
                {
                    if(!crushwave.activeSelf) //未激活才觸發
                    {
                        crushwave.SetActive(true);
                    }

                    if(!eigh_ball.activeSelf) //未激活才觸發
                    {
                        eigh_ball.SetActive(true);
                    }
                }

                else if(Next_Skill_Statue == Statue.MagicCircle) //魔法陣重置
                {
                    magic_circle_reset();
                }

                else if(Next_Skill_Statue == Statue.LaserShield) //死光重置
                {
                    focustime = RechargeTime; //重新充能
                    shoottime = 0; //射擊次數歸0

                    active_shield_amount = 0;

                    for(int i = 0 ; i < shieldList.Length ; i++) //監禁開始
                    {
                        if(shieldList[i].GetComponentInParent<NPC>().HP > 0) //NPC存活才會觸發
                        {
                            shieldList[i].SetActive(true);
                        }
                    }
                }

                current_Statue = Next_Skill_Statue;

            }
            break;
        }

        switch (SkillPhase)
        {
            case 0:
            Next_Skill_Statue = Statue.FourSideShoot;

            faceside();

            if(FourSideShoot.GetComponent<FourSideShoot>().i == 4) //4次後轉階段
            {
                PhaseTime = 2f;
                SkillPhase++;

                current_Statue = Statue.Idle; //待機
            }
    
            break;

            case 1:
            //技能2
            //吃球達次數則施放衝擊波
            Next_Skill_Statue= Statue.EightBall;
            
            faceside();

            var balls = eigh_ball.GetComponent<EightBall>();

            //吃達4個且 爆炸後
            //轉階段
            if(ballamount == 4)
            {
                eigh_ball.SetActive(false);
                crushwave.GetComponent<Animator>().SetBool("Start",true);
                //ballamount = 0
                //phasetime = 3
                //skillphase ++
                //statue.idle
            }

            //球達8個且 都被打破
            //轉階段
     
            
            //吃到4個球後球都消失了 直接跳階段 有bug!
            if(balls.i == 8 && ballamount != 4) 
            { 
                foreach(var ball in balls.ballpoint)
                {
                    if(ball.activeSelf)
                    {
                        return;
                    }
                }

                eigh_ball.SetActive(false);

                crushwave.SetActive(false);

                PhaseTime = 2f;
                SkillPhase++;

                current_Statue = Statue.Idle;

            }
 
            break;

            case 2:
            Next_Skill_Statue = Statue.MagicCircle;

            faceside();
            
            if(PhaseTime <= 0)
            {
                magic_circle_countdown(); //魔法陣
            }

            foreach(var bomb in bomb_List)
            {
                if(bomb.activeSelf)
                {
                    return;
                }
            }

            if(j == 4)
            {
                PhaseTime = 2f;
                SkillPhase++;
                magic_circle_reset();

                current_Statue = Statue.Idle; //待機
            }

            break;

            case 3:
            Next_Skill_Statue = Statue.LaserShield;
            
            LaserShield.SetActive(true); //自身護盾

            //死光破盾
            if(shoottime < 4)
            {
                if(focustime > 0)
                {
                    focustime -= Time.deltaTime;
                    Laser.SetActive(true);

                    flamelaserfocus();  //瞄準
                    faceside();
                }
                else if(focustime <= 0)
                {
                    Laser.GetComponent<Animator>().SetBool("Start",true);
                    //碰撞取消
                    //重新充能
                    //射擊次數+1
                    //動畫結束
                    
                }
            }

            //護盾存在則NPC受傷
            if(shoottime == 4)
            {
                if(focustime > 0)
                {
                    focustime -= Time.deltaTime;
                }
                else if(focustime <= 0)
                {
                    foreach(var shield in shieldList)
                    {
                        if(shield.activeSelf)
                        {
                            active_shield_amount++;
                        }
                    }

                    for(int i = 0 ; i < shieldList.Length ; i++)
                    {
                        if(shieldList[i].activeSelf)
                        {
                            shieldList[i].GetComponentInParent<NPC>().GetDamage(1);
                            shieldList[i].SetActive(false);
                        }

                        if(shieldList[i].GetComponentInParent<NPC>().HP > 0)
                        {
                            Cherry_Pool.instance.GetFormPool(shieldList[i].GetComponentInParent<Transform>().Find("Point"));
                        }
                    }
                    
                    print(active_shield_amount);

                    LaserShield.SetActive(false); //自身護盾
                    
                    PhaseTime = 2f;
                    SkillPhase = 0; //迴圈重置

                    focustime = RechargeTime; //重新充能
                    shoottime = 0; //射擊次數歸0
                    active_shield_amount = 0;

                    current_Statue = Statue.Idle; //待機
                    //shoottime = 5;           
                }
            }

            break;
        }

        #region 死光破盾原始碼
        // //死光破盾
        // if(shoottime < 4)
        // {
        //     if(focustime > 0)
        //     {
        //         focustime -= Time.deltaTime;
        //         Laser.SetActive(true);
        //         flamelaserfocus();
        //     }
        //     else if(focustime <= 0)
        //     {
        //         // var obj = Instantiate(Laser,shootPoint.transform.position,Quaternion.identity);
        //         // obj.transform.up = Direction;

        //         // focustime = RechargeTime; //重新充能
        //         // shoottime++;

        //         Laser.GetComponent<Animator>().SetBool("Start",true);
        //     }
        // }

        // //護盾存在則受傷
        // if(shoottime == 4)
        // {
        //     if(focustime > 0)
        //     {
        //         focustime -= Time.deltaTime;
        //     }
        //     else if(focustime <= 0)
        //     {
        //         foreach(var shield in shieldList)
        //         {
        //             if(shield.activeSelf)
        //             {
        //                 active_shield_amount++;
        //             }
        //         }

        //         for(int i = 0 ; i < shieldList.Length ; i++)
        //         {
        //             if(shieldList[i].activeSelf)
        //             {
        //                 shieldList[i].GetComponentInParent<NPC>().GetDamage(1);
        //                 shieldList[i].SetActive(false);
        //             }
        //         }
                
        //         print(active_shield_amount);

        //         LaserShield.SetActive(false); //自身護盾

        //         shoottime = 5;           
        //     }
        // }
        #endregion
        

        
    }

    void shuffleArray<T>(T[] array)
    {
        for (int j = 0; j < array.Length; j++) 
        {
            int randomIndex = Random.Range(j, array.Length);
            T temp = array[j];
            array[j] = array[randomIndex];
            array[randomIndex] = temp;
        }    
    }

    void magic_circle_countdown()
    {
        //i是魔法陣次數
        //j是爆炸次數
        if(i < 4) //魔法陣未達4個
        {
            if(magic_appear_time > 0) //0,1,2,3
            {
                magic_appear_time -= Time.deltaTime;
            }
            else if(magic_appear_time <= 0)
            {
                magic_circle_List[numbers[i]].SetActive(true);
                i++;
                magic_appear_time = magic_appear_time_CD;
            }
        }

        if(i == 4 && j < 4) //魔法陣達4個 且爆炸未達4次
        {
            if(bomb_appear_time > 0)
            {
                bomb_appear_time -= Time.deltaTime;
            }
            else if(bomb_appear_time <= 0)
            {
                bomb_List[numbers[j]].SetActive(true);
                j++;
                bomb_appear_time = bomb_appear_time_CD;
            }        
        }

    }

    void magic_circle_reset()
    {
        bomb_appear_time = bomb_appear_time_CD;
        magic_appear_time = magic_appear_time_CD; //重新施法
        i = 0; //次數歸0
        j = 0; //次數歸0

        shuffleArray(numbers);
        Debug.Log(string.Join(", ", numbers));    
    }

    void flamelaserfocus()
    {
        targetpos = Target.transform.position;

        Direction = targetpos - origin; // 計算方向向量，以圓心為參考點 (3,4)-(0,0) = (3,4) 坐標值

        //shootPoint.transform.up = Direction.normalized; // 瞄準目標 Direction.normalized = 根號(3^2+4^5) = 5 得出  (3/5, 4/5),長度為1的方向向量

        // 計算 shootPoint 在圓上的位置
        Vector2 circlePos = origin + radius * Direction.normalized; //

        float distance = Direction.magnitude; // 使用向量長度計算距離

        if (distance < radius)
        {
            circlePos = origin + Direction.normalized * radius;
        }
        //shootPoint.transform.position = circlePos;

        Laser.transform.position = circlePos;
        Laser.transform.up = Direction.normalized;
    }

    void faceside()
    {
        if(faceright && transform.position.x > Target.transform.position.x )
        {
            faceright = !faceright;
            transform.Rotate(0,180,0);        
        }
        else if(!faceright && transform.position.x < Target.transform.position.x)
        {
            faceright = !faceright;
            transform.Rotate(0,180,0);        
        }
    }
}
