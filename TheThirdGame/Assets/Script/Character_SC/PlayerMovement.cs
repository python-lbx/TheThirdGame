using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    [Header("移動參數")]
    public int speed;

    [Header("衝刺參數")]
    public float DashTime;//dash時長
    private float DashTimeLeft; //dash剩余時間
    public float LastDash; //上一次dash時間點
    public float DashCD;
    public float DashSpeed;
    public bool Dashing;

    [Header("跳躍參數")]
    public int jumpForce;
    public bool isJump;
    public int jumpTime;

    [Header("角色狀態")]
    public int facedirection;
    public bool faceright;
    public bool isOnGround;
    public bool canClimb;
    public bool canDoubleJump;
    public bool IsClimbing;

    [Header("按鈕控制")]
    private float horizontal;
    private float vertical;
    public bool jumpPressed;
    public bool climbUpPressed;
    public bool climbDownPressed;
    public bool DashPressed;


    [Header("環境檢測")]
    public Transform LeftFootGroundCheckPoint;
    public Transform RightFootGroundCheckPoint;
    public Transform LadderCheckPoint;
    public LayerMask groundLayer;
    public LayerMask LadderLayer;
    public float GroundCheckDistance;

    [Header("角色數值")]
    public Player_Attributes CharacterState;
    public PlayerAttackController playerAttack;
    [Header("角色位置")]
    public GameObject Player;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        playerAttack = GetComponent<PlayerAttackController>();
        
        faceright = true;
        if(!Player.activeSelf)
        {
            Player.SetActive(true);
        }
    }

    void Update()
    {   
        if(horizontal != 0) //記錄方向改向
        {
            facedirection = (int)horizontal;
        }

        if(CharacterState != null) //獲取速度
        {
            DashSpeed = CharacterState.Player_SPD;
        }

        //print(horizontal);
        //檢測行為
        PhysicalCheck();
        Jump();
        Climb();
        animController();

        // //按鈕判定
        // jumpPressed = Input.GetKeyDown(KeyCode.Space);
        // climbUpPressed = Input.GetKey(KeyCode.UpArrow);
        // climbDownPressed = Input.GetKey(KeyCode.DownArrow);
        // DashPressed = Input.GetKeyDown(KeyCode.C);

        jumpPressed = Input.GetKeyDown(GameManager.GM.jump);
        climbUpPressed = Input.GetKey(GameManager.GM.up);
        climbDownPressed = Input.GetKey(GameManager.GM.down);
        DashPressed = Input.GetKeyDown(GameManager.GM.dash);

        AnimatorStateInfo stateinfo = anim.GetCurrentAnimatorStateInfo(1);
        
        //動畫控制
        if(stateinfo.IsName("Attack"))
        {
            if(facedirection != horizontal) //如果方向發生改變就不執行下列程式
            {
                return;
            }
            //原方向移動攻擊 不能反方向移動
        }
        if(stateinfo.IsName("Lightning"))
        {
            if(facedirection != horizontal) //如果方向發生改變就不執行下列程式
            {
                rb.velocity = new Vector2(0,0);
                return;
            }
        }

        Dash();
        if(!Dashing)
        {
            movement();
            flip();
            playerAttack.enabled = true;
        }
        else
        {
            playerAttack.enabled = false;
        }
    }

    public void animController()
    {
        anim.SetFloat("Speed",Mathf.Abs(horizontal));

        if(rb.velocity.y > 0 && jumpPressed)
        {
            anim.SetBool("IsJump",true);
        }
        
        if(isOnGround)
        {
            anim.SetBool("IsJump",false);
        }
    }

    public void flip()
    {
        if(horizontal < 0 && faceright)
        {
            faceright = false;
            transform.Rotate(0,180,0);
        }
        else if(horizontal > 0 && !faceright)
        {
            faceright = true;
            transform.Rotate(0,180,0);
        }
    }

    public void movement()
    {
        //horizontal = Input.GetAxisRaw("Horizontal");

        if(Input.GetKey(GameManager.GM.left))
        {
            horizontal = -1;
        }
        else if(Input.GetKey(GameManager.GM.right))
        {
            horizontal = 1;
        }
        else
        {
            horizontal = 0;
        }

        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    //需優化
    public void Jump()
    {
        if(isOnGround)
        {
            canDoubleJump = true;
        }

        if(jumpPressed && isOnGround)
        {
            jumpPressed = false;
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
        }

        if(jumpPressed && canDoubleJump)
        {
            jumpPressed = false;
            canDoubleJump = false;
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
        }

        if(jumpPressed && canDoubleJump && !isOnGround)
        {
            jumpPressed = false;
            canDoubleJump = false;
            rb.velocity = new Vector2(rb.velocity.x,jumpForce);
        }
    }

    public void Climb()
    {   
        //vertical = Input.GetAxis("Vertical");
        if(climbUpPressed)
        {
            vertical = 1;
        }
        else if(climbDownPressed)
        {
            vertical = -1;
        }
        else
        {
            vertical = 0;
        }

        if(canClimb && Mathf.Abs(vertical) > 0f)
        {
            IsClimbing = true;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x , vertical * jumpForce / 2);
            canDoubleJump = true;
            anim.SetBool("IsJump",false);
            anim.SetBool("IsClimb",true);
        }
        else 
        {   
            rb.gravityScale = 2f;
            IsClimbing = false;
            anim.SetBool("IsClimb",false);
            anim.SetBool("IsJump",true);
        }

        #region 廢案
        /*if(canClimb && climbUpPressed)
        {   
            IsClimbing = true;
            rb.velocity = new Vector2(rb.velocity.x , jumpForce / 2);
            canDoubleJump = true;
            anim.SetBool("IsJump",false);
            anim.SetBool("IsClimb",true);
        }
        else if(canClimb && climbDownPressed)
        {
            IsClimbing = true;
            rb.velocity = new Vector2(rb.velocity.x , -jumpForce / 2);
            canDoubleJump = true;
            anim.SetBool("IsJump",false);
            anim.SetBool("IsClimb",true);
        }*/
        #endregion
    }

    void Dash()
    {
        if(DashPressed && Mathf.Abs(horizontal) > 0)
        {
            if(Time.time > (DashCD +LastDash))
            {
                GetComponent<PlayerAttackController>().newtime = 0.5f;//無敵時間
                anim.SetTrigger("IsDashing");
                
                DashPressed = false;
                LastDash = Time.time;
                DashTimeLeft = DashTime;
                Dashing = true;
            }
        }

        if(Dashing)
        {
            if(DashTimeLeft > 0)
            {                
                gameObject.layer = LayerMask.NameToLayer("Invincible");
                rb.gravityScale = 0;
                rb.velocity = new Vector2(DashSpeed * facedirection,0);
                DashTimeLeft -= Time.deltaTime;
            }
            else if(DashTimeLeft < 0)
            {                
                Dashing = false;
            }
        }

        // if(Time.time >= (LastDash + 0.5f))
        // {
        //     gameObject.layer = LayerMask.NameToLayer("Player");
        // }
    }

    void PhysicalCheck()
    {
        RaycastHit2D LgroundRay = Raycast(LeftFootGroundCheckPoint,Vector2.down,GroundCheckDistance,groundLayer);
        RaycastHit2D RgroundRay = Raycast(RightFootGroundCheckPoint,Vector2.down,GroundCheckDistance,groundLayer);
        RaycastHit2D ladderRay = Raycast(LadderCheckPoint,Vector2.up,GroundCheckDistance,LadderLayer);
        //RaycastHit2D topRay = Raycast(TopCheckPoint,Vector2.up,TopCheckDistance,groundLayer);

            
        if(LgroundRay || RgroundRay)
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }

        if(ladderRay)
        {
            canClimb = true;
        }
        else
        {
            canClimb = false;
        }

            /*if(topRay)
            {
                isHeadBlock = true;
            }
            else
            {
                isHeadBlock = false;
            }*/
    }

    RaycastHit2D Raycast(Transform pointpos, Vector2 raydir, float raydis,LayerMask layer)
    {
        RaycastHit2D hit = Physics2D.Raycast(pointpos.position,raydir,raydis,layer);

        Color color = hit? Color.red:Color.green;

        Debug.DrawRay(pointpos.position,raydir*raydis,color);

        return hit;
    }
}
