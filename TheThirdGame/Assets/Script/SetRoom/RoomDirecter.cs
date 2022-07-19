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

    [Header("位置控制")]
    public Transform directerPoint;
    public float xOffset;
    public float yOffset;
    public LayerMask roomLayer;

    public List<Room> rooms = new List<Room>();

    // Start is called before the first frame update
    void Start()
    {
        for(int i =0 ; i < roomNumber ; i++)
        {
            rooms.Add(Instantiate(roomPrefab,directerPoint.position,Quaternion.identity).GetComponent<Room>());

            ChangePos();
        }

        rooms[0].GetComponent<SpriteRenderer>().color = startColor;
        
        //endroom = rooms[0].gameObject;

        foreach(var room in rooms)
        {
            /*if(room.transform.position.sqrMagnitude > endroom.transform.position.sqrMagnitude)
            {
                endroom = room.gameObject;
            }*/

            SetUpRoom(room,room.transform.position); //更改房間門位置
        }

        endroom = rooms[roomNumber -1 ].gameObject;
        directerPoint.position = endroom.transform.position;


        
        endroom.GetComponent<SpriteRenderer>().color = endColor;
        endroom.SetActive(false);

        closeroom();

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }   

        if(Input.GetKeyDown(KeyCode.E))
        {
            endroom.SetActive(true);
            openroom();
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
                    print(directerPoint.position);

                    if(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer) != null)
                    {
                        Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
                        Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().downdoor = false;
                    }
                    directerPoint.position = endroom.transform.position;
                break;


                case Direction.down:
                    directerPoint.position += new Vector3(0,-yOffset,0);
                    print(directerPoint.position);

                    if(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer) != null)
                    {
                        Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
                        Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().updoor = false;
                    }
                    directerPoint.position = endroom.transform.position;
                break;


                case Direction.left:
                    directerPoint.position += new Vector3(xOffset,0,0);
                    print(directerPoint.position);
                    if(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer) != null)
                    {
                        Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
                        Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().leftdoor = false;
                    }
                    directerPoint.position = endroom.transform.position;
                break;

                case Direction.right:
                    directerPoint.position += new Vector3(-xOffset,0,0);
                    print(directerPoint.position);
                    if(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer) != null)
                    {
                        Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
                        Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().rightdoor = false;
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
                    print(directerPoint.position);

                    if(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer) != null)
                    {
                        Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
                        Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().downdoor = true;
                    }
                    directerPoint.position = endroom.transform.position;
                break;


                case Direction.down:
                    directerPoint.position += new Vector3(0,-yOffset,0);
                    print(directerPoint.position);

                    if(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer) != null)
                    {
                        Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
                        Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().updoor = true;
                    }
                    directerPoint.position = endroom.transform.position;
                break;


                case Direction.left:
                    directerPoint.position += new Vector3(xOffset,0,0);
                    print(directerPoint.position);
                    if(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer) != null)
                    {
                        Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
                        Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().leftdoor = true;
                    }
                    directerPoint.position = endroom.transform.position;
                break;

                case Direction.right:
                    directerPoint.position += new Vector3(-xOffset,0,0);
                    print(directerPoint.position);
                    if(Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer) != null)
                    {
                        Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<SpriteRenderer>().color = Color.grey;
                        Physics2D.OverlapCircle(directerPoint.position,0.2f,roomLayer).gameObject.GetComponent<Room>().rightdoor = true;
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

        newRoom.UpDateRoom(); //顯示步數
    }
}
