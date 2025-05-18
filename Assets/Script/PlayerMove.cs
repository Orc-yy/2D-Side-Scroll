using UnityEngine;
using UnityEngine.Android;

public class PlayerMove : MonoBehaviour
{
    public float movePower = 1f;
    public float jumpPower = 1f;
    public int jumpnumber = 0;
    SpriteRenderer spriteRenderer;
    Animator anim;

    [SerializeField]
    public float normalSpeed, runSpeed;

    Rigidbody2D rigid;

    Vector3 movement;
    bool isJumping = false;
    private void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        // DoubleJump
        if (Input.GetButtonDown("Jump") && jumpnumber < 2)
        {
            isJumping = true;
            
        }
        else if (jumpnumber == 2)
        {
            anim.SetBool("isDoubleJump", true);
        }
        
    }
    private void FixedUpdate()
    {
        Move();
        Jump();
        //Landing Platform
        Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1.0f, LayerMask.GetMask("Tile"));
        if(rayHit.collider != null)
        {
            if (rayHit.distance < 1.0f)
                Debug.Log(rayHit.collider.name);
        }

    }
    private void Move()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        { 
            if(Input.GetAxisRaw("Horizontal") == 0)
            {
                anim.SetBool("isIdle", true);
                anim.SetBool("isFastRun", false);
            }
            else
            {
                anim.SetBool("isIdle", false);
            }

                movePower = runSpeed;
            anim.SetBool("isFastRun", true);
        }
        else
        {
            movePower = normalSpeed;
            anim.SetBool("isFastRun", false);
        }
            Vector3 moveVelocity = Vector3.zero;
        if(Input.GetAxisRaw("Horizontal") < 0)
        {
            moveVelocity = Vector3.left;
            spriteRenderer.flipX = true;
        }
        else if(Input.GetAxisRaw("Horizontal") > 0)
        {
            moveVelocity = Vector3.right;
            spriteRenderer.flipX = false;
        }

        // RunAnimation
        if(Input.GetAxisRaw("Horizontal") == 0)
            anim.SetBool("isRun",false);
        else
            anim.SetBool("isRun", true);

        transform.position += moveVelocity * movePower * Time.deltaTime;
    }
    void Jump()
    {
        if (isJumping == true)
        {
            rigid.linearVelocity = Vector2.zero;

            Vector2 jumpVelocity = new Vector2(0, jumpPower);
            rigid.AddForce(jumpVelocity, ForceMode2D.Impulse);

            anim.SetBool("isJump", true);
            anim.SetBool("isDown", true);
            jumpnumber += 1;
            isJumping = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.name)
        {
            case "Tilemap":
                jumpnumber = 0;
                anim.SetBool("isJump", false);
                anim.SetBool("isDoubleJump", false);
                anim.SetBool("isDown", false);
                break;
        }
    }
}
