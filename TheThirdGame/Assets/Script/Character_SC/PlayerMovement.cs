using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public int speed;
    private float horizontal;
    private float vertical;
    Rigidbody2D rb;
    CharacterStats characterStats;
    // Start is called before the first frame update
    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        characterStats = GetComponent<CharacterStats>();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if(characterStats != null)
        {
        speed = characterStats.Character.Speed;
        }
        rb.velocity = new Vector2(horizontal * speed, vertical * speed);
    }
}
