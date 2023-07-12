using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLaser : MonoBehaviour
{
    public Transform trans;
    public float speed;
    public float rotatedelay;
    public float rotatedelaytime;
    public Transform[] laser_Scale;
    void OnEnable()
    {
        if(trans == null)
        {
            trans = GetComponent<Transform>();
        }
        else
        {
            trans.Rotate(0,0,Random.Range(-120,240));
            AVmanager.instance.Play("Wizard_FireSpell_2");
        }

        
        rotatedelaytime = rotatedelay;
        speed = 0.3f;
    }

    private void OnDisable() 
    {
        foreach(Transform scale in laser_Scale)
        {
            scale.GetComponent<Transform>().localScale = new Vector3(3,2,1);
        }
        
        AVmanager.instance.Stop("Wizard_FireSpell_2");
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
