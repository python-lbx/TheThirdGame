using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal_Cross : MonoBehaviour
{
    public GameObject obj;


    // Start is called before the first frame update

    void Update()
    {
        if(obj != null)
        {
            transform.position = obj.transform.position;
        }
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
