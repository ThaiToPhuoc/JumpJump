using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camera : MonoBehaviour
{
    public Transform robot;

    public float smoothspeed = 0.125f;

    public Vector3 offset;

    public float currentStage = 0f;

    public Transform left,right;

    // Start is called before the first frame update
    void Start()
    {
        float width = ActualResolutionWidth(Camera.main.orthographicSize)/100f;
        left.position = new Vector3(- width / 2 - 0.1f,transform.position.y,0);
        right.position = new Vector3(width / 2 + 0.1f, transform.position.y, 0);
    }

    public int ActualResolutionWidth(float orthoSize)
    {
        return (int)(orthoSize * 2.0 * (Screen.width * 1.0) / Screen.height * 100);
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
