using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour
{
    Animator anim;
    public Vector2 BoxSize;
    public LayerMask PlayerLayer;
    public bool Here;
    public GameObject FirstRoom;
    [Header("掉寶")]
    public GameObject[] Tresure;
    public GameObject[] cherrypoint;
    public int DropTime;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        FirstRoom = GameObject.Find("RoomDirecter");
    }

    // Update is called once per frame
    void Update()
    {
        Here = Physics2D.OverlapBox(transform.position,BoxSize,0,PlayerLayer);

        if(Here)
        {
            if(Input.GetKeyDown(GameManager.GM.interactive))
            {
                anim.SetBool("Open",true);
                AVmanager.instance.Play("Chest_Open");
                FirstRoom.GetComponent<RoomDirecter>().rooms[0].PortalActive = false;
            }
        }

    }
    
    private void OnDrawGizmos() 
    {
        Gizmos.color = Physics2D.OverlapBox(transform.position,BoxSize,0,PlayerLayer)? Color.green:Color.clear;
        Gizmos.DrawCube(transform.position,BoxSize);
    }

    public void DropTresure()
    {
        for(var j = 0 ; j < DropTime ; j++)
        {
            int num = Random.Range(0,5);
            print(num);
            for(var i = 0 ; i < Tresure.Length ; )
            {
                Instantiate(Tresure[num],transform.position,Quaternion.identity);
                break;  
            }
        }

        for(var i = 0 ; i < cherrypoint.Length ; i++)
        {
            Cherry_Pool.instance.GetFormPool(cherrypoint[i].transform);
        }
    }
}
