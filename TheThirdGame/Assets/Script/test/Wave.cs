using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Wave : MonoBehaviour
{
    public GameObject Ground;
    public float damage;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Cancel",1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Cancel()
    {   
        Ground.GetComponent<Tilemap>().color = Color.clear;

        this.gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponentInChildren<PlayerController>().GetDamage(damage);
        }
    }
}
