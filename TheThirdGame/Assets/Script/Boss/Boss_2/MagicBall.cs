using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBall : MonoBehaviour
{
    Collider2D coll;
    //public SpriteRenderer SR;

    //public Color Tough_color;
    //public Color Not_Tough_color;
    //public Color Seted_color;
    //public Color NotSet_color;
    public bool here;
    public bool holded;
    public bool circle_here;
    public bool touchable;
    public string Key;
    //public float button_cd_time;
    //public float last_button_time;
    public GameObject player;
    public GameObject Magic_Circle;

    public bool Get_Press;
    // Start is called before the first frame update
    void Start()
    {
        coll = GetComponent<Collider2D>();
        //SR = GetComponent<SpriteRenderer>();
        touchable = true;
        //last_button_time = -button_cd_time;
    }

    // Update is called once per frame
    void Update()
    {
        //if(Time.time >= last_button_time + button_cd_time)
        //{
        //    if (Input.GetKeyDown(KeyCode.G))
        //    {
        //        last_button_time = Time.time;
        //    }
        //}

        if (here)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                //transform.position = player.transform.GetChild(0).GetComponent<Transform>().position;
                holded = !holded;
            }
        }

        if (holded && touchable)
        {
            transform.position = player.transform.Find("FloatDamagePoint").transform.position;
        }
        
        if(!holded && circle_here)
        {
            if (Magic_Circle != null)
            {
                if (Key == Magic_Circle.GetComponent<MagicCircle>().Lock)
                {
                    transform.position = Magic_Circle.transform.Find("Point").transform.position;
                    this.gameObject.transform.SetParent(Magic_Circle.transform);
                    touchable = false;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            here = true;
            //SR.color = Tough_color;
            player = other.gameObject;
        }

        if(other.CompareTag("Magic_Circle"))
        {
            circle_here = true;
            Magic_Circle = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            here = false;
            //SR.color = Not_Tough_color;
            player = null;
        }

        if (other.CompareTag("Magic_Circle"))
        {
            circle_here = false;
            Magic_Circle = null;
        }
    }
  
}
