using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attributes_Tutorial : MonoBehaviour
{
    public GameObject[] UI;
    public GameObject UIBG;
    public int num;
    public bool here;
    // Update is called once per frame
    void Update()
    {
        if(here)
        {
            UIBG.SetActive(true);

            if(Input.GetKeyDown(KeyCode.Y) && num < UI.Length)
            {
                num ++;
            }

            
            if(num >= UI.Length)
            {
                num = 0;
            }

            for(int i = 0 ; i < UI.Length ; i++)
            {
                if(i == num)
                {
                    UI[i].SetActive(true);
                }
                else
                {
                    UI[i].SetActive(false);
                }
            }
        }
        else
        {
            UIBG.SetActive(false);
            
            for(int i = 0 ; i < UI.Length ; i++)
            {
                UI[i].SetActive(false);
                num = 0;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            here = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            here = false;
        }
    }
}
