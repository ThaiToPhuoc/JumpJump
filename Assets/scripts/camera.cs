using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform robot;

    public float smoothspeed = 0.125f;

    public Vector3 offset;

    public float currentStage = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (robot.position.y >= transform.position.y || transform.position.y > currentStage)
        {
            followCharacter();
        } 

        if (transform.position.y > currentStage + Gamedata.instance.stageHeigth)
        {
            currentStage += Gamedata.instance.stageHeigth;
        }
    }

    private void followCharacter()
    {
        Vector3 desiredPosition = new Vector3(0, robot.position.y, 0) + offset;
        Vector3 smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothspeed);
        transform.position = smoothPosition;
    }
}
