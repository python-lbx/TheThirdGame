using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate_Laser_Controller : MonoBehaviour
{
    public GameObject[] magic_circle;
    public GameObject magic_ball_group;
    public GameObject laser;

    public bool[] shutdown;
    void OnEnable()
    {
        shutdown = new bool [3];
        for(int i = 0 ; i < magic_circle.Length ; i++)
        {
            magic_circle[i].GetComponent<MagicCircle>().Shutdowned = false;
            magic_circle[i].SetActive(true);
        }

        magic_ball_group.SetActive(true);

        laser.SetActive(true);
    }

    private void OnDisable() 
    {
        magic_ball_group.SetActive(false);

        for(int i = 0 ; i < magic_circle.Length ; i++)
        {
            magic_circle[i].SetActive(false);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        for(int i = 0 ; i < magic_circle.Length; i++)
        {
            shutdown[i] = magic_circle[i].GetComponent<MagicCircle>().Shutdowned;
            
        }

        if(shutdown[0] && shutdown[1] && shutdown[2])
        {
            laser.SetActive(false);
        }

    }
}
