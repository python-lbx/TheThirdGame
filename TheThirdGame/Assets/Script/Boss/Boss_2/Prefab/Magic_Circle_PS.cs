using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magic_Circle_PS : MonoBehaviour
{
    public GameObject laser;
    // Start is called before the first frame update
    void Start()
    {
        var main = GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnParticleSystemStopped()
    {
        Debug.Log("System has stopped!");
        laser.SetActive(true);
    }
}
