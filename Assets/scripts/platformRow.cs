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

    public MOVE_DIRECTION direction;

    private float rowWidth;

    private float gap;

    // Start is called before the first frame update
    void Start()
    {
        rowWidth = (ActualResolutionWidth(Camera.main.orthographicSize) / 100f);
        gap = Random.Range(1f, 2.5f);
        createPlatform(direction);
    }

    // Update is called once per frame
    void Update()
    {
        movingPlatform();
    }

    public int ActualResolutionWidth(float orthoSize)
    {
        return (int)(orthoSize * 2.0 * (Screen.width * 1.0) / Screen.height * 100);
    }

    public int ActualResolutionHeight(float orthoSize)
    {
        return (int)(orthoSize * 2.0 * 100);
    }

    public void createPlatform(MOVE_DIRECTION direction)
    {
        this.direction = direction;
        int platformCount = (int)(rowWidth / gap) + 3;
        rowWidth = platformCount * gap;
        float positionX = - rowWidth / 2f;
        for(int i = 0; i < platformCount; i ++)
        {
            platforms.Add(Instantiate(platformPrefab, new Vector3(0, transform.position.y, 0), Quaternion.identity));
            platforms[i].transform.SetParent(transform);
            platforms[i].transform.position = new Vector3(positionX, transform.position.y, 0);
            positionX += gap;
        }
;   }

    private void movingPlatform()
    {
        switch (direction)
        {
            case MOVE_DIRECTION.RIGHT:
                for (int i = 0; i < platforms.Count; i++)
                {
                    platforms[i].transform.position += speed * Time.deltaTime * Vector3.right;
                    if (platforms[i].transform.position.x > rowWidth/2)
                        platforms[i].transform.position = new Vector3(-rowWidth / 2, transform.position.y);
                }
                break;
            case MOVE_DIRECTION.LEFT:
                for (int i = 0; i < platforms.Count; i++)
                {
                    platforms[i].transform.position += speed * Time.deltaTime * Vector3.left;
                    if (platforms[i].transform.position.x < -rowWidth/2)
                        platforms[i].transform.position = new Vector3(rowWidth/2, transform.position.y);
                }
                break;
        }
    }
}
