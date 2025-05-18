using Unity.VisualScripting;
using UnityEngine;

public class Enemy02 : MonoBehaviour
{
    Rigidbody2D rigid;
    Animator anim;
    
    SpriteRenderer spriteRenderer;
    public int nextMove;
    [SerializeField] float movePower = 0f;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        Invoke("Think", 4);
    }
    private void FixedUpdate()
    {
        //Move
        rigid.linearVelocity = new Vector2(nextMove * movePower, rigid.linearVelocity.y);
        

        //Front Check
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove*0.2f, rigid.position.y);  
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 2.0f, LayerMask.GetMask("Tile"));
        if (rayHit.collider == null)
            Return();
        
    }
    public void Think()
    {
        nextMove = Random.Range(-1, 2);

        //SpriteAnimation
        anim.SetInteger("walkSpeed", nextMove);
        //πÊ«‚
        if(nextMove != 0)
        {
            spriteRenderer.flipX = nextMove == -1;
        }

        //Recursive
        float nextThinkTime = Random.Range(2f, 4f);
        Invoke("Think", nextThinkTime);
    }
    void Return()
    {
        nextMove *= -1;
        spriteRenderer.flipX = nextMove == -1;

        CancelInvoke();
        Invoke("Think", 4);
    }
}
