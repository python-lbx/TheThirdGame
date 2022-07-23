using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public RoomDirecter roomDirecter;
    public GameObject[] WallStyle;
    public GameObject[] EnemyPoint;
    public GameObject Enemy;
    public Room whichroom;

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
        
    }

    public void CreatEnemy()
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
