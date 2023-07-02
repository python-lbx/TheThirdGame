using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialCherry : MonoBehaviour
{
    public GameObject[] cherryPoint = new GameObject[3];
    public float appeartime;
    public float appeartimcd;

    // Update is called once per frame
    void Update()
    {
        if(appeartime > 0)
        {
            appeartime -= Time.deltaTime;
        }
        else if(appeartime <= 0)
        {
            for(var i = 0 ; i < cherryPoint.Length ; i++)
            {
                Cherry_Pool.instance.GetFormPool(cherryPoint[i].transform);
            }
            appeartime = appeartimcd;
        }
    }
}
