using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialChest : MonoBehaviour
{
    Animator anim;
    public Vector2 BoxSize;
    public LayerMask PlayerLayer;
    public bool Here;
    [Header("掉寶")]
    public GameObject[] Tresure;
    public int DropTime;
    public bool firsttime = true;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Here = Physics2D.OverlapBox(transform.position,BoxSize,0,PlayerLayer);

        if(Here)
        {
            if(Input.GetKeyDown(GameManager.GM.interactive) && firsttime)
            {
                anim.SetBool("Open",true);
                AVmanager.instance.Play("Chest_Open");
                firsttime = false;
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
    }
}
