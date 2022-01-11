using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MOVE_DIRECTION
{
    LEFT, RIGHT
}
public class platformRow : MonoBehaviour
{
    public float speed = 1f;

    public List<Transform> platforms;

    public Transform platformPrefab;

    public float gap;

    public MOVE_DIRECTION direction;

    public Transform bottom, top;
         
    // Start is called before the first frame update
    void Start()
    {
       setPlatform();
    }

    // Update is called once per frame
    void Update()
    {
        switch (direction)
        {
            case MOVE_DIRECTION.RIGHT:
                for (int i = 0; i < platforms.Count; i++)
                {
                    platforms[i].transform.position += speed * Time.deltaTime * Vector3.right;
                    if (platforms[i].transform.position.x > 3f)
                        platforms[i].transform.position = new Vector3(-4.4f, transform.position.y);
                }
                break;
            case MOVE_DIRECTION.LEFT:
                for (int i = 0; i < platforms.Count; i++)
                {
                    platforms[i].transform.position += speed * Time.deltaTime * Vector3.left;
                    if (platforms[i].transform.position.x < -3f)
                        platforms[i].transform.position = new Vector3(3f, transform.position.y);
                }
                break;
        }
    }

    public int ActualResolutionWidth(float orthoSize)
    {
        return (int)(orthoSize * 2.0 * (Screen.width * 1.0) / Screen.height * 100);
    }

    public int ActualResolutionHeight(float orthoSize)
    {
        return (int)(orthoSize * 2.0 * 100);
    }

    private void setPlatform()
    {
        int platformRowWidth = (ActualResolutionWidth(Camera.main.orthographicSize) / 100) + 2;
        int platformCount = (int)(platformRowWidth / gap) + 1;
        float positionX = - platformRowWidth / 2f;
        for(int i = 0; i < platformCount; i ++)
        {
            platforms.Add(Instantiate(platformPrefab, new Vector3(0, transform.position.y, 0), Quaternion.identity));
            platforms[i].transform.SetParent(transform);
            platforms[i].transform.position = new Vector3(positionX, transform.position.y, 0);
            positionX += gap;
        }
;   }
}
