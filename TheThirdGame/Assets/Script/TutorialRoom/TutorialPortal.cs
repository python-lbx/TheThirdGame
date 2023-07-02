using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialPortal : MonoBehaviour
{
    public bool IsPlayer;
    public GameObject Player;
    public Transform BackToLevel;

    // Update is called once per frame
    void Update()
    {
        if(IsPlayer)
        {
            if(Input.GetKeyDown(KeyCode.Y))
            {
                Player.transform.position = BackToLevel.position;
            }
        }    
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            IsPlayer = true;
            Player = other.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            IsPlayer = false;
            Player = null;
        }    
    }
}
