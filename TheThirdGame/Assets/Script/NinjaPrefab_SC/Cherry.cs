using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cherry : MonoBehaviour
{   
    public float heal;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInChildren<PlayerController>().GetHeal(heal);
            Heal_Cross_Pool.instance.GetFormPool(other.gameObject.transform);
            Cherry_Pool.instance.ReturnPool(this.gameObject);
        }
    }
}
