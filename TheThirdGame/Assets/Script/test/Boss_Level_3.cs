using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Level_3 : MonoBehaviour
{

    public bool faceright;
    
    [Header("階段")]
    public Statue statue;
    public enum Statue{Focus,Shoot};


    [Header("死光破盾")]
    public GameObject[] shieldList = new GameObject[4];
    public int active_shield_amount;
    public GameObject Target;
    public GameObject shootPoint;
    public GameObject bullet;
    Vector2 Direction;
    Vector2 targetpos;
    public float focustime;
    public float RechargeTime;
    public int shoottime;
    public float speed;

    [Header("魔法陣承受傷害")]
    public GameObject[] magic_circle_List = new GameObject[4];
    public GameObject[] bomb_List = new GameObject[4];
    public float magic_appear_time;
    public float magic_appear_time_CD;
    public float bomb_appear_time;
    public float bomb_appear_time_CD;
    public int i;
    public int j;

    [Header("吃球防炸")]
    public int ballamount;
    public GameObject eigh_ball;
    public GameObject crushwave;

    [Header("彈幕排位")]
    public GameObject FourSideShoot;


    public int[] numbers = { 0, 1, 2, 3 };

    // Start is called before the first frame update
    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player");
        Direction = Target.transform.position;

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
            j = 0;
        }

        if(Input.GetKeyDown(KeyCode.Keypad4)) //死光破盾
        {
            for(int i = 0 ; i < shieldList.Length ; i++)
            {
                shieldList[i].SetActive(true);
            }

            focustime = RechargeTime; //重新充能
            shoottime = 0; //射擊次數歸0
            active_shield_amount = 0;
        }    





        //吃球達次數則施放衝擊波
        if(ballamount == 4)
        {
            eigh_ball.SetActive(false);
            //crushwave.GetComponent<SpriteRenderer>().color = Color.red;
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
                
                print(active_shield_amount);

                shoottime = 5;           
            }
        }

        //魔法陣
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
        

        switch (statue)
        {
            case Statue.Focus:
            targetpos = Target.transform.position;

            Direction = targetpos - (Vector2)shootPoint.transform.position;
            

            if(focustime > 0 && shoottime < 4)
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
            else if(focustime <= 0 && shoottime < 4)
            {
                statue = Statue.Shoot;
            }

            break;

            case Statue.Shoot:
            var bulletshoot = Instantiate(bullet,shootPoint.transform.position,Quaternion.identity);
            bulletshoot.GetComponent<Rigidbody2D>().velocity = shootPoint.transform.right * speed;
            focustime = RechargeTime;

            shoottime ++;

            statue = Statue.Focus;
            break;
        }
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
}
