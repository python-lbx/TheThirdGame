using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    [Header("移動參數")]
    public int speed;

    [Header("角色狀態")]
    public int facedirection;
    public bool faceright;
    public bool isOnGround;

    [Header("環境檢測")]
    public Transform GroundCheckPoint;
    public LayerMask groundLayer;
    public LayerMask Player;
    public float GroundCheckDistance;
    public Vector2 BoxSize;
    public GameObject Target;

    public enum Statue{Idle,Patorl,Chase,Battle}
    public Statue statue;
    public float PhaseTime; 

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        faceright = true;
    }

    // Update is called once per frame
    void Update()
    {
        PhysicalCheck();

        //偵測
        if(Physics2D.OverlapBox(transform.position,BoxSize,0,Player) && statue != Statue.Battle)
        {   
            Target = Physics2D.OverlapBox(transform.position,BoxSize,0,Player).gameObject;
            statue = Statue.Chase;
        }        

        //偵測 有玩家 進入攻擊範圍 攻擊 距離太遠 停止追逐進入待機 -> 巡邏 ->偵測
        switch (statue)
        {
            case Statue.Idle:
            rb.velocity = new Vector2(0,0);

            if(PhaseTime > 0)
            {
                PhaseTime -= Time.deltaTime;
            }
            else if(PhaseTime <= 0)
            {
                PhaseTime = Random.Range(2,6);
                statue = Statue.Patorl;
            }
            break;

            case Statue.Patorl:
            speed = 3;
            rb.velocity = transform.right * speed;

            if(PhaseTime > 0)
            {                
                PhaseTime -= Time.deltaTime;

                if(!isOnGround)
                {
                    transform.Rotate(0,180,0);
                    faceright = !faceright;
                }
            }
            else if(PhaseTime <= 0)
            {
                PhaseTime = 3f;
                statue = Statue.Idle;
            }
            break;

            case Statue.Chase:
            speed = 4;
            rb.velocity = transform.right * speed;

            //範圍內追逐
            if(Target.transform.position.x < transform.position.x && faceright)
            {
                faceright = false;
                transform.Rotate(0,180,0);
                print("on your left");
            }
            else if(Target.transform.position.x > transform.position.x && !faceright)
            {
                faceright = true;
                transform.Rotate(0,180,0);
                print("on your right");
            }

            //進入戰鬥範圍
            if(Mathf.Abs(transform.position.x - Target.transform.position.x) < 1.5f)
            {
                statue = Statue.Battle;
            }
            //離開偵測範圍
            if(Mathf.Abs(transform.position.x - Target.transform.position.x) > 3f)
            {
                rb.velocity = new Vector2(0,0);
                statue = Statue.Patorl;
            }
            break;

            case Statue.Battle:
            rb.velocity = new Vector2(0,0);

            if(Target.transform.position.x < transform.position.x && faceright)
            {
                faceright = false;
                transform.Rotate(0,180,0);
                print("on your left");
            }
            else if(Target.transform.position.x > transform.position.x && !faceright)
            {
                faceright = true;
                transform.Rotate(0,180,0);
                print("on your right");
            }
            
            if(Mathf.Abs(transform.position.x - Target.transform.position.x) > 1.5f)
            {
                statue = Statue.Patorl;
            }
            break;
        }




    }
    void PhysicalCheck()
    {
        RaycastHit2D groundRay = Raycast(GroundCheckPoint,Vector2.down,GroundCheckDistance,groundLayer);

        if(groundRay)
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }
    }

    RaycastHit2D Raycast(Transform pointpos, Vector2 raydir, float raydis,LayerMask layer)
    {
        RaycastHit2D hit = Physics2D.Raycast(pointpos.position,raydir,raydis,layer);

        Color color = hit? Color.red:Color.green;

        Debug.DrawRay(pointpos.position,raydir*raydis,color);

        return hit;
    }
    
    private void OnDrawGizmos() 
    {
        Gizmos.DrawCube(transform.position,BoxSize);
    }
}
