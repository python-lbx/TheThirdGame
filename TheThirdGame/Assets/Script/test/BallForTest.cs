using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallForTest : MonoBehaviour
{
    public GameObject boss;
    public float speed;
    public Vector3 startPos;

    private void Start() 
    {
        startPos = transform.localPosition;
        this.gameObject.SetActive(false);
    }
    
    private void Update() 
    {
        transform.position = Vector2.MoveTowards(transform.position,boss.transform.position,speed * Time.deltaTime);
    }

    public void ResPos()
    {
        transform.localPosition = startPos;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.name == "Boss")
        {
            print("Boss");
            other.GetComponent<Boss_Level_3>().ballamount ++;
            ResPos();
            this.gameObject.SetActive(false);
        }
    }
}
