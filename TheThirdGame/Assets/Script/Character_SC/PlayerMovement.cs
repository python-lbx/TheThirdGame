using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    private float horizontal;
    private float vertical;

    [Header("移動參數")]
    public int speed;

    [Header("跳躍參數")]
    public int jumpForce;
    public bool jumpPressed;
    public bool isJump;
    public int jumpTime;

    [Header("角色狀態")]
    public bool faceright;
    public bool isOnGround;
    public bool canClimb;

    [Header("環境檢測")]
    public Transform GroundCheckPoint;
    public LayerMask groundLayer;
    public LayerMask LadderLayer;
    public float GroundCheckDistance;

    [Header("角色數值")]
    public Player_Attributes CharacterState;
    
    // Start is called before the first frame update
    private void Awake() 
    {

    }
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
        Jump();
        Climb();
        animController();

        horizontal = Input.GetAxisRaw("Horizontal");
        
        if(CharacterState != null)
        {
            speed = CharacterState.Player_SPD;
        }

        movement();

    }

    public void animController()
    {
        anim.SetFloat("Speed",Mathf.Abs(horizontal));

        if(rb.velocity.y > 0 && Input.GetKeyDown(KeyCode.Space))
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
        transform.Rotate(0,180,0);
        faceright = !faceright;
    }

    public void movement()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
        if(horizontal > 0 && !faceright)
        {
            flip();
        }
        else if(horizontal < 0 && faceright)
        {
            flip();
        }


    }

    public void Jump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && isOnGround && jumpTime >0)
        {
            rb.velocity = new Vector2(rb.velocity.x , jumpForce);
            jumpTime --;
        }
        else if(Input.GetKeyDown(KeyCode.Space) && jumpTime > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x , jumpForce);
            jumpTime --;
        }
        else if(isOnGround)
        {
            jumpTime = 1;
        }
    }

    public void Climb()
    {   
        if(canClimb && Input.GetKey(KeyCode.UpArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x , jumpForce / 2);
            anim.SetBool("IsJump",false);
            anim.SetBool("IsClimb",true);
        }
        else if(canClimb && Input.GetKey(KeyCode.DownArrow))
        {
            rb.velocity = new Vector2(rb.velocity.x , -jumpForce / 2);
            anim.SetBool("IsJump",false);
            anim.SetBool("IsClimb",true);
        }
        else if(Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.DownArrow) || !canClimb)
        {
            anim.SetBool("IsClimb",false);
            anim.SetBool("IsJump",true);
        }
    }

    void PhysicalCheck()
    {
        RaycastHit2D groundRay = Raycast(GroundCheckPoint,Vector2.down,GroundCheckDistance,groundLayer);
        RaycastHit2D ladderRay = Raycast(GroundCheckPoint,Vector2.up,GroundCheckDistance,LadderLayer);
        //RaycastHit2D topRay = Raycast(TopCheckPoint,Vector2.up,TopCheckDistance,groundLayer);

            
        if(groundRay)
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
