using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal_Cross : MonoBehaviour
{
    public GameObject player;
    // Start is called before the first frame update
    private void Awake() 
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        transform.position = player.transform.position;
    }
    void Start()
    {
        var main = GetComponent<ParticleSystem>().main;
        main.stopAction = ParticleSystemStopAction.Callback;
    }

    void OnParticleSystemStopped()
    {
        Heal_Cross_Pool.instance.ReturnPool(this.gameObject);
    }
}
