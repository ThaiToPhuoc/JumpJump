using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine.Unity;

public class playerController : MonoBehaviour
{
    public SkeletonAnimation skeletonAnimation;
    public AnimationReferenceAsset idle, walking;
    private Rigidbody2D rigidbody;
    public string currentState;

    public float speed;
    public float movement;
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        currentState = "idle";
        SetCharaterState(currentState);
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }
    
    //Set character animation
    public void SetAnimation(AnimationReferenceAsset animation, bool loop, float timeScale){
        skeletonAnimation.state.SetAnimation(0, animation, loop).TimeScale = timeScale;
    }

    //Set character state
    public void SetCharaterState(string state){
        if(state.Equals("idle")){
            SetAnimation(idle, true, 1f);
        }
        else if (state.Equals("walking"))
        {
            SetAnimation(walking, true, 1f);
        }
    }

    public void move(){
        movement = Input.GetAxis("Horizontal");
        rigidbody.velocity = new Vector2(movement * speed, rigidbody.velocity.y);
        if (movement != 0)
        {
            SetCharaterState("walking");
        }
        else
        {
            SetCharaterState("idle");
        }
    }
}
