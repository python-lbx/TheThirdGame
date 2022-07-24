using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public RoomDirecter roomDirecter;
    public GameObject[] WallStyle;
    public GameObject[] EnemyPoint;
    public GameObject Enemy;
    public GameObject Boss;
    public bool isEndRoom;
    public Room whichroom;
    public GameObject Ladder;


    private void Awake() 
    {
        roomDirecter = FindObjectOfType<RoomDirecter>();
        roomDirecter.Wall.Add(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Ladder.SetActive(showLadder);
    }

    //怪人生成
    public void CreatEnemy()
    {
        if(isEndRoom)
        {
                            //未有BOSS

            if(Enemy != null)
            {   
                whichroom.Enemys.Add(Instantiate(Enemy,transform.position,Quaternion.identity));
            }
        }
        else
        {
            if(EnemyPoint != null)
            {
                for(int i = 0 ; i<EnemyPoint.Length;i++)
                {
                    whichroom.Enemys.Add(Instantiate(Enemy,EnemyPoint[i].transform.position,Quaternion.identity));
                }
            }
        }
    }
}
