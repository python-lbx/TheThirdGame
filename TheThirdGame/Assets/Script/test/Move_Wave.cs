using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move_Wave : MonoBehaviour
{
    public Transform leftpoint;
    public Transform rightpoint;
    public GameObject movewave;
    public float movewaveSpeed;

    public int waveTime; //次數
    public float WaveCD; //間隔
    public float last_wave_time;//最近一次技能施放

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
        // var left = Instantiate(movewave, leftpoint.position, Quaternion.identity);
        // left.GetComponent<Rigidbody2D>().velocity = new Vector2(-movewaveSpeed,0f);
        // Destroy(left,3f);

        // var right = Instantiate(movewave, rightpoint.position, Quaternion.identity);
        // right.GetComponent<Rigidbody2D>().velocity = new Vector2(movewaveSpeed,0f);
        // Destroy(right,3f);
        }

        if(waveTime < 3)
        {
            if(Time.time >= (WaveCD + last_wave_time))
            {
                var left = Instantiate(movewave, leftpoint.position, Quaternion.identity);
                left.GetComponent<Rigidbody2D>().velocity = new Vector2(-movewaveSpeed,0f);
                Destroy(left,3f);

                var right = Instantiate(movewave, rightpoint.position, Quaternion.identity);
                right.GetComponent<Rigidbody2D>().velocity = new Vector2(movewaveSpeed,0f);
                Destroy(right,3f);
                last_wave_time = Time.time;
                waveTime ++;
            }
        }
    }


}
