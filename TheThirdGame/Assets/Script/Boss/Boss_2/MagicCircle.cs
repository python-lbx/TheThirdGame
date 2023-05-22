using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    public GameObject Ball;
    SpriteRenderer SR;
    public Color Set_Color;
    public Color Not_Set_Color;
    public string Lock;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        speed = 50f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime);

        if(Ball != null)
        {
            SR.color = Set_Color;
            speed = 0f;
        }
        else
        {
            SR.color = Not_Set_Color;
        }
    }

}
