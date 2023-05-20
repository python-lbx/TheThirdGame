using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLaser : MonoBehaviour
{
    public Transform trans;
    public float speed;
    public float rotatedelay;
    public float rotatedelaytime;
    void OnEnable()
    {
        if(trans == null)
        {
            trans = GetComponent<Transform>();
        }
        else
        {
            trans.Rotate(0,0,Random.Range(-120,240));
        }
        
        rotatedelaytime = rotatedelay;
    }
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if(rotatedelaytime > 0)
        {
            rotatedelaytime -= Time.deltaTime;
        }
        else if(rotatedelaytime <= 0)
        {
            trans.Rotate(0,0,speed);
        }
    }
}
