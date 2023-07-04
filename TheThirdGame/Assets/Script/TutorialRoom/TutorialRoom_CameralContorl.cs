using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialRoom_CameralContorl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {   
            FindObjectOfType<CameraController>().ChangeTarget(transform);
        }
    }
}

