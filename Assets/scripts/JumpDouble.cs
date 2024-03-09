using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpDouble : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float jumpDoubleTime;
    private float jumpDoubleCounter;
    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    private float horizontalInput;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Flip player when moving left-right
        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);

        // Set animator parameters
        anim.SetBool("run", Mathf.Abs(horizontalInput) > 0.01f);
        anim.SetBool("grounded", isGrounded());

        // Jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
        // Jump Height
        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0)
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);
        }

        // Restore gravity when not on wall or ground
        if (!isGrounded() && !onWall())
        {
            body.gravityScale = 1;
        }
        if (isGrounded())
        {
            jumpDoubleCounter = jumpDoubleTime ;

        }
        else
        {
            jumpDoubleCounter -= Time.deltaTime;
        }

        // Move horizontally
        body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);
    }

    private void Jump()
    {
        if (jumpDoubleCounter <= 0 && !onWall())
        {
            return;
        }
        if (onWall())
        {
          // nothing
        }
        else
        {
            if (isGrounded())
            {
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            }
            else
            {
                if(jumpDoubleCounter > 0)
                {
                    body.velocity = new Vector2(body.velocity.x, jumpPower);
                }
            }
            jumpDoubleCounter = 0; 
        }
    }
   



    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
