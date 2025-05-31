using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    private SpriteRenderer sprtieRenderer;
    private CapsuleCollider2D capsule;
    public ManageHeart managerHeart;
    public GameManager gameManager;
    public PlayerHP playerHP;
    public float movePower = 1f;
    public float jumpPower = 1f;
    public int jumpnumber = 0;
    private EnemyMove enemyMove;
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
        enemyMove = GetComponent<EnemyMove>();
        sprtieRenderer = GetComponent<SpriteRenderer>();
        capsule = GetComponent<CapsuleCollider2D>();

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
        if(rigid.linearVelocityY < 0)
        {
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0));
            RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, Vector3.down, 1.0f, LayerMask.GetMask("Tile"));
            if (rayHit.collider != null)
            {
                Debug.Log("H");
            }
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
        if(collision.gameObject.tag == "Enemy")
        {
            if(transform.position.y > collision.transform.position.y)
            {
                OnAttack(collision.transform);
            }
            else
            {
                OnDamaged(collision.transform.position);
                playerHP.damage(1.0f);
            }
        }
        else if (collision.gameObject.tag == "Trap")
        {
            OnDamaged(collision.transform.position);
            playerHP.damage(1.0f);
        }
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            //Point
            bool isScore = collision.gameObject.name.Contains("Score");
            bool isCherry = collision.gameObject.name.Contains("cherry");
            if (isScore)
            {
                gameManager.stagePoint += 100;
            }
            else if (isCherry)
            {
                playerHP.CurrentHP = 3;
                
            }
                //Deactive Scroe
                collision.gameObject.SetActive(false);
        }
        else if(collision.gameObject.tag == "Finish")
        {
            // Goal in
            SceneManager.LoadScene("GameClear");
        }
        else if (collision.gameObject.tag == "nextStage")
        {
            // next Stage
            gameManager.NextStage();
        }
    }

    public void OnDamaged(Vector2 targetPos)
    {
        // Change Layer
        gameObject.layer = 9;

        // Hit Motion
        anim.SetTrigger("isHit");

        // Hit direction
        int dirc = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rigid.AddForce(new Vector2(dirc, 1) * 5, ForceMode2D.Impulse);

        Invoke("OffDamaged", 2);
    }
    void OffDamaged()
    {
        gameObject.layer = 8;
        
    }
    void OnAttack(Transform enemy)
    {
        // Point
        gameManager.stagePoint += 150;

        // Reaction Force
        rigid.AddForce(Vector2.up * 7, ForceMode2D.Impulse);

        // Enemy Die 
        EnemyMove enemyMove = enemy.GetComponent<EnemyMove>();
        enemyMove.TakeDamage();
    }
    public void DieEffect()
    {
        Debug.Log("Player Die");
        SceneManager.LoadScene("GameOver");
    }
    public void VelocityZero()
    {
        rigid.linearVelocity = Vector2.zero;
    }

} 
