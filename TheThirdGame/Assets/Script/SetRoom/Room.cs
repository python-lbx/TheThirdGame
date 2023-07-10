using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    [Header("門")]
    public GameObject LeftDoor;
    public GameObject RightDoor;
    public GameObject UpDoor;
    public GameObject DownDoor;
    [Header("牆")]
    public RandomMapBG RMBG;
    public GameObject[] LeftWall;
    public GameObject[] RightWall;
    public GameObject[] UpWall;
    public GameObject[] DownWall;

    [Header("傳送門")]
    public GameObject Portal_V;
    public GameObject Portal_X;
    public GameObject Portal_Final;
    public bool PortalActive;

    [Header("門跟牆判定")]
    public bool leftdoor;
    public bool rightdoor;
    public bool updoor;
    public bool downdoor;
    public int stepToStart;
    public int doorNumber;
    public int RoomID;
    public Text text;

    [Header("初次戰鬥判定")]
    public bool IsNewRoom;
    public RoomDirecter roomDirecter;
    [Header("地圖集合")]
    public GameObject MapGroup;
    [Header("小怪集合")]
    public Wall whichWall;
    //public GameObject Enemy;
    //public int enemynum;
    public bool isClear;
    public float totalHP;
    public Color Color;
    public List<GameObject> Enemys = new List<GameObject>();
    [Header("寶箱")]
    public GameObject Treasure;
    [Header("路標")]
    public GameObject StartSign;
    public GameObject EndSign;

    private void Awake() 
    {
        MapGroup = GameObject.Find("MapGroup");
        this.gameObject.transform.SetParent(MapGroup.transform);
        roomDirecter = FindObjectOfType<RoomDirecter>();
    }
    // Start is called before the first frame update
    void Start()
    {

        //LeftDoor.SetActive(leftdoor);
        //RightDoor.SetActive(rightdoor);
        //UpDoor.SetActive(updoor);
        //DownDoor.SetActive(downdoor);

        switch (RMBG.num)
        {
            case 0:
            LeftWall[0].SetActive(!leftdoor);
            RightWall[0].SetActive(!rightdoor);
            UpWall[0].SetActive(!updoor);
            DownWall[0].SetActive(!downdoor);
            break;

            case 1:
            LeftWall[1].SetActive(!leftdoor);
            RightWall[1].SetActive(!rightdoor);
            UpWall[1].SetActive(!updoor);
            DownWall[1].SetActive(!downdoor);
            break;
        }


        //初始房間
        if(RoomID == 0)
        {
            PortalActive = true;
            IsNewRoom = false;
            LeftDoor.SetActive(leftdoor);
            RightDoor.SetActive(rightdoor);
            UpDoor.SetActive(updoor);
            DownDoor.SetActive(downdoor);

            Treasure.SetActive(true);
            StartSign.SetActive(true);
        }
        
        //print(RoomID);
    }

    // Update is called once per frame
    void Update()
    {
        //LeftDoor.SetActive(leftdoor);
        //RightDoor.SetActive(rightdoor);
        //UpDoor.SetActive(updoor);
        //DownDoor.SetActive(downdoor);

       // LeftWall.SetActive(!leftdoor);
        //RightWall.SetActive(!rightdoor);
        //UpWall.SetActive(!updoor);
        //DownWall.SetActive(!downdoor);
        
        if(!IsNewRoom && totalHP <= 0 && !PortalActive)
        {
            isClear = true;
            if(updoor)
            UpDoor.SetActive(false);
            if(downdoor)
            DownDoor.SetActive(false);
            if(leftdoor)
            LeftDoor.SetActive(false);
            if(rightdoor)
            RightDoor.SetActive(false);
        }
    }

    public void UpDateRoom(float xOffset,float yOffset)
    {
        stepToStart = (int)(Mathf.Abs(transform.position.x / xOffset) + Mathf.Abs(transform.position.y / yOffset));

        //text.text = stepToStart.ToString();

        if(updoor)
        doorNumber++;
        if(downdoor)
        doorNumber++;
        if(rightdoor)
        doorNumber++;
        if(leftdoor)
        doorNumber++;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {   
        //鏡頭跟隨
        if(other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<CameraController>().ChangeTarget(transform);

            //在地圖顯示牆邊
            whichWall.GetComponent<Wall>().MapWall.SetActive(true);
            
            //非初始非最終房間第一次進入會染上顏色
            if(RoomID != 0 && RoomID != 11)
            {
                this.gameObject.GetComponent<SpriteRenderer>().color = Color;
            }
            //FindObjectOfType<CameraController>().Immediate = false;
        }

        //戰鬥判定
        if(other.gameObject.CompareTag("Player") && IsNewRoom && RoomID != 0) //首次進入房間會進入戰鬥
        {
            roomDirecter.RoomLevel +=1;

            if(roomDirecter.RoomLevel == 5 || roomDirecter.RoomLevel == 10 || roomDirecter.RoomLevel == 11)
            {
                PortalActive = true;
            }
            
            IsNewRoom = false; //非首次進入房間


            //關門
            LeftDoor.SetActive(leftdoor);
            RightDoor.SetActive(rightdoor);
            UpDoor.SetActive(updoor);
            DownDoor.SetActive(downdoor);

            //生成敵人
            //enemynum = Random.Range(0,6);

            /*for(int i = 0 ; i <= enemynum ;i++)
            {
                Enemys.Add( Instantiate(Enemy,transform.position,Quaternion.identity) );
            }*/
            whichWall.CreatEnemy();

            if(Enemys != null)
            {
                foreach(var enemy in Enemys)
                {
                    //totalHP += enemy.GetComponent<EnemyController>().health; //總計血
                    enemy.GetComponent<EnemyController>().whichroom = this; //這間房
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        //第5關
        if(other.gameObject.CompareTag("Player") && roomDirecter.RoomLevel == 5 && PortalActive)
        {   
            if(totalHP <= 0)
            {
                Portal_V.SetActive(true);
            }
        }
        
        //第10關
        if(other.gameObject.CompareTag("Player") && roomDirecter.RoomLevel == 10 && PortalActive)
        {   
            if(totalHP <= 0)
            {
                Portal_X.SetActive(true);
            }
        }
        
        if(other.gameObject.CompareTag("Player") && roomDirecter.RoomLevel == 11 && PortalActive)
        {
            if(totalHP <= 0)
            {
                Portal_Final.SetActive(true);
            }
        }
    }

    public void dead(float health)
    {
        print("dead");
        totalHP -= health;
    }
}
