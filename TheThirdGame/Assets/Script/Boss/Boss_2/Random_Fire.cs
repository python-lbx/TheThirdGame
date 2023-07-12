using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Fire : MonoBehaviour
{

    [Header("傳送移動")]
    public bool finished;
    public float speed;
    public float teleportcd;
    public float teleporttime;
    public int i;
    public int num;
    public int[] numbers = { 0, 1, 2, 3 };
    public Transform[] Teleport;
    public Transform End;

    [Header("火球射擊")]
    public ShootDir SD;
    public enum ShootDir { up , down , left , right};
    public GameObject FireBall;
    public float fireballspeed;
    public float shootTime;
    public float shootcd;
    public bool canshoot;

    private void OnEnable() 
    {
        finished = false;
        shuffleArray(numbers);
        Debug.Log(string.Join(", ", numbers));
        i = 0;

        canshoot = true;

        num = Random.Range(0,2);
        transform.position = Teleport[numbers[i]].position; //列表第一個位開始 i為0
        teleporttime = teleportcd;
        print(Teleport[numbers[i]]);
    }
    // Start is called before the first frame update
    void Start()
    {
        // shuffleArray(numbers); //洗牌
        // Debug.Log(string.Join(", ", numbers)); //新序列

        // canshoot = true;

        // num = Random.Range(0,2); //新方向
        // transform.position = Teleport[numbers[i]].position; //起始位置
        // print(Teleport[numbers[i]]); //印出起始位置
    }

    // Update is called once per frame
    void Update()
    {   
        #region 
        //正確流程 傳送到起始位置->移動到終點->完成一次->達到四次後重新洗牌
        // if(Input.GetKeyDown(KeyCode.Space)) //傳送
        // {
        //     num = Random.Range(0,2);
        //     transform.position = Teleport[numbers[i]].position; //列表第一個位開始 i為0

        //     print(Teleport[numbers[i]]);

        // }

        // if(Input.GetKeyDown(KeyCode.S)) //洗牌 reset
        // {
        //     shuffleArray(numbers);
        //     Debug.Log(string.Join(", ", numbers));
        //     i = 0;

        //     canshoot = true;

        //     num = Random.Range(0,2);
        //     transform.position = Teleport[numbers[i]].position; //列表第一個位開始 i為0
        //     teleporttime = teleportcd;
        //     print(Teleport[numbers[i]]);
        // }

        // if(Input.GetKeyDown(KeyCode.A)) //移動
        // {
        //     movetest();
        // }
        #endregion

        if(i == 3 && teleporttime <= 0)
        {
            finished = true;
        }

        movetest();

        if(canshoot)
        {
            FireBallShoot();
        }
    }

    public void FireBallShoot()
    {

        if(shootTime > 0)
        {
            shootTime -= Time.deltaTime;
        }

        else if(shootTime <= 0)
        {
            shootTime = shootcd;
            AVmanager.instance.Play("Wizard_FireSpell_1");
            var fireball = FireBall_Pool.instance.GetFormPool(this.transform);

            switch (SD)
            {
                case ShootDir.up:
                fireball.GetComponent<Transform>().Rotate(0,0,90);
                fireball.GetComponent<Rigidbody2D>().velocity = new Vector2(0,fireballspeed);
                break;

                case ShootDir.down:
                fireball.GetComponent<Transform>().Rotate(0,0,-90);
                fireball.GetComponent<Rigidbody2D>().velocity = new Vector2(0,-fireballspeed);
                break;

                case ShootDir.left:
                fireball.GetComponent<Transform>().Rotate(0,180,0);
                fireball.GetComponent<Rigidbody2D>().velocity = new Vector2(-fireballspeed,0);
                break;

                case ShootDir.right:
                fireball.GetComponent<Transform>().Rotate(0,0,0);
                fireball.GetComponent<Rigidbody2D>().velocity = new Vector2(fireballspeed,0);
                break;
            }
        }


    }
    
    //洗牌
    public void shuffleArray<T>(T[] array)
    {
        for (int j = 0; j < array.Length; j++) 
        {
            int randomIndex = Random.Range(j, array.Length);
            T temp = array[j];
            array[j] = array[randomIndex];
            array[randomIndex] = temp;
        }    
    }

    public void movetest()
    {       
            if(Teleport[numbers[i]].position == Teleport[0].transform.position)
            {
                switch(num)
                {
                    case 0:
                    transform.position = Vector2.MoveTowards(transform.position,Teleport[1].transform.position,speed * Time.deltaTime);
                    End = Teleport[1].transform;
                    SD = ShootDir.down;
                    break;

                    case 1:
                    transform.position = Vector2.MoveTowards(transform.position,Teleport[3].transform.position,speed * Time.deltaTime);
                    End = Teleport[3].transform;
                    SD = ShootDir.right;
                    break;
                }
            }
            else if(Teleport[numbers[i]].position == Teleport[1].transform.position)
            {
                switch(num)
                {
                    case 0:
                    transform.position = Vector2.MoveTowards(transform.position,Teleport[0].transform.position,speed * Time.deltaTime);
                    End = Teleport[0].transform;
                    SD = ShootDir.down;
                    break;

                    case 1:
                    transform.position = Vector2.MoveTowards(transform.position,Teleport[2].transform.position,speed * Time.deltaTime);
                    End = Teleport[2].transform;
                    SD = ShootDir.left;
                    break;
                }
            }
            else if(Teleport[numbers[i]].position == Teleport[2].transform.position)
            {
                switch(num)
                {
                    case 0:
                    transform.position = Vector2.MoveTowards(transform.position,Teleport[1].transform.position,speed * Time.deltaTime);
                    End = Teleport[1].transform;
                    SD = ShootDir.left;
                    break;

                    case 1:
                    transform.position = Vector2.MoveTowards(transform.position,Teleport[3].transform.position,speed * Time.deltaTime);
                    End = Teleport[3].transform;
                    SD = ShootDir.up;
                    break;
                }
            }
            else if(Teleport[numbers[i]].position == Teleport[3].transform.position)
            {
                switch(num)
                {
                    case 0:
                    transform.position = Vector2.MoveTowards(transform.position,Teleport[0].transform.position,speed * Time.deltaTime);
                    End = Teleport[0].transform;
                    SD = ShootDir.right;
                    break;

                    case 1:
                    transform.position = Vector2.MoveTowards(transform.position,Teleport[2].transform.position,speed * Time.deltaTime);
                    End = Teleport[2].transform;
                    SD = ShootDir.up;
                    break;
                }
            }

            if(transform.position == End.position) //到達終點
            {
                canshoot = false; //結束射擊

                if(teleporttime > 0) //開始倒計時
                {
                    teleporttime -= Time.deltaTime;
                }
                else if(teleporttime <= 0) //時間結束開始傳送
                {   
                    if(i<3) //0,1,2,3 執行4次
                    {
                        i++; //為之完成一次移動

                        canshoot = true; //傳送後開始射擊

                        num = Random.Range(0,2); //決定方向
                        transform.position = Teleport[numbers[i]].position; //新起點
                        teleporttime = teleportcd;
                        print(Teleport[numbers[i]]); //印出位置
                    }
                }
            }

    }
}
