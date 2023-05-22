using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    Collider2D coll;

    // public bool here;
    // public bool holded;
    public bool circle_here;
    public bool touchable;
    public string Key;
    public GameObject player;
    public GameObject Magic_Circle;
    public Vector3 startPos;

    // Start is called before the first frame update
    private void OnEnable() 
    {
        startPos = transform.position;
    }
    void Start()
    {
        coll = GetComponent<Collider2D>();
        touchable = true;
    }

    // Update is called once per frame
    void Update()
    {


        // if (here)
        // {
        //     if (Input.GetKeyDown(KeyCode.G))
        //     {
        //         //transform.position = player.transform.GetChild(0).GetComponent<Transform>().position;
        //         holded = !holded;
        //     }
        // }

        // if (holded && touchable)
        // {
        //     transform.position = player.transform.Find("FloatDamagePoint").transform.position;
        // }
        
        // if(!holded && circle_here)
        // {
        if (Magic_Circle != null)
        {
            if (Key == Magic_Circle.GetComponent<MagicCircle>().Lock) //對應顏色
            {
                transform.position = Magic_Circle.transform.position; //安放位置
                Magic_Circle.GetComponent<MagicCircle>().Ball = this.gameObject; //激活
                touchable = false; //鎖定無法取出
            }
        }
        // }
    }

    public void resetPos()
    {
        transform.position = startPos;
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        // if(other.CompareTag("Player"))
        // {
        //     // here = true;
        //     //SR.color = Tough_color;
        //     player = other.gameObject;
        // }

        if(other.CompareTag("Magic_Circle"))
        {
            circle_here = true;
            Magic_Circle = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        // if(other.CompareTag("Player"))
        // {
        //     here = false;
        //     //SR.color = Not_Tough_color;
        //     player = null;
        // }

        if (other.CompareTag("Magic_Circle"))
        {
            circle_here = false;
            Magic_Circle = null;
        }
    }
  
}
