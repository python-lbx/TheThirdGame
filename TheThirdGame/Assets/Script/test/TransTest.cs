using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransTest : MonoBehaviour
{
    public Transform LDPos;
    public Transform RUPos;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector2(Random.Range(LDPos.position.x,RUPos.position.x),Random.Range(LDPos.position.y,RUPos.position.y));
    }

    // Update is called once per frame
    void Update()
    {   

    }

    private void OnTriggerStay2D(Collider2D other) 
    {
        if(other.gameObject.name == "TransPoint")
        {
            //print("A");
                    transform.position = new Vector2(Random.Range(LDPos.position.x,RUPos.position.x),Random.Range(LDPos.position.y,RUPos.position.y));

        }
    }

    public void ChangePos()
    {
        transform.position = new Vector2(Random.Range(LDPos.position.x,RUPos.position.x),Random.Range(LDPos.position.y,RUPos.position.y));
    }
}
