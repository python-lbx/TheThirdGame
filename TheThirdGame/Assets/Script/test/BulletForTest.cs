using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletForTest : MonoBehaviour
{
    [SerializeField]
    int numberOfProjectiles;

    [SerializeField]
    GameObject Bullet;
    public GameObject startPoint;
    public float radius;
    public float moveSpeed;
    
    // Start is called before the first frame update
    void OnEnable() 
    {
        SpawnProjectiles();
    }
    void Start()
    {
        
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
        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        for(int i = 0; i <= numberOfProjectiles -1 ; i++)
        {
            float projectileDirXposition = startPoint.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYposition = startPoint.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector2 projectileVector = new Vector2 (projectileDirXposition,projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - (Vector2)startPoint.transform.position).normalized * moveSpeed;

            var proj = Instantiate(Bullet,startPoint.transform.position,transform.rotation);

            proj.GetComponent<Rigidbody2D>().velocity =
            new Vector2 (projectileMoveDirection.x , projectileMoveDirection.y);
            
            proj.GetComponent<Transform>().transform.right = 
            new Vector2 (projectileMoveDirection.x , projectileMoveDirection.y);


            angle += angleStep;
        }
    }

}
