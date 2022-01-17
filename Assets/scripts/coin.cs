using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class coin : MonoBehaviour
{
    public SkeletonAnimation animation;
    // Start is called before the first frame update
    void Start()
    {
        animation.state.Complete += AnimationCompleteHandler;
    }
    public void AnimationCompleteHandler(TrackEntry trackEntry)
    {
        if(trackEntry.Animation.Name.Equals("Collect"))
        {
            Destroy(this.gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animation.AnimationName = "Collect";
    }
}
