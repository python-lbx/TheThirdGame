using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Level_3 : MonoBehaviour
{

    public bool faceright;
    
    // [Header("階段")]
    // public Statue statue;
    // public enum Statue{Focus,Shoot};


    [Header("死光破盾")]
    public GameObject[] shieldList = new GameObject[4];
    public int active_shield_amount; //存活護盾數
    public GameObject Target; //目標
    public GameObject originObject; // 圓心 object
    public GameObject Laser; //發射物
    public GameObject ownSheild; //自身護盾
    public float radius = 5f; // 圓的半徑
    Vector2 Direction; //方向
    Vector2 targetpos; //目標位置
    Vector2 origin; // 圓心

    public float focustime; //瞄準時間
    public float RechargeTime; //充填時間
    public int shoottime; //射擊次數

    [Header("魔法陣承受傷害")]
    public GameObject[] magic_circle_List = new GameObject[4]; //魔法陣
    public GameObject[] bomb_List = new GameObject[4]; //魔法陣爆炸
    public float magic_appear_time; //魔法陣出現
    public float magic_appear_time_CD; //魔法陣冷卻
    public float bomb_appear_time; //爆炸出現
    public float bomb_appear_time_CD; //爆炸冷卻
    public int i; //魔法陣i
    public int j; //爆炸j

    [Header("吃球防炸")]
    public int ballamount; //吃球數
    public GameObject eigh_ball; //能量球
    public GameObject crushwave; //衝擊波

    [Header("彈幕排位")]
    public GameObject FourSideShoot; //擴散射擊

    public int[] numbers = { 0, 1, 2, 3 }; //隨機數

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        origin = originObject.transform.position; // 取得圓心位置    

        shuffleArray(numbers);
        Debug.Log(string.Join(", ", numbers));

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Keypad1)) //彈幕排位
        {
            FourSideShoot.SetActive(true); 
        }

        if(Input.GetKeyDown(KeyCode.Keypad2)) //吃球
        {
            crushwave.SetActive(true);
            eigh_ball.SetActive(true);
        }

        if(Input.GetKeyDown(KeyCode.Keypad3)) //魔法陣
        {
            shuffleArray(numbers);
            Debug.Log(string.Join(", ", numbers));

            magic_appear_time = 0; //重新施法
            i = 0; //次數歸0
            j = 0; //次數歸0
        }

        if(Input.GetKeyDown(KeyCode.Keypad4)) //死光破盾
        {
            for(int i = 0 ; i < shieldList.Length ; i++)
            {
                shieldList[i].SetActive(true);
            }

            ownSheild.SetActive(true);
            focustime = RechargeTime; //重新充能
            shoottime = 0; //射擊次數歸0
            active_shield_amount = 0;
        }    

        //技能2
        //吃球達次數則施放衝擊波
        if(ballamount == 4)
        {
            eigh_ball.SetActive(false);
            //crushwave.GetComponent<SpriteRenderer>().color = Color.red;
        }

        magic_circle_countdown(); //魔法陣

        //死光破盾
        if(shoottime < 4)
        {
            if(focustime > 0)
            {
                focustime -= Time.deltaTime;
                Laser.SetActive(true);
                flamelaser();
            }
            else if(focustime <= 0)
            {
                // var obj = Instantiate(Laser,shootPoint.transform.position,Quaternion.identity);
                // obj.transform.up = Direction;

                // focustime = RechargeTime; //重新充能
                // shoottime++;

                Laser.GetComponent<Animator>().SetBool("Start",true);
            }
        }

        //護盾存在則受傷
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
                        shieldList[i].GetComponentInParent<NPC>().HP --;
                        shieldList[i].SetActive(false);
                    }
                }
                
                ownSheild.SetActive(false);

                print(active_shield_amount);

                shoottime = 5;           
            }
        }


        

        // switch (statue)
        // {
        //     case Statue.Focus:
        //     targetpos = Target.transform.position;

        //     Direction = targetpos - (Vector2)shootPoint.transform.position;
            

        //     if(focustime > 0 && shoottime < 4)
        //     {
        //         focustime -= Time.deltaTime;
                
        //         shootPoint.transform.right = Direction;

        //         if(Target.transform.position.x < transform.position.x && faceright)
        //         {
        //             faceright = false;
        //             transform.Rotate(0,180,0);
        //             print("on your left");
        //         }
        //         else if(Target.transform.position.x > transform.position.x && !faceright)
        //         {
        //             faceright = true;
        //             transform.Rotate(0,180,0);
        //             print("on your right");
        //         }  

        //     }
        //     else if(focustime <= 0 && shoottime < 4)
        //     {
        //         statue = Statue.Shoot;
        //     }

        //     break;

        //     case Statue.Shoot:
        //     var bulletshoot = Instantiate(bullet,shootPoint.transform.position,Quaternion.identity);
        //     bulletshoot.GetComponent<Rigidbody2D>().velocity = shootPoint.transform.right * speed;
        //     focustime = RechargeTime;

        //     shoottime ++;

        //     statue = Statue.Focus;
        //     break;
        // }
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
        if(magic_appear_time > 0 && i < 4) //0,1,2,3
        {
            magic_appear_time -= Time.deltaTime;
        }
        else if(magic_appear_time <= 0)
        {
            magic_circle_List[numbers[i]].SetActive(true);
            i++;
            magic_appear_time = magic_appear_time_CD;
        }

        if(bomb_appear_time > 0 && j < 4 && i == 4)
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

    void flamelaser()
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
}