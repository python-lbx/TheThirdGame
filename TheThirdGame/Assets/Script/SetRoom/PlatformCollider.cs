using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollider : MonoBehaviour
{
    public GameObject currentOneWayPlatform;

    public float time;
    [SerializeField] Collider2D playerCollider;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(GameManager.GM.down))
        {
            if(currentOneWayPlatform != null)
            {
                StartCoroutine(DisableCollision(time));
            }
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("OneWayPlatform"))
        {
             currentOneWayPlatform = other.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("OneWayPlatform"))
        {
             currentOneWayPlatform = null;
        }    
    }

    private IEnumerator DisableCollision(float time)
    {
        BoxCollider2D platformCollider = currentOneWayPlatform.GetComponent<BoxCollider2D>();

        Physics2D.IgnoreCollision(playerCollider,platformCollider);
        yield return new WaitForSeconds(time);
        Physics2D.IgnoreCollision(playerCollider,platformCollider,false);
    }
}
