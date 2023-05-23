using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpMagicBall : MonoBehaviour
{
    public Transform Point;
    public GameObject currentBall;
    public GameObject tempBall;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(currentBall != null)
        {
            currentBall.transform.position = Point.transform.position;

            if(currentBall.GetComponent<MagicBall>().touchable == false)
            {
                currentBall = null;
            }
        }
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
                }
                else
                {
                    tempBall = currentBall;
                    tempBall.GetComponent<MagicBall>().resetPos();

                    tempBall = null;
                    
                    currentBall = other.gameObject;
                }
            }
        }
    }
}
