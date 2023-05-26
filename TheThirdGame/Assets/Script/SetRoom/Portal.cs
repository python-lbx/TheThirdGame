using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public bool IsPlayer;
    public GameObject Player;
    public GameObject Boss_One_Portal;
    public GameObject Boss_Two_Portal;
    public Vector3 RoomPos;
    public int RoomLevel;
    public Room room;
    // Start is called before the first frame update
    private void OnEnable() 
    {
        room = GetComponentInParent<Room>();
        Boss_One_Portal = GameObject.Find("BossOnePos");
        Boss_Two_Portal = GameObject.Find("BossTwoPos");
    }

    private void Start() 
    {
        RoomPos = transform.position;
        RoomLevel = FindObjectOfType<RoomDirecter>().RoomLevel;
    }
    // Update is called once per frame
    void Update()
    {
        if(RoomLevel == 5)
        {
            //有待加強
            if(Boss_One_Portal.GetComponentInChildren<BackToLevel>().WhichBoss.GetComponent<EnemyController>().currenthealth <= 0)
            {
                room.PortalActive = false;
            }
        }
        else if(RoomLevel == 10)
        {
            if(Boss_Two_Portal.GetComponentInChildren<BackToLevel>().WhichBoss.GetComponent<EnemyController>().currenthealth <= 0)
            {
                room.PortalActive = false;
            }

        }




        if(IsPlayer)
        {
            if(Input.GetKeyDown(KeyCode.Y))
            {
                if(RoomLevel == 5)
                {
                    Boss_One_Portal.GetComponentInChildren<BackToLevel>().OriginalRoomPos = RoomPos;
                    //Boss_One_Portal.SetActive(false);
                    Player.transform.position = Boss_One_Portal.transform.position;
                }
                else if(RoomLevel == 10)
                {
                    Boss_Two_Portal.GetComponentInChildren<BackToLevel>().OriginalRoomPos = RoomPos;
                    //Boss_Two_Portal.SetActive(false);
                    Player.transform.position = Boss_Two_Portal.transform.position;
                }
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
