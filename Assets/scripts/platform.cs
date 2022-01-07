using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MOVE_DIRECTION
{
    LEFT, RIGHT
}
public class platform : MonoBehaviour
{
    public float speed = 1f;

    public Transform[] platforms;

    public MOVE_DIRECTION direction;

    public Transform bottom, top;
         
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        switch (direction)
        {
            case MOVE_DIRECTION.RIGHT:
                for(int i = 0; i < platforms.Length; i ++)
                {
                    platforms[i].transform.position += speed * Time.deltaTime * Vector3.right;
                    if (platforms[i].transform.position.x > 4.4f)
                        platforms[i].transform.position = new Vector3(-4.4f, transform.position.y);
                }
                break;
            case MOVE_DIRECTION.LEFT:
                for (int i = 0; i < platforms.Length; i++)
                {
                    platforms[i].transform.position += speed * Time.deltaTime * Vector3.left;
                    if (platforms[i].transform.position.x < -4.4f)
                        platforms[i].transform.position = new Vector3(4.4f, transform.position.y);
                }
                break;
        }

        if(transform.position.y < bottom.position.y)
        {
            transform.position = new Vector3(transform.position.x, top.position.y);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.relativeVelocity.y < 0)
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
