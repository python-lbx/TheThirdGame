using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public RoomDirecter roomDirecter;
    private void Awake() 
    {
        roomDirecter = FindObjectOfType<RoomDirecter>();
        roomDirecter.Wall.Add(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
