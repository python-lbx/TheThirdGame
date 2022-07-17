using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatDamage : MonoBehaviour
{
    public GameObject floatdamage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void MakeDamage()
    {
        Instantiate(floatdamage,transform.position,Quaternion.identity);
    }
}
