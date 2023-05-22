using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMagicBall : MonoBehaviour
{
    public Transform Point;
    public GameObject currentBall;
    public GameObject tempBall;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.name == "MagicBall")
        {
            if(other.GetComponent<MagicBall>().touchable)
                {
                if(currentBall == null)
                {
                    currentBall = other.gameObject;
                    currentBall.transform.position = Point.transform.position;
                }
                else
                {
                    tempBall = currentBall;
                    tempBall.GetComponent<MagicBall>().resetPos();

                    tempBall = null;
                    
                    currentBall = other.gameObject;
                    currentBall.transform.position = Point.transform.position;
                }
            }
        }
    }
}
