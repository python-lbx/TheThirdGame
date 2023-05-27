using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orc_Blade : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    [Header("移動參數")]
    public int speed;

    [Header("角色狀態")]
    public int facedirection;
    public bool faceright;
    public bool isOnGround;
    public bool isOnPlayer;
    public bool isOnWall;

    [Header("環境檢測")]
    public Transform GroundCheckPoint;
    public Transform PlayerCheckPoint;
    public LayerMask groundLayer;
    public LayerMask playerLayer;
    public float GroundCheckDistance;
    public float PlayerCheckDistance;
    public Vector2 BoxSize;
    public GameObject Target;

    [Header("階段")]
    public Statue statue;
    public enum Statue{Idle,Patorl,Battle}
    public float PhaseTime; 
    public GameObject Attack_Box;
    public float Last_AttackTime;
    public float AttackTime_CD;

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
        if(anim.GetNextAnimatorStateInfo(0).IsName("Attack"))
        {
            rb.velocity = new Vector2(0,0);
            return;
        }
        anim.SetFloat("Speed",Mathf.Abs(rb.velocity.x));

        //前後偵測是否有玩家
        if(Physics2D.OverlapBox(transform.position,BoxSize,0,playerLayer))
        {   
            Target = Physics2D.OverlapBox(transform.position,BoxSize,0,playerLayer).gameObject;
        }
        else
        {
            Target = null;
        }

        //偵測 有玩家 往玩家方向移動 如進入攻擊範圍 停頓數秒後攻擊
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
                PhaseTime = Random.Range(3,6);
                statue = Statue.Patorl;
            }
            break;

            case Statue.Patorl:
            
            //移動
            speed = 3;
            rb.velocity = transform.right * speed;

            if(Target != null)
            {
                lookatyouropposite();
            }

            if(isOnPlayer) //玩家進入攻擊範圍
            {
                statue = Statue.Battle;
            }
            
            if(PhaseTime > 0)
            {                
                PhaseTime -= Time.deltaTime;

                if(!isOnGround)
                {
                    transform.Rotate(0,180,0);
                    faceright = !faceright;
                }

                if(isOnGround && isOnWall)
                {
                    transform.Rotate(0,180,0);
                    faceright = !faceright;
                }
            }
            else if(PhaseTime <= 0)
            {
                PhaseTime = 1.5f;
                statue = Statue.Idle;
            }
            break;

            case Statue.Battle:
            if(!isOnPlayer)
            {
                PhaseTime = 1f;
                statue = Statue.Idle;
            }

            // if(Target != null)
            // {
            //     lookatyouropposite();
            // }

            if(Time.time > (AttackTime_CD + Last_AttackTime) && isOnPlayer) //視線前方有玩家則攻擊
            {
                rb.velocity = new Vector2(0,0);
                Last_AttackTime = Time.time;
                anim.SetTrigger("Attack");
            }

            if(Time.time > Last_AttackTime + 1f)
            {
                if(!Physics2D.OverlapBox(transform.position,BoxSize,0,playerLayer)) //玩家離開了偵測範圍1
                {
                    statue = Statue.Patorl;
                }
            }
            break;

            //偵測範圍內有玩家
            //則向玩家方向移動
            //如進入攻擊範圍
            //則進行準備攻擊動作
            //短暫延遲後攻擊
        }
    }


    //向目標移動
    public void lookatyouropposite()
    {
        if(Target.transform.position.x < transform.position.x && faceright)
        {
            faceright = false;
            transform.Rotate(0,180,0);
            //print("on your left");
        }
        else if(Target.transform.position.x > transform.position.x && !faceright)
        {
            faceright = true;
            transform.Rotate(0,180,0);
            //print("on your right");
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

        RaycastHit2D wallRay = Raycast(PlayerCheckPoint,transform.right,PlayerCheckDistance,groundLayer);

        if(wallRay)
        {
            isOnWall = true;
        }
        else
        {
            isOnWall = false;
        }


        RaycastHit2D playerRay = Raycast(PlayerCheckPoint,transform.right,PlayerCheckDistance,playerLayer);

        if(playerRay)
        {
            isOnPlayer = true;
        }
        else
        {
            isOnPlayer = false;
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
        Gizmos.color = Physics2D.OverlapBox(transform.position,BoxSize,0,playerLayer)? Color.green : Color.clear;
        Gizmos.DrawCube(transform.position,BoxSize);
    }

    void ZboxActive()
    {
        Attack_Box.GetComponent<BoxCollider2D>().enabled = true;
    }

    void ZboxUnActive()
    {
        Attack_Box.GetComponent<BoxCollider2D>().enabled = false;
    }

}
