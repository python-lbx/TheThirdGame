using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed;
    public Transform target;
    //public string LastLevelName;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null)
        {   
            //transform.position = Vector3.MoveTowards(transform.position,new Vector3(target.position.x ,target.position.y,transform.position.z),speed * Time.deltaTime);
            transform.position = new Vector3(target.position.x,target.position.y,transform.position.z);
        }
    }

    public void ChangeTarget(Transform newtarget)
    {
        target = newtarget;
        //LastLevelName = target.name;
    }
}
