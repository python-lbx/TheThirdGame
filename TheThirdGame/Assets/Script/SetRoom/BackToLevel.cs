using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToLevel : MonoBehaviour
{
    public bool IsPlayer;
    public GameObject Player;
    public Vector3 OriginalRoomPos;
    public GameObject WhichBoss;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(IsPlayer)
        {
            if(Input.GetKeyDown(KeyCode.Y))
            {
                Player.transform.position = OriginalRoomPos;
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
