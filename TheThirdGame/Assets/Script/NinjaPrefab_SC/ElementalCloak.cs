using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementalCloak : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("EnemyWeapon"))
        {
            if(other.gameObject.name == "Boomerang(Clone)")
            {
                Orc_Boomerang_Pool.instance.ReturnPool(other.gameObject);
            }

            if(other.gameObject.name == "Orc_Bullet(Clone)")
            {
                Orc_Bullet_Pool.instance.ReturnPool(other.gameObject);
            }
        }
    }
}
