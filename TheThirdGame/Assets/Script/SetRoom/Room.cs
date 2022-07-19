using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Room : MonoBehaviour
{
    public GameObject LeftDoor,RightDoor,UpDoor,DownDoor;

    public bool leftdoor,rightdoor,updoor,downdoor;
    public int stepToStart;
    public Text text;

    // Start is called before the first frame update
    void Start()
    {
        LeftDoor.SetActive(leftdoor);
        RightDoor.SetActive(rightdoor);
        UpDoor.SetActive(updoor);
        DownDoor.SetActive(downdoor);
    }

    // Update is called once per frame
    void Update()
    {
        LeftDoor.SetActive(leftdoor);
        RightDoor.SetActive(rightdoor);
        UpDoor.SetActive(updoor);
        DownDoor.SetActive(downdoor);
    }

    public void UpDateRoom()
    {
        stepToStart = (int)(Mathf.Abs(transform.position.x / 18) + Mathf.Abs(transform.position.y / 9));

        text.text = stepToStart.ToString();
    }
}
