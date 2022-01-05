using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public enum STATE
{
    START, IDLE, WALKING, JUMPING, FALLING
}

public class playerController : MonoBehaviour{

    //component

    public Rigidbody2D m_Rigidbody2D;

    public SkeletonAnimation skeletonAnimation;

    public BoxCollider2D boxCollider2D;

    //Layer

    public LayerMask ground;

    public LayerMask wall;

    //Properties

    public float runSpeed = 1f;

    public float jumpForce = 1f;

    float horizontalMove = 0f;

    bool jump = false;

    //Animation

    public AnimationReferenceAsset idle, start, walking, jumping;

    public STATE currentState = STATE.START;
    void Start()
    {
        onStartState();
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case STATE.IDLE:
                Idle();
                break;
            case STATE.WALKING:
                Walking();
                break;
            case STATE.JUMPING:
                Jumping();
                break;
        }

        if (m_Rigidbody2D.velocity.x > 0)
        {
            skeletonAnimation.skeleton.FlipX = false;
        }

        if (m_Rigidbody2D.velocity.x < 0)
        {
            skeletonAnimation.skeleton.FlipX = true;

        }

        //Wall collision
        if (wallCollisionLeft())
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

    }

    public void setCharacterState(STATE state)
    {
        STATE currentState = state;
        switch (currentState)
        {
            case STATE.START:
                setAnimation(start, false, 1f);
                break;
            case STATE.IDLE:
                addAnimation(idle, true, 1f);
                break;
            case STATE.WALKING:
                setAnimation(walking, true, 1f);
                break;
            case STATE.JUMPING:
                setAnimation(jumping, true, 1f);
                break;
        }
    }

    void onStartState()
    {
        setAnimation(start, false, 1f);
        currentState = STATE.IDLE;
    }

    void Idle()
    {
        setCharacterState(STATE.IDLE);
        if(Input.GetKey("left") || Input.GetKey("right"))
        {
            setCharacterState(STATE.WALKING);
            currentState = STATE.WALKING;
        }

        if (Input.GetKeyDown("space"))
        {
            jump = true;
            currentState = STATE.JUMPING;
        }
    }

    void Walking()
    {
        if (Input.GetKey("left") || Input.GetKey("right"))
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            Vector3 targetVelocity = new Vector2(horizontalMove, m_Rigidbody2D.velocity.y);
            m_Rigidbody2D.velocity = targetVelocity;
        } else
        {
            currentState = STATE.IDLE;
        }

        if (Input.GetKeyDown("space"))
        {
            jump = true;
            currentState = STATE.JUMPING;
        }
    }

    void Jumping()
    {
        bool fall = false;
        if(jump)
        {
            setCharacterState(STATE.JUMPING);
            m_Rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
        if(m_Rigidbody2D.velocity.y < 0)
        {
            fall = true;
        }
        if(fall && isGrounded())
        {
            currentState = STATE.IDLE;
        }    
    }

    //Set Character Animation
    public void setAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
    }

    public void addAnimation(AnimationReferenceAsset animation, bool loop, float timeScale)
    {
        skeletonAnimation.state.AddAnimation(0, animation, loop, 0).TimeScale = timeScale;
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
