using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boomerang : MonoBehaviour
{
    Rigidbody2D rb;
    public int speed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        Destroy(this.gameObject,2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
