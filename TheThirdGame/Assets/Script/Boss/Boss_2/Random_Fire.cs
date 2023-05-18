using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Random_Fire : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;
    public GameObject FireBall;
    public float shootTime;
    public float shootcd;
    public float teleportcd;
    public float teleporttime;
    public int i;
    public int amout;
    public int num;
    public int[] numbers = { 0, 1, 2, 3 };
    public Transform[] Teleport;
    public Transform End;

    public enum Direction { up , down , left , right};
    public Direction direction;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        shuffleArray(numbers);
        Debug.Log(string.Join(", ", numbers));
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)) //傳送
        {
            num = Random.Range(0,2);
            transform.position = Teleport[numbers[i]].position; //列表第一個位開始 i為0

            print(Teleport[numbers[i]]);
            i++;

        }

        if(Input.GetKeyDown(KeyCode.S)) //洗牌
        {
        shuffleArray(numbers);
        Debug.Log(string.Join(", ", numbers));
        i = 0;
        }

        if(Input.GetKeyDown(KeyCode.A)) //移動
        {

            movetest();

        }

        //當i到達第4次時且己到終點則洗牌
        if(i == 0)
        {
            print("A");
            num = Random.Range(0,2);
            transform.position = Teleport[numbers[i]].position; //列表第一個位開始 i為0

            print(Teleport[numbers[i]]);
            i++;        
        }
        else
        {
            if(End != null)
            {
                if(i == 4 && transform.position == End.position)
                {
                    shuffleArray(numbers);
                    Debug.Log(string.Join(", ", numbers));
                    i = 0;            
                }
                else if(transform.position == End.position)
                {
                    print("B");
                    num = Random.Range(0,2);
                    transform.position = Teleport[numbers[i]].position; //列表第一個位開始 i為0

                    print(Teleport[numbers[i]]);
                    i++;
                }
            }

        }
        
        // if(End != null)
        // {
        //     if(transform.position == End.position)
        //     {
        //         print("B");
        //         num = Random.Range(0,2);
        //         transform.position = Teleport[numbers[i]].position; //列表第一個位開始 i為0

        //         print(Teleport[numbers[i]]);

        //         if(i == 4 && transform.position == End.position)
        //         {
        //             shuffleArray(numbers);
        //             Debug.Log(string.Join(", ", numbers));
        //             i = 0;            
        //         }
        //         else
        //         {
        //             i++;  
        //         }
        //     }
        // }


        movetest();
    }

    public void FireBallShoot()
    {
        rb.velocity = new Vector2(speed,0f);
        if(teleporttime > 0)
        {
            shootTime -= Time.deltaTime;
        }
        else if(shootTime <= 0)
        {
            shootTime = shootcd;
            Instantiate(FireBall,transform.position,Quaternion.identity);
        }
    }
    
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
    public void movetest2()
    {
            if(Teleport[numbers[i-1]].position == Teleport[0].transform.position)
            {
                print("0");
            }
            else if(Teleport[numbers[i-1]].position == Teleport[1].transform.position)
            {
                print("1");

            }
            else if(Teleport[numbers[i-1]].position == Teleport[2].transform.position)
            {
                print("2");

            }
            else if(Teleport[numbers[i-1]].position == Teleport[3].transform.position)
            {
                print("3");

            }

    }
    public void movetest()
    {
            if(Teleport[numbers[i-1]].position == Teleport[0].transform.position)
            {
                switch(num)
                {
                    case 0:
                    transform.position = Vector2.MoveTowards(transform.position,Teleport[1].transform.position,speed * Time.deltaTime);
                    End = Teleport[1].transform;
                    break;

                    case 1:
                    transform.position = Vector2.MoveTowards(transform.position,Teleport[3].transform.position,speed * Time.deltaTime);
                    End = Teleport[3].transform;
                    break;
                }
            }
            else if(Teleport[numbers[i-1]].position == Teleport[1].transform.position)
            {
                switch(num)
                {
                    case 0:
                    transform.position = Vector2.MoveTowards(transform.position,Teleport[0].transform.position,speed * Time.deltaTime);
                    End = Teleport[0].transform;
                    break;

                    case 1:
                    transform.position = Vector2.MoveTowards(transform.position,Teleport[2].transform.position,speed * Time.deltaTime);
                    End = Teleport[2].transform;
                    break;
                }
            }
            else if(Teleport[numbers[i-1]].position == Teleport[2].transform.position)
            {
                switch(num)
                {
                    case 0:
                    transform.position = Vector2.MoveTowards(transform.position,Teleport[1].transform.position,speed * Time.deltaTime);
                    End = Teleport[1].transform;
                    break;

                    case 1:
                    transform.position = Vector2.MoveTowards(transform.position,Teleport[3].transform.position,speed * Time.deltaTime);
                    End = Teleport[3].transform;
                    break;
                }
            }
            else if(Teleport[numbers[i-1]].position == Teleport[3].transform.position)
            {
                switch(num)
                {
                    case 0:
                    transform.position = Vector2.MoveTowards(transform.position,Teleport[0].transform.position,speed * Time.deltaTime);
                    End = Teleport[0].transform;
                    break;

                    case 1:
                    transform.position = Vector2.MoveTowards(transform.position,Teleport[2].transform.position,speed * Time.deltaTime);
                    End = Teleport[2].transform;
                    break;
                }
            }

    }
}
