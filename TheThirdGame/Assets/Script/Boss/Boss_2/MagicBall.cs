using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    Collider2D coll;
    public bool circle_here;
    public bool touchable;
    public string Key;
    public GameObject Magic_Circle;
    public Vector2 startPos;

    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        touchable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Magic_Circle != null)
        {
            if (Key == Magic_Circle.GetComponent<MagicCircle>().Lock) //對應顏色
            {
                transform.position = Magic_Circle.transform.position; //安放位置
                Magic_Circle.GetComponent<MagicCircle>().Shutdowned = true;
                touchable = false; //鎖定無法取出
            }
        }
    }

    public void resetPos()
    {
        transform.localPosition = startPos;
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.CompareTag("Magic_Circle"))
        {
            circle_here = true;
            Magic_Circle = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.CompareTag("Magic_Circle"))
        {
            circle_here = false;
            Magic_Circle = null;
        }
    }
  
}
