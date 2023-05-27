using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Level_3 : MonoBehaviour
{

    public bool faceright;


    [Header("攻擊目標")]
    public GameObject Target;
    public GameObject shootPoint;
    public GameObject bullet;
    Vector2 Direction;
    Vector2 targetpos;
    public float focustime;
    public float RechargeTime;
    public float speed;

    public int shoottime;

    public float magic_appear_time;
    public float magic_appear_time_CD;

    [Header("階段")]
    public Statue statue;
    public enum Statue{Focus,Shoot};

    public GameObject[] shieldList = new GameObject[4];
    public GameObject[] magic_circle_List = new GameObject[4];
    public int i;


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
        if(Input.GetKeyDown(KeyCode.A))
        {
            for(int i = 0 ; i < shieldList.Length ; i++)
            {
                shieldList[i].SetActive(true);
            }
        }    

        if(Input.GetKeyDown(KeyCode.X))
        {
            shuffleArray(numbers);
            Debug.Log(string.Join(", ", numbers));
        }

        if(shoottime == 4)
        {
            for(int i = 0 ; i < shieldList.Length ; i++)
            {
                if(shieldList[i].activeSelf)
                {
                    shieldList[i].GetComponentInParent<NPC>().HP --;
                    shieldList[i].SetActive(false);
                }
            }

            shoottime = 5;
        }


        if(magic_appear_time > 0 & i < 4)
        {
            magic_appear_time -= Time.deltaTime;
        }
        else if(magic_appear_time <= 0)
        {
            magic_circle_List[numbers[i]].SetActive(true);
            i++;
            magic_appear_time = magic_appear_time_CD;
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
            else if(focustime <= 0)
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
