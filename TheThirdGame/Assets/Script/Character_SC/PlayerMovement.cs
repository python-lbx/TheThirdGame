using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed;
    private float horizontal;
    private float vertical;
    Rigidbody2D rb;
    public Player_Attributes CharacterState;
    
    // Start is called before the first frame update
    private void Awake() 
    {

    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if(CharacterState != null)
        {
            speed = CharacterState.Player_SPD;
        }
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
    }
}
