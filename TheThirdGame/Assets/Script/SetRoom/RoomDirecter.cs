using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomDirecter : MonoBehaviour
{
    public enum Direction { up , down , left , right};
    public Direction direction;

    [Header("房間信息")]
    public GameObject roomPrefab;
    public int roomNumber;
    public Color startColor,endColor;
    public GameObject endroom;
    public bool endroomCanOpen;
    public int roomClear;

    [Header("位置控制")]
    public Transform directerPoint;
    public float xOffset;
    public float yOffset;
    public LayerMask roomLayer;

    public  List<Room> rooms = new List<Room>();
    public List<GameObject> NearEndRoom = new List<GameObject>(); //達成條件才能開通門
    public List<GameObject> Wall = new List<GameObject>();
    //public List<RandomMapBG> Rooms = new List<RandomMapBG>();
    public WallType walltype;
    public GameObject MapGroup;
    public GameObject WallGroup;

    // Start is called before the first frame update
    void Start()
    {
        for(int i =0 ; i < roomNumber ; i++) //起地圖
        {
            
            rooms.Add(Instantiate(roomPrefab,directerPoint.position,Quaternion.identity).GetComponent<Room>());
            
            ChangePos();
        }

        foreach(var room in rooms)
        {
            //找最遠距離房間
            /*if(room.transform.position.sqrMagnitude > endroom.transform.position.sqrMagnitude)
            {
                endroom = room.gameObject;
            }*/

            SetUpRoom(room,room.transform.position); //更改房間門位置
        }

        //房間LIST
        for(int i = 0 ; i < rooms.Count ; i++) 
        {   
            //入ID
            rooms[i].GetComponent<Room>().RoomID = i;
            Wall[i].GetComponent<Wall>().whichroom = rooms[i].GetComponent<Room>();
            rooms[i].GetComponent<Room>().whichWall = Wall[i].GetComponent<Wall>();

            print(rooms[i].gameObject.GetComponent<RandomMapBG>().num);

            //改背景
            switch (rooms[i].gameObject.GetComponent<RandomMapBG>().num)
            {
                case 0:
                Wall[i].gameObject.GetComponent<Wall>().WallStyle[0].SetActive(true);
                break;

                case 1:
                Wall[i].gameObject.GetComponent<Wall>().WallStyle[1].SetActive(true);
                break;
            }
        }

        rooms[0].GetComponent<SpriteRenderer>().color = startColor;

        endroom = rooms[roomNumber -1 ].gameObject;
        directerPoint.position = endroom.transform.position;

        endroom.GetComponent<SpriteRenderer>().color = endColor;

        #region  隱藏房間與牆體

        endroom.SetActive(false);
        closeroom();

        Wall[Wall.Count-1].GetComponent<Wall>().isEndRoom = true; //最終房間判定
        Wall[Wall.Count-1].SetActive(false);

        if(Wall[Wall.Count-1].GetComponent<Wall>().Ladder != null)
        {
            Wall[Wall.Count-1].GetComponent<Wall>().Ladder.SetActive(false);
        }

        //int wallstylelength = Wall[Wall.Count -1].GetComponent<Wall>().WallStyle.Length;
        //int wallchildcount = Wall[Wall.Count -1].GetComponent<Wall>().WallStyle[wallstylelength].gameObject.transform.childCount;
        //print(Wall[Wall.Count -1].GetComponent<Wall>().WallStyle[0].gameObject.transform.childCount);
        //Wall[Wall.Count-1].GetComponent<Wall>().WallStyle[0].gameObject.transform.GetChild(0).gameObject.SetActive(false);

        //參考上面
        for(var x = 0 ; x < Wall[Wall.Count -1].GetComponent<Wall>().WallStyle.Length ; x++)
        {
            for(var y = 0 ; y < Wall[Wall.Count -1].GetComponent<Wall>().WallStyle[x].gameObject.transform.childCount ; y++)
            {
                 Wall[Wall.Count-1].GetComponent<Wall>().WallStyle[x].gameObject.transform.GetChild(y).gameObject.SetActive(false);
            }
        }
        #endregion

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }   

        //全清則最終房間打開
        for(int i = 0; i < rooms.Count -1 ;i++)
        {
            if(!rooms[i].GetComponent<Room>().isClear)
            {
                endroomCanOpen = false;
                break;
            }
            else
            {
                endroomCanOpen = true;
            }
        }

        
        if(endroomCanOpen || Input.GetKeyDown(KeyCode.E))
        {
            endroom.SetActive(true);
            openroom();
            Wall[Wall.Count-1].SetActive(true);

        /*for(int i = 0 ; i < NearEndRoom.Count ; i++)
        {
            print(NearEndRoom[i].GetComponent<Room>().RoomID);
            for(int x = 0 ; x<Wall.Count ; x++)
            {
                Wall[NearEndRoom[i].GetComponent<Room>().RoomID].SetActive(true);
            }
        }*/
        }
    }

    public void ChangePos()
    {
        do
        {
            direction = (Direction)Random.Range(0,4); //轉型 用數字表示方向

            switch(direction)
            {
                case Direction.up:
                    directerPoint.position += new Vector3(0,yOffset,0);
                break;

                case Direction.down:
                    directerPoint.position += new Vector3(0,-yOffset,0);
                break;       

                case Direction.left:
                    directerPoint.position += new Vector3(xOffset,0,0);
                break;   

                case Direction.right:
                    directerPoint.position += new Vector3(-xOffset,0,0);
                break;
            }
        }
        while(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer));
    }

    public void closeroom()
    {
        for(var i = 0 ;i<4;i++)
        {
            direction = (Direction)i;
            switch(direction)
            {
                case Direction.up:
                    directerPoint.position += new Vector3(0,yOffset,0);
                    //print(directerPoint.position);

                    if(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer) != null)
                    {
                        //Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
                        Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().downdoor = false;
                        NearEndRoom.Add(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject);
                    }
                    directerPoint.position = endroom.transform.position;
                break;


                //隱藏關卡下面房間的梯子關閉
                case Direction.down:
                    directerPoint.position += new Vector3(0,-yOffset,0);

                    if(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer) != null)
                    {
                        Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().updoor = false;

                        if(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().whichWall.Ladder != null)
                        {
                            Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().whichWall.Ladder.SetActive(false);
                        }

                        NearEndRoom.Add(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject);
                    }
                    directerPoint.position = endroom.transform.position;
                break;


                case Direction.left:
                    directerPoint.position += new Vector3(xOffset,0,0);

                    if(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer) != null)
                    {

                        Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().leftdoor = false;
                        NearEndRoom.Add(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject);
                    }
                    directerPoint.position = endroom.transform.position;
                break;

                case Direction.right:
                    directerPoint.position += new Vector3(-xOffset,0,0);

                    if(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer) != null)
                    {

                        Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().rightdoor = false;
                        NearEndRoom.Add(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject);
                    }
                    directerPoint.position = endroom.transform.position;
                break;
            }

        }
    }

    public void openroom()
    {
        for(var i = 0 ;i<4;i++)
        {
            direction = (Direction)i;
            switch(direction)
            {
                case Direction.up:
                    directerPoint.position += new Vector3(0,yOffset,0);
                    //print(directerPoint.position);

                    if(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer) != null)
                    {
                        //Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
                        
                        //Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().downdoor = false; //開門
                        
                        //所有牆體關閉
                        //Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().DownWall[0].SetActive(false);
                        for(int j = 0 ; j < 2 ; j++)
                        {
                            Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().DownWall[j].SetActive(false);
                        }

                        //選擇 無必要
                        /*switch (rooms[i].gameObject.GetComponent<RandomMapBG>().num) //邊個隨機牆要關閉;
                            {
                                case 0:
                                Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().DownWall[0].SetActive(false);
                                break;

                                case 1:
                                Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().DownWall[1].SetActive(false);
                                break;
                            }*/
                    }

                    directerPoint.position = endroom.transform.position;
                break;

                //隱藏關卡下面房間的梯子開啟
                case Direction.down:
                    directerPoint.position += new Vector3(0,-yOffset,0);

                    if(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer) != null)
                    {
                        //Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().updoor = false;

                        if(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().whichWall.Ladder != null)
                        {
                            Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().whichWall.Ladder.SetActive(true);
                        }


                        for(int j = 0 ; j < 2 ; j++)
                        {
                            Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().UpWall[j].SetActive(false);
                        }
                    }

                    directerPoint.position = endroom.transform.position;
                break;


                case Direction.left:
                    directerPoint.position += new Vector3(xOffset,0,0);

                    if(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer) != null)
                    {

                        //Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().leftdoor = false;

                        for(int j = 0 ; j < 2 ; j++)
                        {
                            Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().LeftWall[j].SetActive(false);
                        }
        
                    }
                    directerPoint.position = endroom.transform.position;
                break;

                case Direction.right:
                    directerPoint.position += new Vector3(-xOffset,0,0);

                    if(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer) != null)
                    {

                        //Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().rightdoor = false;


                        for(int j = 0 ; j < 2 ; j++)
                        {
                            Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().RightWall[j].SetActive(false);
                        }

                    }
                    directerPoint.position = endroom.transform.position;
                break;
            }

        }
    }    
    

    public void SetUpRoom(Room newRoom , Vector3 roomPos)
    {
        newRoom.updoor = Physics2D.OverlapCircle(roomPos + new Vector3(0,yOffset,0),0.2f,roomLayer);
        newRoom.downdoor = Physics2D.OverlapCircle(roomPos + new Vector3(0,-yOffset,0),0.2f,roomLayer);
        newRoom.leftdoor = Physics2D.OverlapCircle(roomPos + new Vector3(-xOffset,0,0),0.2f,roomLayer);
        newRoom.rightdoor = Physics2D.OverlapCircle(roomPos + new Vector3(xOffset,0,0),0.2f,roomLayer);

        newRoom.UpDateRoom(xOffset,yOffset); //顯示步數

        switch(newRoom.doorNumber)
        {
            case 1:
                if(newRoom.rightdoor) { Instantiate(walltype.R,roomPos,Quaternion.identity).gameObject.transform.SetParent(WallGroup.transform); }
                if(newRoom.leftdoor) { Instantiate(walltype.L,roomPos,Quaternion.identity).gameObject.transform.SetParent(WallGroup.transform); }
                if(newRoom.updoor) { Instantiate(walltype.U,roomPos,Quaternion.identity).gameObject.transform.SetParent(WallGroup.transform); }
                if(newRoom.downdoor) { Instantiate(walltype.D,roomPos,Quaternion.identity).gameObject.transform.SetParent(WallGroup.transform); }
            break;

            case 2:
                if(newRoom.leftdoor && newRoom.rightdoor) { Instantiate(walltype.LR,roomPos,Quaternion.identity).gameObject.transform.SetParent(WallGroup.transform); }
                if(newRoom.leftdoor && newRoom.updoor) { Instantiate(walltype.LU,roomPos,Quaternion.identity).gameObject.transform.SetParent(WallGroup.transform); }
                if(newRoom.leftdoor && newRoom.downdoor) { Instantiate(walltype.LD,roomPos,Quaternion.identity).gameObject.transform.SetParent(WallGroup.transform); }
                
                if(newRoom.rightdoor && newRoom.updoor) { Instantiate(walltype.RU,roomPos,Quaternion.identity).gameObject.transform.SetParent(WallGroup.transform); }
                if(newRoom.rightdoor && newRoom.downdoor){ Instantiate(walltype.RD,roomPos,Quaternion.identity).gameObject.transform.SetParent(WallGroup.transform); }

                
                if(newRoom.updoor && newRoom.downdoor) { Instantiate(walltype.UD,roomPos,Quaternion.identity).gameObject.transform.SetParent(WallGroup.transform); }
            break;

            case 3:
                if(newRoom.leftdoor && newRoom.rightdoor && newRoom.updoor) { Instantiate(walltype.LRU,roomPos,Quaternion.identity).gameObject.transform.SetParent(WallGroup.transform); }
                if(newRoom.leftdoor && newRoom.rightdoor && newRoom.downdoor) { Instantiate(walltype.LRD,roomPos,Quaternion.identity).gameObject.transform.SetParent(WallGroup.transform); }
                if(newRoom.leftdoor && newRoom.updoor && newRoom.downdoor) { Instantiate(walltype.LUD,roomPos,Quaternion.identity).gameObject.transform.SetParent(WallGroup.transform); }

                if(newRoom.rightdoor && newRoom.updoor && newRoom.downdoor) { Instantiate(walltype.RUD,roomPos,Quaternion.identity).gameObject.transform.SetParent(WallGroup.transform); }
            break;

            case 4:
                if(newRoom.leftdoor && newRoom.rightdoor && newRoom.updoor && newRoom.downdoor) { Instantiate(walltype.LRUD,roomPos,Quaternion.identity).gameObject.transform.SetParent(WallGroup.transform); }
            break;
        }
    }

    [System.Serializable]
    public class WallType
    {
        public GameObject L,R,U,D,
                          LR,LU,LD,RU,RD,UD,
                          LRU,LRD,LUD,RUD,
                          LRUD;
    }
}
