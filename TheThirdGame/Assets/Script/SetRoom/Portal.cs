using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public bool IsPlayer;
    public GameObject Player;
    public GameObject Boss_Orc_Wizzard_Portal;
    // Start is called before the first frame update
    private void OnEnable() 
    {
        Boss_Orc_Wizzard_Portal = GameObject.Find("BackToLevel");
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPlayer)
        {
            if(Input.GetKeyDown(KeyCode.Y))
            {
                Player.transform.position = Boss_Orc_Wizzard_Portal.transform.position;
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
