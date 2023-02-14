using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLevelManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Boss;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Player"))
        {
            if(Boss != null)
            {
                FindObjectOfType<CameraController>().ChangeTarget(transform);
                Boss.SetActive(true);
            }
        }
    }
}
