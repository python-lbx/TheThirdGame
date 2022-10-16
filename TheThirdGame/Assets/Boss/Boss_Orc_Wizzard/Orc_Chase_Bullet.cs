using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Chase_Bullet : MonoBehaviour
{
    [SerializeField]
    int numberOfProjectiles;

    [SerializeField]
    GameObject Bullet;
    public GameObject startPoint;
    public float radius;
    public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.G))
        {
            ChaseBullet();
        }
    }

    public void ChaseBullet()
    {
        float angleStep = 360f / numberOfProjectiles;
        float angle = 0f;

        for(int i = 0; i <= numberOfProjectiles -1 ; i++)
        {
            float projectileDirXposition = startPoint.transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180) * radius;
            float projectileDirYposition = startPoint.transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180) * radius;

            Vector2 projectileVector = new Vector2 (projectileDirXposition,projectileDirYposition);
            Vector2 projectileMoveDirection = (projectileVector - (Vector2)startPoint.transform.position).normalized * moveSpeed;

            var proj = Instantiate (Bullet,startPoint.transform.position,Quaternion.identity);
            
            proj.GetComponent<Boss_Orc_Wizzard_Bullet>().enemycontroller = GetComponent<EnemyController>();
            proj.GetComponent<Rigidbody2D>().velocity =
            new Vector2 (projectileMoveDirection.x , projectileMoveDirection.y);

            //proj.transform.position = new Vector2(projectileMoveDirection.x,projectileMoveDirection.y);
            proj.GetComponent<Boss_Orc_Wizzard_Bullet>().focustime = i+1f;
            Destroy(proj,5f);

            angle += angleStep;
        }
    }
}
