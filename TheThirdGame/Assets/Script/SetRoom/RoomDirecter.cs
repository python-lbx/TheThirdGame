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
        endroom.SetActive(false);

        closeroom();
        Wall[Wall.Count-1].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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


                case Direction.down:
                    directerPoint.position += new Vector3(0,-yOffset,0);
                    //print(directerPoint.position);

                    if(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer) != null)
                    {
                        //Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
                        Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().updoor = false;
                        NearEndRoom.Add(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject);
                    }
                    directerPoint.position = endroom.transform.position;
                break;


                case Direction.left:
                    directerPoint.position += new Vector3(xOffset,0,0);
                    //print(directerPoint.position);
                    if(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer) != null)
                    {
                        //Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
                        Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().leftdoor = false;
                        NearEndRoom.Add(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject);
                    }
                    directerPoint.position = endroom.transform.position;
                break;

                case Direction.right:
                    directerPoint.position += new Vector3(-xOffset,0,0);
                    //print(directerPoint.position);
                    if(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer) != null)
                    {
                        //Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
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
                        //關牆開門
                        Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().downdoor = false;
                        //Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().DownWall[0].SetActive(false);

                        switch (rooms[i].gameObject.GetComponent<RandomMapBG>().num)
                            {
                                case 0:
                                Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().DownWall[0].SetActive(false);
                                break;

                                case 1:
                                Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().DownWall[1].SetActive(false);
                                break;
                            }
                        }
                    directerPoint.position = endroom.transform.position;
                break;


                case Direction.down:
                    directerPoint.position += new Vector3(0,-yOffset,0);
                    //print(directerPoint.position);

                    if(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer) != null)
                    {
                        //Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
                        Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().updoor = false;
                        Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().UpWall[0].SetActive(false);
                    }
                    directerPoint.position = endroom.transform.position;
                break;


                case Direction.left:
                    directerPoint.position += new Vector3(xOffset,0,0);
                    //print(directerPoint.position);
                    if(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer) != null)
                    {
                        //Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
                        Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().leftdoor = false;
                        Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().LeftWall[0].SetActive(false);
                    }
                    directerPoint.position = endroom.transform.position;
                break;

                case Direction.right:
                    directerPoint.position += new Vector3(-xOffset,0,0);
                    //print(directerPoint.position);
                    if(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer) != null)
                    {
                        //Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
                        Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().rightdoor = false;
                        Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().RightWall[0].SetActive(false);
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
