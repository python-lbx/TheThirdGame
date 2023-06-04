using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTrigger : MonoBehaviour
{
    [SerializeField]
    int numberOfProjectiles;

    [SerializeField]
    public GameObject startPoint;
    // public CircleCollider2D coll;
    public Animator anim;
    public float radius;
    public float moveSpeed;
    public float delaytime;
    
    
    // Start is called before the first frame update
    void OnEnable() 
    {
        Invoke("SpawnProjectiles",delaytime);
        // coll = GetComponent<CircleCollider2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            SpawnProjectiles();
        }
    }

    public void SpawnProjectiles()
    {
        anim.SetTrigger("Bomb");

        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        for(int i = 0; i <= numberOfProjectiles -1 ; i++)
        {
            float projectileDirXposition = startPoint.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYposition = startPoint.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector2 projectileVector = new Vector2 (projectileDirXposition,projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - (Vector2)startPoint.transform.position).normalized * moveSpeed;

            var proj = ShootBullet_Pool.instance.GetFormPool(startPoint.transform);

            proj.GetComponent<Rigidbody2D>().velocity =
            new Vector2 (projectileMoveDirection.x , projectileMoveDirection.y);
            
            proj.GetComponent<Transform>().transform.right = 
            new Vector2 (projectileMoveDirection.x , projectileMoveDirection.y);


            angle += angleStep;
        }
    }

    // public void activecoll()
    // {
    //     coll.enabled = true;
    // }

    // public void unactivecoll()
    // {
    //     coll.enabled = false;
    // }
}
