using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleForTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //子彈與死光是不一樣的
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            print("Player");
        }

        if(other.gameObject.name == "shield")
        {
            other.gameObject.SetActive(false);
        }

        if(other.gameObject.name == "NPC")
        {
            other.GetComponent<NPC>().HP --;
        }
    }
}
