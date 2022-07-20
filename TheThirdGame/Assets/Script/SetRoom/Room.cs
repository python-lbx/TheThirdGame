using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public GameObject LeftDoor,RightDoor,UpDoor,DownDoor;
    public GameObject LeftWall,RightWall,UpWall,DownWall;

    public bool leftdoor,rightdoor,updoor,downdoor;
    public int stepToStart;
    public int doorNumber;
    public int RoomID;
    public Text text;
    public GameObject MapGroup;


    private void Awake() 
    {
        MapGroup = GameObject.Find("MapGroup");
        this.gameObject.transform.SetParent(MapGroup.transform);
    }
    // Start is called before the first frame update
    void Start()
    {

        LeftDoor.SetActive(leftdoor);
        RightDoor.SetActive(rightdoor);
        UpDoor.SetActive(updoor);
        DownDoor.SetActive(downdoor);

        LeftWall.SetActive(!leftdoor);
        RightWall.SetActive(!rightdoor);
        UpWall.SetActive(!updoor);
        DownWall.SetActive(!downdoor);

    }

    // Update is called once per frame
    void Update()
    {
        LeftDoor.SetActive(leftdoor);
        RightDoor.SetActive(rightdoor);
        UpDoor.SetActive(updoor);
        DownDoor.SetActive(downdoor);

        LeftWall.SetActive(!leftdoor);
        RightWall.SetActive(!rightdoor);
        UpWall.SetActive(!updoor);
        DownWall.SetActive(!downdoor);
    }

    public void UpDateRoom(float xOffset,float yOffset)
    {
        stepToStart = (int)(Mathf.Abs(transform.position.x / xOffset) + Mathf.Abs(transform.position.y / yOffset));

        text.text = stepToStart.ToString();

        if(updoor)
        doorNumber++;
        if(downdoor)
        doorNumber++;
        if(rightdoor)
        doorNumber++;
        if(leftdoor)
        doorNumber++;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<CameraController>().ChangeTarget(transform);
        }
    }
}