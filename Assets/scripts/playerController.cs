using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
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

    bool moving = false;

    //Animation

    public AnimationReferenceAsset idle, start, walking, jumping;

    public STATE currentState = STATE.START;
    void Start()
    {
        skeletonAnimation.state.Complete += AnimationCompleteHandler;
        onStartState();
    }

    public void AnimationCompleteHandler(TrackEntry trackEntry)
    {
        switch (trackEntry.Animation.Name)
        {
            case "Start":
                setCharacterState(STATE.IDLE);
                currentState = STATE.IDLE;
                break;
        }
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

        //Flip

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
            if (m_Rigidbody2D.velocity.x < 0)
                horizontalMove = 0;
        }

        if (wallCollisionRight())
        {
            if (m_Rigidbody2D.velocity.x < 0)
                horizontalMove = 0;
        }

    }

    private void FixedUpdate()
    {
        if (jump)
        {
            setCharacterState(STATE.JUMPING);
            m_Rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }

        if (moving)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            Vector3 targetVelocity = new Vector2(horizontalMove, m_Rigidbody2D.velocity.y);
            m_Rigidbody2D.velocity = targetVelocity;
        }
    }

    public void setCharacterState(STATE state)
    {
        switch (state)
        {
            case STATE.START:
                setAnimation(start, false, 1f);
                break;
            case STATE.IDLE:
                setAnimation(idle, true, 1f);
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
        setCharacterState(STATE.START);
    }

    void Idle()
    {
        if(Input.GetKey("left") || Input.GetKey("right"))
        {
            setCharacterState(STATE.WALKING);
            currentState = STATE.WALKING;
            moving = true;
        }

        if (Input.GetKeyDown("space"))
        {
            jump = true;
            currentState = STATE.JUMPING;
        }
    }

    void Walking()
    {
        
        if (!(Input.GetKey("left") || Input.GetKey("right")))
        {
            setCharacterState(STATE.IDLE);
            currentState = STATE.IDLE;
            moving = false;
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
        if(m_Rigidbody2D.velocity.y < 0)
        {
            fall = true;
        }
        if(fall && isGrounded())
        {
            moving = false;
            setCharacterState(STATE.IDLE);
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
