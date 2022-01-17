using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;
using Spine;
public enum STATE
{
    START, IDLE, WALKING, JUMPING, FALLING, DEATH
}

public class playerController : MonoBehaviour{

    //component
    public Transform camera;

    public Rigidbody2D m_Rigidbody2D;

    public SkeletonAnimation skeletonAnimation;

    public BoxCollider2D boxCollider2D;

    //Layer

    public LayerMask ground;

    public LayerMask wall;

    public LayerMask bottom;

    //Properties

    public float runSpeed = 1f;

    public float jumpForce = 1f;

    float horizontalMove = 0f;

    bool jump = false;

    bool moving = false;

    bool fall = false;

    private float timeExtraScore = 0f;

    //Animation

    public AnimationReferenceAsset idle, start, walking, jumping, death;

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
                currentState = STATE.IDLE;
                setCharacterState(currentState);
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
            case STATE.DEATH:
                break;
        }

        //Flip
        if(!isDeath())
        {
            if (Input.GetKeyDown("right"))
            {
                skeletonAnimation.skeleton.FlipX = false;
            }

            if (Input.GetKeyDown("left"))
            {
                skeletonAnimation.skeleton.FlipX = true;

            }
        }

        //Count down Extra Score
        if (timeExtraScore > 0)
            timeExtraScore -= Time.deltaTime;
    }

    private void FixedUpdate()
    {

        if (jump)
        {
            setCharacterState(currentState);
            m_Rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }

        if (moving)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
            Vector3 targetVelocity = new Vector2(horizontalMove, m_Rigidbody2D.velocity.y);
            m_Rigidbody2D.velocity = targetVelocity;
        }
        //Wall collision
        if (wallCollisionLeft())
        {
            if (m_Rigidbody2D.velocity.x < 0)
                horizontalMove = 0;
        }

        if (wallCollisionRight())
        {
            if (m_Rigidbody2D.velocity.x > 0)
                horizontalMove = 0;
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
            case STATE.DEATH:
                setAnimation(death, false, 1f);
                break;
        }
    }

    void gameover()
    {
        currentState = STATE.DEATH;
        setCharacterState(currentState);
        m_Rigidbody2D.bodyType = RigidbodyType2D.Static;

        if(PlayerPrefs.GetInt("highScore") == null)
        {
            PlayerPrefs.SetInt("highScore", Gamedata.instance.finalScore);
        }
        else
        {
            if(Gamedata.instance.finalScore > PlayerPrefs.GetInt("highScore"))
                PlayerPrefs.SetInt("highScore", Gamedata.instance.finalScore);
        }
        Gamedata.instance.gameover = true;
    }
    void onStartState()
    {
        setCharacterState(currentState);
    }

    void Idle()
    {
        fall = false;
        if(Input.GetKey("left") || Input.GetKey("right"))
        {
            currentState = STATE.WALKING;
            setCharacterState(currentState);
            moving = true;
        }

        if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
        {
            jump = true;
            currentState = STATE.JUMPING;
        }


        if (isDeath())
        {
            gameover();
        }

        if(Gamedata.instance.score > 40)
        {
            if (wallCollisionLeft() || wallCollisionRight())
                gameover();
        }    
    }

    void Walking()
    {

        if (!(Input.GetKey("left") || Input.GetKey("right")))
        {
            currentState = STATE.IDLE;
            setCharacterState(currentState);
            moving = false;
        }

        if (Input.GetKeyDown("space") || Input.GetMouseButtonDown(0))
        {
            jump = true;
            moving = false;
            currentState = STATE.JUMPING;
        }

        if(isDeath())
        {
            gameover();
        }
        if (Gamedata.instance.score > 40)
        {
            if (wallCollisionLeft() || wallCollisionRight())
                gameover();
        }
    }

    void Jumping()
    {
        fall = false;
        if (m_Rigidbody2D.velocity.y < 0)
        {
            fall = true;
        }
        if (fall && isGrounded())
        {
            moving = false;
            currentState = STATE.IDLE;
            setCharacterState(currentState);
        }

        if (isDeath())
        {
            gameover();
        }
        if (Gamedata.instance.score > 40)
        {
            if (wallCollisionLeft() || wallCollisionRight())
                gameover();
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

    private bool isDeath()
    {
        return Physics2D.BoxCast(boxCollider2D.bounds.center, boxCollider2D.bounds.size, 0f, Vector2.down, .1f, bottom);
    }

    public void setCurrentState(STATE state)
    {
        this.currentState = state;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Saw(Clone)")
            gameover();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Coin(Clone)")
        {
            Gamedata.instance.coins++;
            Gamedata.instance.finalScore++;

            if (timeExtraScore > 0)
                Gamedata.instance.finalScore += 10;

            timeExtraScore = 10f;
        }
    }
}
