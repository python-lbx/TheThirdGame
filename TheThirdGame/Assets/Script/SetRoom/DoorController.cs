using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public int RoomID;

    private void OnEnable() 
    {
        RoomID = gameObject.GetComponentInParent<Room>().RoomID;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(RoomID == 0)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                this.gameObject.SetActive(false);
            }
        }
    }
}
