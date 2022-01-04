using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class playerController : MonoBehaviour{

    public Rigidbody2D m_Rigidbody2D;

    public SkeletonAnimation skeletonAnimation;

    public BoxCollider2D boxCollider2D;

    public LayerMask ground;

    public LayerMask wall;

    public float runSpeed = 1f;

    public float jumpForce = 1f;

    float horizontalMove = 0f;

    bool jump = false;

    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

        if (Input.GetKeyDown("space"))
        {
            jump = true;
        }

        //Flip
        
        if(m_Rigidbody2D.velocity.x > 0)
        {
            skeletonAnimation.skeleton.FlipX = false;
        }

        if (m_Rigidbody2D.velocity.x < 0)
        {
            skeletonAnimation.skeleton.FlipX = true;

        }

        //Wall collision
        if(wallCollisionLeft())
        {
            if (horizontalMove < 0)
                horizontalMove = 0;
        }

        if (wallCollisionRight())
        {
            if (horizontalMove > 0)
                horizontalMove = 0;
        }
    }

    private void FixedUpdate()
    {
        Vector3 targetVelocity = new Vector2(horizontalMove, m_Rigidbody2D.velocity.y);
        m_Rigidbody2D.velocity = targetVelocity;

        if (jump && isGrounded())
        {
            m_Rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }

    }
    
    private bool isGrounded()
    {
        return Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, ground);
    }

    private bool wallCollisionLeft()
    {
        return Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.left, .1f, wall);
    }

    private bool wallCollisionRight()
    {
        return Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.right, .1f, wall);
    }
}
