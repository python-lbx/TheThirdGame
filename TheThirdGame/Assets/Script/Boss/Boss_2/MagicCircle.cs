using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircle : MonoBehaviour
{
    SpriteRenderer SR;
    public Color Set_Color;
    public Color Not_Set_Color;
    public string Lock;
    public float speed;
    public bool Shutdowned;
    // Start is called before the first frame update
    void Start()
    {
        SR = GetComponent<SpriteRenderer>();
        speed = 50f;
        Shutdowned = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, speed * Time.deltaTime);

        if(Shutdowned)
        {
            SR.color = Set_Color;
            speed = 0f;
        }
        else
        {
            SR.color = Not_Set_Color;
            speed = 10f;
        }
    }

}
